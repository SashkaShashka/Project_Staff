using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Staff
{
    class StaffConsole
    {
        
        public static Position CreatePosition()
        {
            string title = ReadString("должность");
            string devision = ReadString("подразделение"); ;
            decimal salary = ReadSalary();
            return new Position(title, devision, salary);

        }
        public static Post CreatePost()
        {
            Position position = new Position(CreatePosition());
            double bet = ReadBet();
            return new Post(position, bet);
        }
        public static void CreateStaff()
        {
            uint serviceNumber = ReadServiceNumber();
            string surName = ReadString("Фамилию");
            string firstName = ReadString("Имя");
            string middleName = ReadString("Отчество");
            DateTime birthDay = ReadBirthDay();

            Staff staff = new Staff(serviceNumber, surName, firstName, middleName, birthDay);

            Console.WriteLine("Введите должности: ");
            Console.WriteLine("Нажмите ESC, чтобы завершить ввод ответов, или любую другую клавишу, чтобы начать.");

            while (Console.ReadKey(true).Key != ConsoleKey.Escape)
            {
                Console.WriteLine();
                Post post = CreatePost();
                staff.AddPost(post);
                Console.WriteLine("Нажмите ESC, чтобы завершить ввод ответов, или любую другую клавишу, чтобы продолжить.");
            }

            Console.WriteLine();
            Console.WriteLine("Сотрудник:");
            Console.WriteLine(staff.ToString());
            Console.Write("Оклад сотрудника :");
            Console.WriteLine(staff.CalculateSalary);
        }
        private static decimal ReadSalary()
        {
            decimal val;
            Console.WriteLine("Оклад должен быть не отрицательный в виде числа");
            Console.Write("Введите оклад: ");
            while (!decimal.TryParse(Console.ReadLine(), out val))
            {
                Console.WriteLine("Оклад должен быть не отрицательный в виде числа");
                Console.Write("Введите оклад: ");
            }
            return val;
        }
        private static uint ReadServiceNumber()
        {
            uint serviceNumber;
            Console.WriteLine("Номер должен быть ввиде целого неотрицательного числа");
            Console.Write("Введите номер: ");
            while (!uint.TryParse(Console.ReadLine(), out serviceNumber))
            {
                Console.WriteLine("Номер должен быть ввиде целого неотрицательного числа");
                Console.Write("Введите номер: ");
            }
            return serviceNumber;
        }
        
        private static double ReadBet()
        {
            double bet;
            Console.WriteLine("Ставка должна быть числом от 0 до 1");
            Console.Write("Введите ставку: ");
            while (!double.TryParse(Console.ReadLine(), out bet) || bet < 0 || bet > 1)
            {
                Console.WriteLine("Ставка должна быть числом от 0 до 1");
                Console.Write("Введите ставку: ");
            }
            return bet;
        }
        private static string ReadString(string str)
        {
            string value;
            do
            {
                Console.Write("Введите {0}: ", str);
                value = Console.ReadLine();
            } while (value.Length == 0);
            return value;
        }
        private static DateTime ReadBirthDay()
        {
            string[] formats = { "dd.MM.yyyy" };
            DateTime parsedDate;
            do
            {
                Console.Write("Введите время и дату события в формате dd.mm.yyyy: ");
            } while (!DateTime.TryParseExact(Console.ReadLine(), formats, null,
                               System.Globalization.DateTimeStyles.AllowWhiteSpaces |
                               System.Globalization.DateTimeStyles.AdjustToUniversal,
                               out parsedDate));
            return parsedDate;
        }

    }
}
