using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Staff
{
    class Table<T> where T : class
    {
        public Table(IEnumerable<TableColumn<T>> columns)
        {
            Columns = columns;
        }

        readonly IEnumerable<TableColumn<T>> Columns;

        public void Print(IEnumerable<T> rows, T selectedRow)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            foreach (var column in Columns)
            {
                Console.Write("{0} ", column.PrintTitle());
            }
            Console.WriteLine();
            Console.ResetColor();
            foreach (var row in rows)
            {
                if (row == selectedRow)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                foreach (var column in Columns)
                {
                    Console.Write("{0} ", column.PrintCell(row));
                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }
    }
    
}
