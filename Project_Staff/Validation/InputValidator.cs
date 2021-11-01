using Project_Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Staff
{
    static class InputValidator
    {
        public static string ReadString(string str, bool notEmpty=true)
        {
            string value;
            do
            {
                Console.Write("Введите {0}: ", str);
                value = Console.ReadLine();
            } while (notEmpty && value.Length == 0);
            return value;
        }

        public static DateTime ReadBirthDay()
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
        public static double ReadBet()
        {
            double bet;
            Console.WriteLine("Ставка должна быть числом от 0 до 1");
            Console.Write("Введите ставку: ");
            while (!double.TryParse(Console.ReadLine(), out bet) || bet <= 0 || bet > 1)
            {
                Console.WriteLine("Ставка должна быть числом от 0 до 1");
                Console.Write("Введите ставку: ");
            }
            return bet;
        }
        public static decimal ReadSalary()
        {
            decimal val=0;
            Console.WriteLine("Оклад должен быть не отрицательный в виде числа");
            Console.Write("Введите оклад: ");
            while (!decimal.TryParse(Console.ReadLine(), out val) || val < 0)
            {
                Console.WriteLine("Оклад должен быть не отрицательный в виде числа");
                Console.Write("Введите оклад: ");
            }
            return val;
        }
    }
}
