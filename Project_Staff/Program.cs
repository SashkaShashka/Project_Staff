using System;
using System.Collections;
using System.Collections.Generic;

namespace Project_Staff
{
    class Program
    {
        readonly static PositionController positionController;
        readonly static StaffController staffController;

        static Program()
        {

            positionController = new PositionController(MocksFabric.MockPositions);
            staffController = new StaffController(positionController.Positions,MocksFabric.MockStaffs);
            positionController.AddStaff(staffController.Staffs);
        }
        static void Main(string[] args)
        {
            while (MainMenuInput());
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