namespace Adventure_rpg
{
    class systemInterface
    {

        public static void threadedWrite(string text, int delay)//Вывод текста с задержкой
        {
            foreach (char ch in text)
            {
                Console.Write(ch);
                Thread.Sleep(delay);
            }
        }

        public static void ColorWrite(string text, string coloredWord, ConsoleColor color) //Вывод слова с цветом
        {

            string[] normalParts = text.Split(new string[] { coloredWord }, StringSplitOptions.None);
            for (int i = 0; i < normalParts.Length; i++)
            {
                Console.ResetColor();

                Console.Write(normalParts[i]);
                if (i != normalParts.Length - 1)
                {
                    Console.ForegroundColor = color;

                    Console.Write(coloredWord);
                }
            }
        }

        public static void ColorWrite(string text, string coloredWord, ConsoleColor color, int delay) //Вывод слова с цветом И ЗАДЕРЖКОЙ
        {

            string[] normalParts = text.Split(new string[] { coloredWord }, StringSplitOptions.None);
            for (int i = 0; i < normalParts.Length; i++)
            {
                Console.ResetColor();
                foreach (char ch in normalParts[i])
                {
                    Thread.Sleep(delay);
                    Console.Write(ch);
                }

                if (i != normalParts.Length - 1)
                {
                    Console.ForegroundColor = color;

                    foreach (char ch1 in coloredWord)
                    {
                        Thread.Sleep(delay * 4);
                        Console.Write(ch1);
                    }
                }
            }
        }

