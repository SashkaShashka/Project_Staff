using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Staff
{
    class PositionController
    {
        public PositionManager Positions { get; }

        private Dictionary<string, int> Devision = new Dictionary<string, int>();

        public void AddStaff(StaffManager staffs)
        {
            this.Positions.AddStaff(staffs);
        }
        string searchBy = null;
        string SearchBy { get => searchBy; set => searchBy = value.ToLower(); }

        string filterBy = null;
        string FilterBy { get => filterBy; set => filterBy = value; }
         IList<Position> AllPositions //позже сделаю выборку
        {
            get
            {
                var positions = Positions;
                if (!string.IsNullOrEmpty(SearchBy))
                    return positions.Where(position => position.Title.ToLower().Contains(searchBy) || position.Devision.ToLower().Contains(searchBy)).ToList();
                if (!string.IsNullOrEmpty(FilterBy))
                    return positions.Where(position => position.Devision.Equals(FilterBy)).ToList();
                return positions.ToList();
            }
        }

        Table<Position> table = new Table<Position>(new[] {
            new TableColumn<Position>("Должность", 25, position => position.Title),
            new TableColumn<Position>("Подразделение", 25, position => position.Devision),
            new TableColumn<Position>("Оклад", 15, position => string.Format("{0:C2}", position.Salary)),
        });
        public Menu Menu { get; }
        public SelectFromList<Position> SelectPosition { get; }
        public Position SelectedPosition => SelectPosition.SelectedNode;

        public SelectFromList<string> SelectDevision { get; }
        public string SelectedDevision => SelectDevision.SelectedNode;
        private void AddPosition()
        {
            if (Positions.Lenght==0)
                SelectPosition.SelectedNodeIndex = 0;
            Console.Clear();
            Positions.AddPosition(InputValidator.ReadString("Должность"), InputValidator.ReadString("Подразделение"), InputValidator.ReadSalary());
            string dev = Positions[Positions.Lenght - 1].Devision;
            if (!Devision.ContainsKey(dev))
                Devision.Add(dev, 1);
            else
                ++Devision[dev];

        }
        private void EditPosition()
        {
            if (Positions.Lenght <= 0)
            {
                Console.WriteLine("Таблица пуста!");
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить");
                Console.ReadKey(true);
                return;
            }
            Console.Clear();
            Position position = SelectedPosition;
            string old_dev = position.Devision;
            Console.WriteLine("Редактирование товара: ");
            Console.WriteLine(position.ToString());
            Positions.Edit(position, new Position(InputValidator.ReadString("Должность"), InputValidator.ReadString("Подразделение"), InputValidator.ReadSalary()));
            if (!old_dev.Equals(position.Devision))
            {
                if (Devision[old_dev] > 1)
                    --Devision[old_dev];
                else
                    Devision.Remove(old_dev);
                if (!Devision.ContainsKey(position.Devision))
                    Devision.Add(position.Devision, 1);
                else
                    ++Devision[position.Devision];
            }
                



        }
        private void RemovePosition()
        {
            if (Positions.Lenght<=0)
            {
                Console.WriteLine("Таблица пуста!");
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить");
                Console.ReadKey(true);
                return;
            }
            Console.Clear();
            var position = SelectedPosition;
            Console.WriteLine("Вы уверены, что хотете безвозвратно удалить должность: \"{0}\"? ", position.Title);

            Console.WriteLine("Нажмите D, чтобы подтвердить удаление, или любую другую клавишу, чтобы отменить");
            if (Console.ReadKey(true).Key == ConsoleKey.D)
            {
                if (Positions.RemovePosition(position))
                {
                    if (Devision[position.Devision] > 1)
                        --Devision[position.Devision];
                    else
                        Devision.Remove(position.Devision);
                    Console.WriteLine("Должность успешно удалена!");
                    SelectPosition.SelectedNodeIndex=0;
                }
                else
                    Console.WriteLine("Удалить должность нельзя, так как имеются связанные с ней сотрудники");
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить");
                Console.ReadKey(true);
            }
           
        }

        void SaveToFile()
        {
            try
            {
                SelectFile.SaveToFile(Positions.Positions.Select(p => PositionFileDto.Map(p)), "Должности");
                Console.WriteLine("Список должностей успешно сохранен!");
            }
            finally
            {
                Console.WriteLine($"Для возврата к списку должностей нажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        void LoadFromFile()
        {
            try
            {
                var loadedData = SelectFile.LoadFromFile<PositionFileDto>("Должности");
                if (loadedData != null)
                {
                    Console.WriteLine("Выполняем чтение данных...");
                    Positions.LoadPosition(loadedData.Select(p => PositionFileDto.Map(p)));
                    Console.WriteLine("Список должностей успешно загружен!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка! Файл содержит некорректные данные: " + e.Message);
            }
            finally
            {
                Console.WriteLine($"Для возврата к списку должностей нажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        public PositionController(List<Position> positions)
        {
            Positions = new PositionManager(positions);
            SelectPosition = new SelectFromList<Position>(() => AllPositions);
            foreach (var item in Positions)
            {
                if (!Devision.ContainsKey(item.Devision))
                    Devision.Add(item.Devision, 1);
                else
                    Devision[item.Devision] = Devision[item.Devision]+1;
            }
            SelectDevision = new SelectFromList<string>(() => Devision.Keys.ToList());
            Menu = new Menu(new List<MenuItem>(SelectPosition.Menu.Items) {
                   new MenuAction(ConsoleKey.F1, "Новая должность", AddPosition),
                   new MenuAction(ConsoleKey.F2, "Редактировать", EditPosition),
                   new MenuAction(ConsoleKey.F3, "Удалить", RemovePosition),
                   new MenuAction(ConsoleKey.F4, "Поиск",  SearchPosition),
                   new MenuAction(ConsoleKey.F5, "Фильтр по подразделению", FilterDivision),
                   new MenuAction(ConsoleKey.F6, "Сбросить фильтры", () =>  FilterBy = null),
                   new MenuAction(ConsoleKey.F9, "Сохранить", SaveToFile),
                   new MenuAction(ConsoleKey.F10, "Загрузить", LoadFromFile),
                   new MenuClose(ConsoleKey.Tab, "Вернуться к сотрудникам"),
                }
            );
            
           
        }

        private void FilterDivision()
        {
            ConsoleKey ch;
            SelectDevision.SelectedNodeIndex = 0;
            string select="";
            do
            {
                Console.Clear();
                Console.WriteLine("Выберите подразделение");
                foreach (var item in Devision)
                {
                    if (item.Key==SelectedDevision)
                    {
                        select = item.Key;
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.ForegroundColor = ConsoleColor.White;
                        for (int i = 0; i < Console.WindowWidth; i++)
                        {
                            Console.Write(" ");
                        }
                        Console.CursorLeft = 0;
                    }
                    Console.WriteLine(item.Key);
                    Console.ResetColor();
                }
                ch = Console.ReadKey(true).Key;
                switch (ch)
                {
                    case ConsoleKey.UpArrow:
                        SelectDevision.SelectedNodeIndex--;
                        break;
                    case ConsoleKey.DownArrow:
                        SelectDevision.SelectedNodeIndex++;
                        break;
                }
                if (SelectDevision.SelectedNodeIndex < 0)
                    SelectDevision.SelectedNodeIndex = Devision.Count - 1;
                if (SelectDevision.SelectedNodeIndex > Devision.Count - 1)
                    SelectDevision.SelectedNodeIndex = 0;
                Console.Clear();
            } while (ch != ConsoleKey.Enter);
            SelectPosition.SelectedNodeIndex = 0;
            FilterBy = select;
        }

        private void SearchPosition()
        {
            Console.Clear();
            Console.WriteLine("Поиск сотрудников по ФИО");
            Console.Write("Введите текст для поиска, или пустое значение, чтобы показать всех сотрудников:");
            SelectPosition.SelectedNodeIndex = 0;
            SearchBy = Console.ReadLine();
        }

        public void PrintAll()
        {
            table.Print(AllPositions, SelectedPosition);
        }
    }
}
