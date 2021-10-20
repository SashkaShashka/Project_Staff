using System;
using System.Collections;
using System.Collections.Generic;

namespace Project_Staff
{
    class Program
    {
        static void SelectPosition(PositionController positions,Staff staff) // временный метод заполнения должности
        //возможно потом растащу в другие классы
        {
            Console.Clear();
            ConsoleKey ch;
            int index = 0;
            do
            {
                Console.WriteLine("Нажмите ESC, чтобы завершить ввод ответов, или Enter для присвоения должности сотруднику.");
                Console.WriteLine("Навигация по меню стрелки Вверх и Вниз.");
                int i = 0;
                foreach (Position position in positions)
                {
                    if (i == index)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        for (int j = 0; j < 3; j++)
                        {
                            for (int k = 0; k < Console.WindowWidth; k++)
                                Console.Write(" ");
                            Console.CursorLeft = 0;
                            Console.CursorTop++;
                        }
                        Console.CursorTop = Console.CursorTop - 3;
                        Console.CursorLeft = 0;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(position.ToString());
                        Console.ResetColor();
                    }
                    else
                        Console.Write(position.ToStringPos());
                    i++;
                }
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
                        staff.AddPost(positions.FindByIndex(index));
                        break;
                }
                if (index < 0)
                    index = positions.Lenght - 1;
                if (index > positions.Lenght - 1)
                    index = 0;
                Console.Clear();
            } while (ch != ConsoleKey.Escape);
                

        }
        static void Main(string[] args)
        {

            PositionController positionController = new PositionController();
            StaffController staffController = new StaffController();
            Console.WriteLine("Введите должности: ");
            Console.WriteLine("Нажмите ESC, чтобы завершить ввод ответов, или любую другую клавишу, чтобы начать.");

            while (Console.ReadKey(true).Key != ConsoleKey.Escape)
            {
                Console.WriteLine();
                positionController.AddPosition();
                Console.WriteLine("Нажмите ESC, чтобы завершить ввод ответов, или любую другую клавишу, чтобы продолжить.");
            }


            Console.WriteLine("Введите работников: ");
            Console.WriteLine("Нажмите ESC, чтобы завершить ввод ответов, или любую другую клавишу, чтобы начать.");

            while (Console.ReadKey(true).Key != ConsoleKey.Escape)
            {
                Console.WriteLine();
                SelectPosition(positionController,staffController.AddStaff());
                Console.WriteLine("Нажмите ESC, чтобы завершить ввод ответов, или любую другую клавишу, чтобы продолжить.");
            }
            Console.WriteLine(staffController.ToString());

            // снизу закомментирована проверка реализованных методов - работают

            /*
             * staffController.AddStaff("Анлреев", "Юрий", "Васильевич", DateTime.Now, positionController.FindByIndex(3), 1);
            staffController.AddStaff("Анлреев", "Кожемяка", "Юрьевич", DateTime.Now, new Post(positionController.FindByIndex(2), 1));
            List<Post> posts = new List<Post>();
            posts.Add(new Post(positionController.FindByIndex(0), 1));
            Console.WriteLine(staffController.ToString());
            staffController.RemoveStaff(1);
            Console.WriteLine(staffController.ToString());
            staffController.Edit(2, null,"Александр");
            Console.WriteLine(staffController.ToString());
            staffController.AddPostOfStaff(0, positionController.FindByIndex(1), 1);
            staffController.AddPostOfStaff(0, positionController.FindByIndex(2), 1);
            staffController.AddPostOfStaff(0, positionController.FindByIndex(3), 1);
             */


        }
    }
}