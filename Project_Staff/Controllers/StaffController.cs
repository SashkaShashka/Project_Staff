using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Staff
{
    class StaffController
    {
        public StaffManager Staffs { get; private set; }

        bool isOrderByBirthDayDesc = false;

        string searchBy = null;
        string SearchBy { get => searchBy; set => searchBy = value.ToLower(); }
        public IList<Staff> OrderedStaffs
        {
            get
            {
                var staffs = Staffs.Staffs;
                if (!string.IsNullOrEmpty(SearchBy))
                    staffs = staffs.Where(staff => staff.FirstName.ToLower().Contains(SearchBy) || staff.MiddleName.ToLower().Contains(SearchBy) || staff.SurName.ToLower().Contains(SearchBy));
                if (isOrderByBirthDayDesc)
                    return staffs.OrderByDescending(staff => staff.BirthDay).ToList();
                else
                    return staffs.OrderBy(staff => staff.BirthDay).ToList();

            }
        }
        public Menu Menu { get; }
        public Menu SortMenu { get; }
        public SelectFromList<Staff> SelectStaff { get; }
        public Staff SelectedStaff => SelectStaff.SelectedNode;
        public SelectFromList<Position> SelectPosition { get; }
        public Position SelectedPosition => SelectPosition.SelectedNode;

        public PositionManager Positions { get; }

        Table<Staff> table = new Table<Staff>(new[] {
            new TableColumn<Staff>("№",10,
                staff => string.Format("{0:000000}", staff.ServiceNumber)),
            new TableColumn<Staff>("Фамилия И.О.", 20,
                staff => string.Join(' ', new string[] {staff.SurName,string.Join("",new string[] { staff.FirstName.Substring(0, 1), ".",staff.MiddleName.Length!=0 ? string.Join("", new string[] { staff.MiddleName.Substring(0, 1), "." }) : ""}) })),
            new TableColumn<Staff>("Дата рождения", 14,
                staff => staff.BirthDay.ToString("d")),
            new TableColumn<Staff>("Кол-во должностей", 18,
                staff => staff.Posts.Count.ToString()),
            new TableColumn<Staff>("Суммарный оклад", 15,
                staff =>  string.Format("{0:C2}", staff.CalculateSalary)),
        });

        
        private void StaffInfo()
        {
            Console.Clear();
            Console.WriteLine(SelectedStaff.ToString());
            Console.WriteLine("Нажмите любую клавишу, чтобы продолжить");
            Console.ReadKey(true);

        }
        private void AddStaff()
        {
            if (Positions.Lenght == 0)
                SelectStaff.SelectedNodeIndex = 0;
            Console.Clear();
            Staffs.AddStaff(InputValidator.ReadString("Фамилию"), InputValidator.ReadString("Имя"), InputValidator.ReadString("Отчество", false), InputValidator.ReadBirthDay());
        }
        private void EditStaff()
        {
            if (Staffs.Lenght <= 0)
            {
                Console.WriteLine("Таблица пуста!");
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить");
                Console.ReadKey(true);
                return;
            }
            Console.Clear();
            Staff staff = SelectedStaff;
            Console.WriteLine("Редактирование сотрудника: ");
            Console.WriteLine(staff.ToShortString());
            Staffs.Edit(staff.ServiceNumber, InputValidator.ReadString("Фамилию"), InputValidator.ReadString("Имя"), InputValidator.ReadString("Отчество", false), InputValidator.ReadBirthDay());
        }
        private void RemoveStaff()
        {
            if (Staffs.Lenght <= 0)
            {
                Console.WriteLine("Таблица пуста!");
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить");
                Console.ReadKey(true);
                return;
            }
            Console.Clear();
            Staff staff = SelectedStaff;
            Console.WriteLine("Вы уверены, что хотете безвозвратно удалить следующего сотрудника?:\n{0}", staff.ToShortString());

            Console.WriteLine("Нажмите D, чтобы подтвердить удаление, или любую другую клавишу, чтобы отменить");
            if (Console.ReadKey(true).Key == ConsoleKey.D)
            {

                if (Staffs.RemoveStaff(staff))
                {
                    Console.WriteLine("Должность успешно удалена!");
                    SelectStaff.SelectedNodeIndex--;
                }
                else
                    Console.WriteLine("Что-то пошло не так");
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить");
                Console.ReadKey(true);
            }

        }
        private void AddPost()
        {
            ConsoleKey ch;
            while(true)
            {
                do
                {
                    Console.WriteLine("Нажмите ESC, чтобы завершить, или Enter для выбора.");
                    PrintPositions();
                    ch = Console.ReadKey(true).Key;
                    switch (ch)
                    {
                        case ConsoleKey.UpArrow:
                            SelectPosition.Menu.Action(ConsoleKey.UpArrow);
                            break;
                        case ConsoleKey.DownArrow:
                            SelectPosition.Menu.Action(ConsoleKey.DownArrow);
                            break;
                    }
                    Console.Clear();
                } while (ch != ConsoleKey.Escape && ch != ConsoleKey.Enter);
                if (ch==ConsoleKey.Enter)
                    SelectedStaff.AddPost(new Post(SelectedPosition, InputValidator.ReadBet()));
                else break;
            } 
            
        }
        private void RemovePost()
        {
            int index;
            while (true)
            {
                index = Remove();
                if (index != -1)
                    SelectedStaff.RemovePost(SelectedStaff.Posts[index].Position);
                else break;
            }
        }
        private void ChooseOrder()
        {
            Console.CursorTop = 0;
            SortMenu.Print();
            SortMenu.Action(Console.ReadKey().Key);
        }

        private void SearchStaff()
        {
            Console.Clear();
            Console.WriteLine("Поиск сотрудников по ФИО");
            Console.Write("Введите текст для поиска, или пустое значение, чтобы показать всех сотрудников:");
            SelectStaff.SelectedNodeIndex = 0;
            SearchBy = Console.ReadLine();
        }

        public StaffController(PositionManager positions, List<Staff> staffs)
        {
            Positions = positions;
            Staffs = new StaffManager(staffs);
            SelectStaff = new SelectFromList<Staff>(() => OrderedStaffs);

            SelectPosition = new SelectFromList<Position>(() => Positions.Positions.ToList());
            Menu = new Menu(new List<MenuItem>(SelectStaff.Menu.Items) {
                   new MenuAction(ConsoleKey.I, "Посмотреть", StaffInfo),
                   new MenuAction(ConsoleKey.F1, "Новый сотрудник", AddStaff),
                   new MenuAction(ConsoleKey.F2, "Редактировать", EditStaff),
                   new MenuAction(ConsoleKey.F3, "Новая должность", AddPost),
                   new MenuAction(ConsoleKey.F4, "Удалить должность", RemovePost),
                   new MenuAction(ConsoleKey.F5, "Удалить", RemoveStaff),
                   new MenuAction(ConsoleKey.F6, "Сортировать", ChooseOrder),
                   new MenuAction(ConsoleKey.F7, "Поиск по ФИО", SearchStaff),
                /* 
                 
                new MenuAction(ConsoleKey.F9, "Сохранить",
                    SaveToFile),
                new MenuAction(ConsoleKey.F10, "Загрузить",
                    LoadFromFile),
                */
            });
            SortMenu = new Menu(new List<MenuItem>() {
                new MenuAction(ConsoleKey.DownArrow, "Сортировка по убыванию", () => isOrderByBirthDayDesc = true),
                new MenuAction(ConsoleKey.UpArrow, "Сортировка по цене по ворастанию", () => isOrderByBirthDayDesc = false),
            });
        }
        private void PrintPositions()
        {
            Console.Clear();
            foreach (Position position in Positions)
            {
                if (position.Equals(SelectedPosition))
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    for (int j = 0; j < 3; j++)
                    {
                        for (int k = 0; k < Console.WindowWidth; k++)
                            Console.Write(" ");
                        Console.CursorLeft = 0;
                        Console.CursorTop++;
                    }
                    Console.CursorTop = Console.CursorTop - 3;
                    Console.CursorLeft = 0;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(position.ToString());
                    Console.ResetColor();
                }
                else
                    Console.Write(position.ToStringTitle());
            }
        }
        private void PrintPost(int index) //не знаю, как сделать лучше
        {
            int i = 0;
            foreach (Post post in SelectedStaff.Posts)
            {

                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    for (int j = 0; j < 4; j++)
                    {
                        for (int k = 0; k < Console.WindowWidth; k++)
                            Console.Write(" ");
                        Console.CursorLeft = 0;
                        Console.CursorTop++;
                    }
                    Console.CursorTop -= 4;
                    Console.CursorLeft = 0;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write(post.ToString());
                Console.ResetColor();
                i++;
            }
        }
        private int Remove()
        {
            Console.Clear();
            ConsoleKey ch;
            int index = 0;
            do
            {
                Console.WriteLine("Нажмите ESC, чтобы завершить, или Enter для выбора.");
                PrintPost(index);
                ch = Console.ReadKey(true).Key;
                switch (ch)
                {
                    case ConsoleKey.UpArrow:
                        --index;
                        break;
                    case ConsoleKey.DownArrow:
                        ++index;
                        break;
                    case ConsoleKey.Enter:
                        return index;
                }
                if (index < 0)
                    index = SelectedStaff.Posts.Count - 1;
                if (index > SelectedStaff.Posts.Count - 1)
                    index = 0;
                Console.Clear();
            } while (ch != ConsoleKey.Escape);
            return -1;
        }
        public void PrintAll()
        {
            if (!string.IsNullOrEmpty(SearchBy))
            {
                Console.WriteLine("Выполнен поиск по строке \"{0}\"", SearchBy);
            }
            table.Print(OrderedStaffs, SelectedStaff);
        }
    }
}
