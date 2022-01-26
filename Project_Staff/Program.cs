using Project_Staff.BD;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Project_Staff
{
    class Program
    {
        readonly static PositionController positionController;
        readonly static StaffController staffController;
        const bool useMocks = false;

        static Program()
        {
            if (useMocks)
            {
                positionController = new PositionController(MocksFabric.MockPositions);
                staffController = new StaffController(positionController.Positions, MocksFabric.MockStaffs);
            }
            else
            {
                positionController = new PositionController(DbManager.GetPositions());
                staffController = new StaffController(positionController.Positions, DbManager.GetStaff(positionController.Positions));
            }
            positionController.AddStaff(staffController.Staffs);
        }
        static void Main(string[] args)
        {
            while (MainMenuInput()) ;

            if (!useMocks)
            {
                DbManager.UpdatePositions(positionController.Positions);
                DbManager.UpdateStaff(staffController.Staffs);
            }
        }

        static readonly Menu mainMenu = new Menu(new MenuItem[] {
            new MenuAction(ConsoleKey.Tab, "Перейти к списку должностей",() => { while (PositionMenuInput()); }),
            new MenuClose(ConsoleKey.Escape, "Выход"),
        });

        static bool MainMenuInput()
        {
            Console.Clear();
            staffController.Menu.Print();
            mainMenu.Print();
            staffController.PrintAll();
            var key = Console.ReadKey().Key;
            return mainMenu.Action(key) ? staffController.Menu.Action(key) : false;
        }

        static bool PositionMenuInput()
        {
            Console.Clear();
            positionController.Menu.Print();
            positionController.PrintAll();
            return positionController.Menu.Action(Console.ReadKey().Key);
        }
    }
}