        static void DrawInventoryMenu(string[] options, string[] slotAmount, int index, bool erase)
        {
            if (erase) ClearLines(options.Length);
            string prefix = " ";
            for (int i = 0; i < options.Length; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    prefix = "->";
                    Console.WriteLine($"{prefix} {options[i],-17} {slotAmount[i],-4}");
                    Console.ResetColor();
                }

                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    prefix = "  ";
                    Console.WriteLine($"{prefix} {options[i],-17} {slotAmount[i],-4}");
                    Console.ResetColor();
                }
            }
        }

        /// <summary>
        /// Метод для навигации в виде меню
        /// </summary>
        /// <param name="options"> варианты навигации (ДОЛЖНЫ БЫТЬ РАВНЫ КОЛИЧЕСТВУ МЕТОДОВ)</param>
        /// <param name="method1">Метод 1</param>
        /// <param name="method2">Метод 2</param>
        //public static void SelectMenu(string[] options, Delegate method1, Delegate method2)
        //{
        //    int currentIndex = 0;
        //    bool pressed = false;

        //   DrawMenu(options, currentIndex, false);
        //    while(pressed == false)
        //    {
        //        ConsoleKeyInfo keyPressed = Console.ReadKey();

        //        switch (keyPressed.Key)
        //        {
        //            case ConsoleKey.UpArrow:
        //                currentIndex--;
        //                break;
        //            case ConsoleKey.DownArrow:
        //                currentIndex++;
        //                break;
        //            default:
        //                break;
        //        }
        //        if(currentIndex > options.Length - 1)
        //        {
        //            currentIndex = 0;
        //        }
        //        else if(currentIndex < 0)
        //        {
        //            currentIndex = options.Length - 1;
        //        }

        //       DrawMenu(options, currentIndex, true);
        //    }
        //}


        public static void AddToInventory(InventorySystem inventory, string itemname, int amount, string errorMsg)//Функция добавки в инветарь(с проверкой)
        {
            Item item = ItemList.allItems[itemname];


            if (inventory.isCapableOfAdding(item, amount))
            {
                inventory.addItemToInventory(item, amount);
            }
            else
            {
                Console.WriteLine(errorMsg);

            }
        }

        public static void RemoveFromInventory(InventorySystem inventory, string itemname, int amount, string errorMsg)//Функция удаления из инветаря(с проверкой)
        {
            Item item = ItemList.allItems[itemname];
            if (inventory.isCapableOfDeleting(item, amount))
            {
                inventory.removeItemFromInventory(item, amount);
            }
            else
            {
                Console.WriteLine(errorMsg);

            }
        }

        public static void DrawInventory(InventorySystem inventory)//отрисовка инвентаря
        {
            Console.WriteLine("Ваш инвентарь:");
            for (int i = 0; i < inventory.GetMaxSlots(); i++)
            {
                string cellName = "Пусто";
                string cellDescription = "Пусто";
                int cellAmount = 0;
                if (inventory.IsCellExist(i))
                {
                    cellName = inventory.GetInventoryCell(i).thisItem.name;
                    cellDescription = inventory.GetInventoryCell(i).thisItem.description;
                    cellAmount = inventory.GetInventoryCell(i).Quantity;
                    Console.WriteLine($"[{cellName}],[{cellDescription}] в количестве [{cellAmount}].");
                }
                else
                {
                    Console.WriteLine($"В ячейке {i + 1} ничего нет.");
                }

            }
        }

        public static void InventorySelectMenu(InventorySystem inventory, bool canSell, string backAction, Game currentGame)//инвентарь с меню и кнопкой назад
        {
            Console.WriteLine(@"");
            string[] options = new string[inventory.GetMaxSlots() + 2];
            string[] slotAmount = new string[inventory.GetMaxSlots() + 2];
            int currentIndex = 0;
            bool selected = false;
            for (int i = 0; i < inventory.GetMaxSlots(); i++)
            {
                if (inventory.IsCellExist(i))
                {
                    options[i] = inventory.GetInventoryCell(i).thisItem.name;
                    if(inventory.GetInventoryCell(i).thisItem.maxSTACK > 1)
                    {
                        slotAmount[i] = " x" + inventory.GetInventoryCell(i).Quantity.ToString();
                    }
                    else
                    {
                        slotAmount[i] = "   ";
                    }
                    
                }
                else
                {
                    options[i] = "Пустая ячейка";
                    slotAmount[i] = "  ";
                }
            }

            options[inventory.GetMaxSlots() + 1] = "Назад";

            DrawInventoryMenu(options, slotAmount, currentIndex, false);
            while (selected == false)
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey();

                switch (keyPressed.Key)
                {
                    case ConsoleKey.UpArrow:
                        currentIndex--;
                        break;
                    case ConsoleKey.DownArrow:
                        currentIndex++;
                        break;
                    case ConsoleKey.Enter:
                        if (currentIndex < inventory.GetMaxSlots() && inventory.IsCellExist(currentIndex))
                        {
                            selected = true;
                            DisplaySelectedItemInfo(inventory, currentIndex, canSell, backAction, currentGame);
                            continue;
                        }
                        else if (currentIndex == inventory.GetMaxSlots() + 1)
                        {
                            selected = true;
                            switch (backAction)
                            {
                                case "Торговец":
                                    throw new Exception("Торговец ещё не сделан");
                                    break;
                                case "Информация":
                                    currentGame.Character.Greetings(currentGame);
                                    break;
                            }
                            continue;
                        }
                        else
                        {
                            break;
                        }
                        break;
                    default:
                        break;
                }
                if (currentIndex > options.Length - 1)
                {
                    currentIndex = 0;
                }
                else if (currentIndex < 0)
                {
                    currentIndex = options.Length - 1;
                }

                DrawInventoryMenu(options, slotAmount, currentIndex, true);


            }

        }


        public static void ClearLines(int lines)//стереть несколько линий(не весь экран)
        {
            for (int i = 1; i <= lines; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', 100));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
        }

        static void DrawMenu(string[] options, int index, bool erase)
        {
            string prefix = "";
            if (erase) ClearLines(options.Length);
            for (int i = 0; i < options.Length; i++)
            {

                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    prefix = "->";
                    Console.WriteLine($"{prefix} {options[i],-17}");
                    Console.ResetColor();
                }

                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    prefix = "  ";
                    Console.WriteLine($"{prefix} {options[i],-17}");
                    Console.ResetColor();
                }
            }
        }
        static void DisplaySelectedItemInfo(InventorySystem inventory, int index, bool canSell, string backAction, Game currentGame)
        {
            string[] foodOptions = { "Использовать", "Выбросить" };
            string[] armorAmdWeaponOptions = { "Экипировать", "Сравнить", "Выбросить" };
            if (canSell)
            {
                Array.Resize(ref foodOptions, foodOptions.Length + 1);
                Array.Resize(ref armorAmdWeaponOptions, armorAmdWeaponOptions.Length + 1);
                foodOptions[foodOptions.Length - 1] = "Продать";
                armorAmdWeaponOptions[armorAmdWeaponOptions.Length - 1] = "Продать";
            }
            Array.Resize(ref foodOptions, foodOptions.Length + 1);
            Array.Resize(ref armorAmdWeaponOptions, armorAmdWeaponOptions.Length + 1);
            foodOptions[foodOptions.Length - 1] = "Назад";
            armorAmdWeaponOptions[armorAmdWeaponOptions.Length - 1] = "Назад";



            Console.Clear();
            Console.WriteLine($" Ячейка [{index + 1}]");
            Console.WriteLine(" Предмет: " + inventory.GetInventoryCell(index).thisItem.name + ", " + inventory.GetInventoryCell(index).thisItem.type);
            Console.WriteLine("\n" + " О предмете:\n " + inventory.GetInventoryCell(index).thisItem.description);

            if (inventory.GetInventoryCell(index).thisItem.maxSTACK > 1)
            {
                Console.WriteLine(" Кол-во {0}/{1}.", inventory.GetInventoryCell(index).Quantity, inventory.GetInventoryCell(index).thisItem.maxSTACK);
            }
            switch (inventory.GetInventoryCell(index).thisItem.type)
            {
                case "Целебное зелье":
                case "Еда":
                    Console.WriteLine($" Восстанавливает [{HealConsumables.GetFoodHealInfo((HealConsumables)inventory.GetInventoryCell(index).thisItem)}] хп.");
                    Console.WriteLine($" У вас сейчас чч хп.");
                    Console.WriteLine("\n" + " Действия:");
                    switch (DrawMenuAndReturnAction(foodOptions))
                    {
                        case "Назад":
                            Console.Clear();
                            InventorySelectMenu(inventory, canSell, backAction, currentGame);
                            break;
                    }
                    break;
                case "Нагрудник":
                case "Перчатки":
                case "Шлем":
                case "Штаны":
                    Console.WriteLine($" Дает [{Armor.GetArmorDefence((Armor)inventory.GetInventoryCell(index).thisItem)}] брони.");
                    Console.WriteLine("\n" + " Действия:");
                    switch (DrawMenuAndReturnAction(armorAmdWeaponOptions))
                    {
                        case "Назад":
                            Console.Clear();
                            InventorySelectMenu(inventory, canSell, backAction, currentGame);
                            break;
                    }

                    break;
                case "Оружие":
                    Console.WriteLine($" Для класса [{Weapon.GetWeaponClass((Weapon)inventory.GetInventoryCell(index).thisItem)}].");
                    Console.WriteLine($" Наносит [{Weapon.GetWeaponDamage((Weapon)inventory.GetInventoryCell(index).thisItem)}] урона.");
                    Console.WriteLine("\n" + " Действия:");
                    switch (DrawMenuAndReturnAction(armorAmdWeaponOptions))
                    {
                        case "Назад":
                            Console.Clear();
                            InventorySelectMenu(inventory, canSell, backAction, currentGame);
                            break;
                    }
                    break;


            }





        }

        public static string DrawMenuAndReturnAction(string[] options)
        {
            int index = 0;
            bool selected = false;
            DrawMenu(options, index, false);
            while (selected == false)
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey();
                switch (keyPressed.Key)
                {
                    case ConsoleKey.UpArrow:
                        index--;
                        break;
                    case ConsoleKey.DownArrow:
                        index++;
                        break;
                    case ConsoleKey.Enter:
                        selected = true;
                        return options[index];
                    default:
                        break;
                }
                if (index > options.Length - 1)
                {
                    index = 0;
                }
                else if (index < 0)
                {
                    index = options.Length - 1;
                }
                DrawMenu(options, index, true);
            }
            return "";
        }
    }
}
