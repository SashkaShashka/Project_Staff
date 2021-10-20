using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Staff
{
    class MocksFabric
    {
        public static IEnumerable<Position> MockProducts => new List<Position>()
        {
            new Position("Менеджер по закупкам", "Отдел закупок", 40000),
            new Position("Менеджер по продажам", "Отдел продаж", 80000),
            new Position("Технический специалист", "Отдел закупок", 100000),
            new Position("Уборщица", "Отдел чистый", 20000),
            new Position("Генеральный директор", "Руководство", 200000),
         };
    }
}
