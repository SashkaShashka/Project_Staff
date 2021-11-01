using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Staff
{
    class MocksFabric
    {
        public static List<Position> MockPositions => new List<Position>()
        {
            new Position("Менеджер по закупкам", "Отдел закупок", 40000),
            new Position("Менеджер по продажам", "Отдел продаж", 80000),
            new Position("Технический специалист", "Отдел закупок", 100000),
            new Position("Уборщица", "Отдел чистый", 20000),
            new Position("Генеральный директор", "Руководство", 200000),
         };
        public static List<Staff> MockStaffs => new List<Staff>()
        {
            new Staff("Андреев","Александр","Юрьевич", new DateTime(1999,12,15)),
            new Staff("Богатова","Юлия","Владимировна", new DateTime(1996,10,02)),
            new Staff("Андреев","Юрий","Васильевич", new DateTime(1970,10,13)),
         };
    }
}
