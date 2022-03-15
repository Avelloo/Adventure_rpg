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

        static void DrawInventoryMenu(string[] options, string[] slotAmount, int index, bool erase, bool trade)
        {
            if (erase) ClearLines(options.Length);
            string[] postfix = new string[options.Length];
            if (trade)
            {
                for (int i = 0; i < postfix.Length; i++)
                {
                    if(ItemList.getItemByName(ItemList.allItems, options[i]) != null)
                    {
                        postfix[i] = $"Цена: [{ItemList.getItemByName(ItemList.allItems, options[i]).buyPrice.ToString(),2}]";
                    }
                    
                }
            }
            string prefix = " ";
            for (int i = 0; i < options.Length; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    prefix = "->";
                    Console.WriteLine($"{prefix} {options[i],-21} {slotAmount[i],-4} {postfix[i],-8}");
                    Console.ResetColor();
                }

                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    prefix = "  ";
                    Console.WriteLine($"{prefix} {options[i],-21} {slotAmount[i],-4} {postfix[i],-8}");
                    Console.ResetColor();
                }
            }
        }



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

        public static void InventorySelectMenu(InventorySystem inventory, ArmorAndWeapon armorAndWeapon, bool canSell, string backAction, Game currentGame)//инвентарь с меню и кнопкой назад
        {
            Console.WriteLine(@"");
            string[] options = new string[inventory.GetMaxSlots() + 2];
            string[] slotAmount = new string[inventory.GetMaxSlots() + 2];
            int currentIndex = 0;
            bool selected = false;
            for (int i = 0; i < inventory.GetMaxSlots() - 1; i++)
            {
                if (inventory.IsCellExist(i))
                {
                    options[i] = inventory.GetInventoryCell(i).thisItem.name;
                    if (inventory.GetInventoryCell(i).thisItem.maxSTACK > 1)
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

            DrawInventoryMenu(options, slotAmount, currentIndex, false,false);
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
                            DisplaySelectedItemInfo(inventory, armorAndWeapon, currentIndex, canSell, backAction, currentGame);
                            continue;
                        }
                        else if (currentIndex == inventory.GetMaxSlots() + 1)
                        {
                            selected = true;
                            switch (backAction)
                            {
                                case "Торговец":
                                    currentGame.TraderOptions();
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

                DrawInventoryMenu(options, slotAmount, currentIndex, true,false);


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
                    Console.WriteLine($"  {prefix} {options[i],-17}");
                    Console.ResetColor();
                }

                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    prefix = "  ";
                    Console.WriteLine($"  {prefix} {options[i],-17}");
                    Console.ResetColor();
                }
            }
        }
        /// <summary>
        /// Информация о предмете в инвентаре и действия с ним
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="armorAndWeapon"></param>
        /// <param name="index"></param>
        /// <param name="canSell"></param>
        /// <param name="backAction"></param>
        /// <param name="currentGame"></param>
        static void DisplaySelectedItemInfo(InventorySystem inventory, ArmorAndWeapon armorAndWeapon, int index, bool canSell, string backAction, Game currentGame)
        {
            string[] foodOptions = { "Использовать"};
            string[] armorAmdWeaponOptions = { "Экипировать", "Сравнить"};
            if (canSell)
            {
                Array.Resize(ref foodOptions, foodOptions.Length + 1);
                Array.Resize(ref armorAmdWeaponOptions, armorAmdWeaponOptions.Length + 1);
                foodOptions[foodOptions.Length - 1] = "Продать";
                armorAmdWeaponOptions[armorAmdWeaponOptions.Length - 1] = "Продать";
            }
            else
            {
                Array.Resize(ref foodOptions, foodOptions.Length + 1);
                Array.Resize(ref armorAmdWeaponOptions, armorAmdWeaponOptions.Length + 1);
                foodOptions[foodOptions.Length - 1] = "Выбросить";
                armorAmdWeaponOptions[armorAmdWeaponOptions.Length - 1] = "Выбросить";
            }
            Array.Resize(ref foodOptions, foodOptions.Length + 1);
            Array.Resize(ref armorAmdWeaponOptions, armorAmdWeaponOptions.Length + 1);
            foodOptions[foodOptions.Length - 1] = "Назад";
            armorAmdWeaponOptions[armorAmdWeaponOptions.Length - 1] = "Назад";



            Console.Clear();
            Console.WriteLine($" Ячейка [{index + 1}]");
            Console.WriteLine(" Предмет: " + inventory.GetInventoryCell(index).thisItem.name + ", " + inventory.GetInventoryCell(index).thisItem.type);
            Console.WriteLine("\n" + " О предмете:\n " + inventory.GetInventoryCell(index).thisItem.description);
            Console.WriteLine("\n" + $" Цена продажи: " + inventory.GetInventoryCell(index).thisItem.sellPrice);

            if (inventory.GetInventoryCell(index).thisItem.maxSTACK > 1)
            {
                Console.WriteLine(" Кол-во {0}/{1}.", inventory.GetInventoryCell(index).Quantity, inventory.GetInventoryCell(index).thisItem.maxSTACK);
            }
            switch (inventory.GetInventoryCell(index).thisItem.type)
            {
                case "Целебное зелье":
                case "Еда":
                    Console.WriteLine($" Восстанавливает [{HealConsumables.GetFoodHealInfo((HealConsumables)inventory.GetInventoryCell(index).thisItem)}] хп.");
                    systemInterface.ColorWrite($" У вас сейчас {currentGame.Character.CurrentHealth}/{currentGame.Character.MaxHealth} хп.",
                                                                 currentGame.Character.CurrentHealth.ToString(), ConsoleColor.DarkRed);
                    Console.WriteLine("\n" + " Действия:");
                    switch (DrawMenuAndReturnAction(foodOptions))
                    {
                        case "Использовать":
                            TryToHeal(inventory, armorAndWeapon, currentGame, index, canSell, backAction, "У вас максимальное хп!");
                            break;
                        case "Выбросить":
                            ItemRemoveAction(inventory, index, currentGame, armorAndWeapon, "Удалить", canSell, backAction);
                            break;
                        case "Продать":
                            ItemRemoveAction(inventory, index, currentGame, armorAndWeapon, "Продать", canSell, backAction);
                            break;
                        case "Назад":
                            Console.Clear();
                            InventorySelectMenu(inventory, armorAndWeapon, canSell, backAction, currentGame);
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
                        case "Экипировать":
                            armorAndWeapon.TryToWear(inventory.GetInventoryCell(index).thisItem, currentGame, inventory, "У вас в инвентаре недостаточно места!");
                            Console.Clear();
                            InventorySelectMenu(inventory, armorAndWeapon, canSell, backAction, currentGame);
                            break;
                        case "Сравнить":
                            Console.Clear();
                            CompareWearings(inventory.GetInventoryCell(index).thisItem, armorAndWeapon);
                            InventorySelectMenu(inventory, armorAndWeapon, canSell, backAction, currentGame);
                            break;
                        case "Выбросить":
                            ItemRemoveAction(inventory, index, currentGame, armorAndWeapon, "Удалить", canSell, backAction);
                            break;
                        case "Продать":
                            ItemRemoveAction(inventory, index, currentGame, armorAndWeapon, "Продать", canSell, backAction);
                            break;
                        case "Назад":
                            Console.Clear();
                            InventorySelectMenu(inventory, armorAndWeapon, canSell, backAction, currentGame);
                            break;
                    }

                    break;
                case "Оружие":
                    Console.WriteLine($" Для класса [{Weapon.GetWeaponClass((Weapon)inventory.GetInventoryCell(index).thisItem)}].");
                    Console.WriteLine($" Наносит [{Weapon.GetWeaponDamage((Weapon)inventory.GetInventoryCell(index).thisItem)}] урона.");
                    Console.WriteLine("\n" + " Действия:");
                    switch (DrawMenuAndReturnAction(armorAmdWeaponOptions))
                    {
                        case "Экипировать":
                            armorAndWeapon.TryToWear(inventory.GetInventoryCell(index).thisItem, currentGame, inventory, "У вас в инвентаре недостаточно места!");
                            Console.Clear();
                            InventorySelectMenu(inventory, armorAndWeapon, canSell, backAction, currentGame);
                            break;
                        case "Сравнить":
                            Console.Clear();
                            CompareWearings(inventory.GetInventoryCell(index).thisItem, armorAndWeapon);
                            InventorySelectMenu(inventory, armorAndWeapon, canSell, backAction, currentGame);
                            break;
                        case "Выбросить":
                            ItemRemoveAction(inventory, index, currentGame, armorAndWeapon, "Удалить", canSell, backAction);
                            break;
                        case "Продать":
                            ItemRemoveAction(inventory, index, currentGame, armorAndWeapon, "Продать", canSell, backAction);
                            break;
                        case "Назад":
                            Console.Clear();
                            InventorySelectMenu(inventory, armorAndWeapon, canSell, backAction, currentGame);
                            break;
                    }
                    break;


            }





        }
        /// <summary>
        /// Информация о выбранном предмете снаряжения и действия с ним
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="armorAndWeapon"></param>
        /// <param name="index"></param>
        /// <param name="canSell"></param>
        /// <param name="backAction"></param>
        /// <param name="currentGame"></param>
        static void DisplaySelectedWearingInfo(InventorySystem inventory, ArmorAndWeapon armorAndWeapon, int index, bool canSell, string backAction, Game currentGame)
        {
            string[] armorAmdWeaponOptions = { "Снять" };
            if (canSell)
            {
                Array.Resize(ref armorAmdWeaponOptions, armorAmdWeaponOptions.Length + 1);
                armorAmdWeaponOptions[armorAmdWeaponOptions.Length - 1] = "Продать";
            }
            Array.Resize(ref armorAmdWeaponOptions, armorAmdWeaponOptions.Length + 1);
            armorAmdWeaponOptions[armorAmdWeaponOptions.Length - 1] = "Назад";



            Console.Clear();

            Console.WriteLine($" Вы смотрите на: [{armorAndWeapon.secondatyInventory[index].type}]");
            Console.WriteLine(" Предмет: " + armorAndWeapon.secondatyInventory[index].name);
            Console.WriteLine("\n" + " О предмете:\n " + armorAndWeapon.secondatyInventory[index].description);

            switch (armorAndWeapon.secondatyInventory[index].type)
            {
                case "Нагрудник":
                case "Перчатки":
                case "Шлем":
                case "Штаны":
                    Console.WriteLine($" Дает [{Armor.GetArmorDefence((Armor)armorAndWeapon.secondatyInventory[index])}] брони.");
                    Console.WriteLine("\n" + " Действия:");
                    switch (DrawMenuAndReturnAction(armorAmdWeaponOptions))
                    {
                        case "Снять":
                            armorAndWeapon.TryToUnWear(inventory, index, "У вас в инвентаре недостаточно места!");
                            Console.Clear();
                            InventorySelectMenu(inventory, armorAndWeapon, canSell, backAction, currentGame);
                            break;
                        case "Назад":
                            Console.Clear();
                            InventorySelectMenu(inventory, armorAndWeapon, canSell, backAction, currentGame);
                            break;
                    }

                    break;
                case "Оружие":
                    Console.WriteLine($" Для класса [{Weapon.GetWeaponClass((Weapon)armorAndWeapon.secondatyInventory[index])}].");
                    Console.WriteLine($" Наносит [{Weapon.GetWeaponDamage((Weapon)armorAndWeapon.secondatyInventory[index])}] урона.");
                    Console.WriteLine("\n" + " Действия:");
                    switch (DrawMenuAndReturnAction(armorAmdWeaponOptions))
                    {
                        case "Снять":
                            armorAndWeapon.TryToUnWear(inventory, index, "У вас в инвентаре недостаточно места!");
                            Console.Clear();
                            DisplayWearingSelectMenu(inventory, armorAndWeapon, backAction, currentGame);
                            break;
                        case "Назад":
                            Console.Clear();
                            DisplayWearingSelectMenu(inventory, armorAndWeapon, backAction, currentGame);
                            break;
                    }
                    break;


            }





        }
        /// <summary>
        /// Отображение списка снаряжения
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="armorAndWeapon"></param>
        /// <param name="backAction"></param>
        /// <param name="currentGame"></param>
        /// <exception cref="Exception"></exception>
        public static void DisplayWearingSelectMenu(InventorySystem inventory, ArmorAndWeapon armorAndWeapon, string backAction, Game currentGame)
        {
            int currentIndex = 0;
            bool selected = false;
            bool canSell = false;

            string[] options = new string[armorAndWeapon.secondatyInventory.Length + 1];
            for (int i = 0; i < armorAndWeapon.secondatyInventory.Length; i++)
            {
                if (armorAndWeapon.secondatyInventory[i] != null)
                {
                    options[i] = armorAndWeapon.secondatyInventory[i].type + ": " + armorAndWeapon.secondatyInventory[i].name;

                }
                else
                {
                    switch (i)
                    {
                        case 0:
                            options[0] = "Оружие: нет";
                            break;
                        case 1:
                            options[1] = "Шлем: нет";
                            break;
                        case 2:
                            options[2] = "Нагрудник: нет";
                            break;
                        case 3:
                            options[3] = "Штаны: нет";
                            break;
                        case 4:
                            options[4] = "Перчатки: нет";
                            break;
                    }


                }
            }

            options[armorAndWeapon.secondatyInventory.Length] = "Назад";
            Console.WriteLine();
            DrawWearingsMenu(options, currentIndex, false);
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
                        if (currentIndex < armorAndWeapon.secondatyInventory.Length && armorAndWeapon.secondatyInventory[currentIndex] != null)
                        {
                            selected = true;
                            DisplaySelectedWearingInfo(inventory, armorAndWeapon, currentIndex, canSell, backAction, currentGame);
                            continue;
                        }
                        else if (currentIndex == armorAndWeapon.secondatyInventory.Length)
                        {
                            selected = true;
                            switch (backAction)
                            {
                                case "Торговец":
                                    currentGame.TraderOptions();
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
                DrawWearingsMenu(options, currentIndex, true);
            }
        }
        /// <summary>
        /// Отрисовка меню снаряжения
        /// </summary>
        /// <param name="options"></param>
        /// <param name="index"></param>
        /// <param name="erase"></param>
        static void DrawWearingsMenu(string[] options, int index, bool erase)
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
                    Console.WriteLine($"{prefix,-3}{options[i],-17}");
                    Console.ResetColor();
                }

                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    prefix = "  ";
                    Console.WriteLine($"{prefix,-3}{options[i],-17}");
                    Console.ResetColor();
                }
            }
        }
        /// <summary>
        /// Сравнение предмета с надетым снаряжением
        /// </summary>
        /// <param name="item"></param>
        /// <param name="armorAndWeapon"></param>
        static void CompareWearings(Item item, ArmorAndWeapon armorAndWeapon)
        {
            int compareIndex = 0;
            string name1 = item.name;
            string name2;
            int compareInfo1 = 0;
            int compareInfo2 = 0;
            switch (item.type)
            {
                case "Нагрудник":
                    compareIndex = 2;
                    break;
                case "Перчатки":
                    compareIndex = 4;
                    break;
                case "Шлем":
                    compareIndex = 1;
                    break;
                case "Штаны":
                    compareIndex = 3;
                    break;

                case "Оружие":
                    compareIndex = 0;
                    break;
            }
            if (item.type == "Оружие") compareInfo1 = Weapon.GetWeaponDamage((Weapon)item);
            else compareInfo1 = Armor.GetArmorDefence((Armor)item);
            if (armorAndWeapon.secondatyInventory[compareIndex] != null)
            {
                name2 = armorAndWeapon.secondatyInventory[compareIndex].name;
                if (compareIndex == 0) compareInfo2 = Weapon.GetWeaponDamage((Weapon)armorAndWeapon.secondatyInventory[compareIndex]);
                else compareInfo2 = Armor.GetArmorDefence((Armor)armorAndWeapon.secondatyInventory[compareIndex]);
            }
            else
            {
                name2 = "Нет";
                compareInfo2 = 0;
            }
            Console.WriteLine("\nСравнение:\n");
            Console.Write("Название: ");
            Console.Write($"[Инвентарь]:{name1,-15} [Надето]:{name2,-15}");
            Console.WriteLine();
            if (item.type == "Оружие")
            {
                Console.Write($"Урон:     [Инвентарь]:{compareInfo1,-15} [Надето]:{compareInfo2,-15}");
            }
            else
            {

                Console.Write($"Защита:   [Инвентарь]:{compareInfo1,-15} [Надето]:{compareInfo2,-15}");
            }

            Console.WriteLine("\n\n\nНажмите любую клавишу, чтобы продолжить");

            Console.ReadKey();
            Console.Clear();

        }
        /// <summary>
        /// Отрисовка меню и воврат string выбор
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Попытка лечения
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="armorAndWeapon"></param>
        /// <param name="currentGame"></param>
        /// <param name="index"></param>
        /// <param name="canSell"></param>
        /// <param name="backAction"></param>
        /// <param name="errorMsg"></param>
        public static void TryToHeal(InventorySystem inventory, ArmorAndWeapon armorAndWeapon, Game currentGame, int index, bool canSell, string backAction, string errorMsg)
        {
            if (currentGame.Character.CurrentHealth < currentGame.Character.MaxHealth)
            {
                int tempHP = currentGame.Character.CurrentHealth;
                currentGame.Character.CurrentHealth += HealConsumables.GetFoodHealInfo((HealConsumables)inventory.GetInventoryCell(index).thisItem);
                if (currentGame.Character.CurrentHealth > currentGame.Character.MaxHealth) currentGame.Character.CurrentHealth = currentGame.Character.MaxHealth;


                if (inventory.GetInventoryCell(index).Quantity > 1 &&

                    (inventory.GetInventoryCell(index).thisItem.type == "Еда" ||
                    inventory.GetInventoryCell(index).thisItem.type == "Лечебное зелье")
                    )
                {
                    inventory.removeItemFromInventory(inventory.GetInventoryCell(index).thisItem, 1);
                    Console.Clear();
                    Console.WriteLine($"\n\nВы восстановили [{currentGame.Character.CurrentHealth - tempHP}] хп.");
                    Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить");
                    Console.ReadLine();
                    Console.Clear();
                    DisplaySelectedItemInfo(inventory, armorAndWeapon, index, canSell, backAction, currentGame);
                }
                else
                {
                    inventory.removeItemFromInventory(inventory.GetInventoryCell(index).thisItem, 1);
                    Console.Clear();
                    Console.WriteLine($"\n\nВы восстановили [{currentGame.Character.CurrentHealth - tempHP}] хп.");
                    Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить");
                    Console.ReadLine();
                    Console.Clear();
                    InventorySelectMenu(inventory, armorAndWeapon, canSell, backAction, currentGame);
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine(errorMsg);
                Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить");
                Console.ReadLine();
                Console.Clear();
                DisplaySelectedItemInfo(inventory, armorAndWeapon, index, canSell, backAction, currentGame);

            }
        }

        /// <summary>
        /// Информация о предмете в инветаре торговца
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="index"></param>
        /// <param name="backAction"></param>
        /// <param name="currentGame"></param>
        /// <param name="armorAndWeapon"></param>
        /// <param name="trader"></param>
        static void TraderDisplaySelectedItemInfo(InventorySystem inventory, int index, string backAction, Game currentGame, ArmorAndWeapon armorAndWeapon, Trader trader)
        {
           
            string[] foodOptions = { "Купить" };
            string[] armorAmdWeaponOptions = { "Купить", "Сравнить" };

            Array.Resize(ref foodOptions, foodOptions.Length + 1);
            Array.Resize(ref armorAmdWeaponOptions, armorAmdWeaponOptions.Length + 1);
            foodOptions[foodOptions.Length - 1] = "Назад";
            armorAmdWeaponOptions[armorAmdWeaponOptions.Length - 1] = "Назад";



            Console.Clear();
            Console.WriteLine($" Ячейка [{index + 1}]");
            Console.WriteLine(" Предмет: " + inventory.GetInventoryCell(index).thisItem.name + ", " + inventory.GetInventoryCell(index).thisItem.type);
            Console.WriteLine("\n" + " О предмете:\n " + inventory.GetInventoryCell(index).thisItem.description);
            Console.WriteLine("\n" + $" Цена покупки: " + inventory.GetInventoryCell(index).thisItem.buyPrice);

            if (inventory.GetInventoryCell(index).thisItem.maxSTACK > 1)
            {
                Console.WriteLine(" Кол-во {0}/{1}.", inventory.GetInventoryCell(index).Quantity, inventory.GetInventoryCell(index).thisItem.maxSTACK);
            }
            switch (inventory.GetInventoryCell(index).thisItem.type)
            {
                case "Целебное зелье":
                case "Еда":
                    Console.WriteLine($" Восстанавливает [{HealConsumables.GetFoodHealInfo((HealConsumables)inventory.GetInventoryCell(index).thisItem)}] хп.");
                    Console.WriteLine();
                    Console.WriteLine("\n" + " Действия:");
                    switch (DrawMenuAndReturnAction(foodOptions))
                    {
                        case "Купить":
                            TraderItemRemoveAction(inventory, trader, index, currentGame, armorAndWeapon, backAction);
                            break;
                        case "Назад":
                            Console.Clear();
                            TraderInventorySelectMenu(inventory, backAction, currentGame, armorAndWeapon, trader);
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
                        case "Сравнить":
                            Console.Clear();
                            CompareWearings(inventory.GetInventoryCell(index).thisItem, armorAndWeapon);
                            TraderInventorySelectMenu(inventory, backAction, currentGame, armorAndWeapon, trader);
                            break;
                        case "Купить":
                            TraderItemRemoveAction(inventory, trader, index, currentGame, armorAndWeapon, backAction);
                            break;
                        case "Назад":
                            Console.Clear();
                            TraderInventorySelectMenu(inventory, backAction, currentGame, armorAndWeapon,trader);
                            break;
                    }

                    break;
                case "Оружие":
                    Console.WriteLine($" Для класса [{Weapon.GetWeaponClass((Weapon)inventory.GetInventoryCell(index).thisItem)}].");
                    Console.WriteLine($" Наносит [{Weapon.GetWeaponDamage((Weapon)inventory.GetInventoryCell(index).thisItem)}] урона.");
                    Console.WriteLine("\n" + " Действия:");
                    switch (DrawMenuAndReturnAction(armorAmdWeaponOptions))
                    {
                        case "Сравнить":
                            Console.Clear();
                            CompareWearings(inventory.GetInventoryCell(index).thisItem, armorAndWeapon);
                            TraderInventorySelectMenu(inventory, backAction, currentGame, armorAndWeapon,trader);
                            break;
                        case "Купить":
                            TraderItemRemoveAction(inventory, trader, index, currentGame, armorAndWeapon,backAction);
                            break;
                        case "Назад":
                            Console.Clear();
                            TraderInventorySelectMenu(inventory, backAction, currentGame, armorAndWeapon,trader);
                            break;
                    }
                    break;


            }





        }

        /// <summary>
        ///  Отобразить инвентарь торговца
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="backAction"></param>
        /// <param name="currentGame"></param>
        /// <param name="armorAndWeapon"></param>
        /// <param name="trader"></param>
        public static void TraderInventorySelectMenu(InventorySystem inventory, string backAction, Game currentGame, ArmorAndWeapon armorAndWeapon, Trader trader)
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
                    if (inventory.GetInventoryCell(i).thisItem.maxSTACK > 1)
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

            DrawInventoryMenu(options, slotAmount, currentIndex, false,true);
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
                            TraderDisplaySelectedItemInfo(inventory, currentIndex, backAction, currentGame, armorAndWeapon,trader);
                            continue;
                        }
                        else if (currentIndex == inventory.GetMaxSlots() + 1)
                        {
                            selected = true;
                            switch (backAction)
                            {
                                case "Торговец":
                                    Console.Clear();
                                    trader.DisplayItems(currentGame, armorAndWeapon);
                                    break;
                                case "Информация":
                                    Console.Clear();
                                    currentGame.Character.Greetings(currentGame);
                                    break;
                            }
                            continue;
                        }
                        else
                        {
                            break;
                        }
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

                DrawInventoryMenu(options, slotAmount, currentIndex, true,true);


            }

        }

        /// <summary>
        /// Действие с выбранным предметом в инвентаре(удалить\продать)
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="index"></param>
        /// <param name="currentGame"></param>
        /// <param name="armorAndWeapon"></param>
        /// <param name="action"></param>
        /// <param name="canSell"></param>
        /// <param name="backAction"></param>
        static void ItemRemoveAction(InventorySystem inventory,int index, Game currentGame, ArmorAndWeapon armorAndWeapon, string action, bool canSell, string backAction)
        {
            int amountToDelete = 0;
            if (inventory.GetInventoryCell(index).Quantity > 1)
            {
                Console.WriteLine($"Введите количество, которое хотите {action.ToLower()}.\n(Всего {inventory.GetInventoryCell(index).thisItem.name} доступно: {inventory.CountItem(inventory.GetInventoryCell(index).thisItem)}.)");
                if (!Int32.TryParse(Console.ReadLine(), out amountToDelete))
                {
                    Console.Clear();
                    InventorySelectMenu(inventory, armorAndWeapon, canSell, backAction, currentGame);
                }
            }
            else
            {
                amountToDelete = 1;
            }
            switch (action)
            {
                case "Продать":
                    if (amountToDelete <= inventory.CountItem(inventory.GetInventoryCell(index).thisItem) && amountToDelete > 0)
                    {
                        int plusMoney = amountToDelete * inventory.GetInventoryCell(index).thisItem.sellPrice;
                        currentGame.Character.Money += plusMoney;
                        Console.Clear();
                        if(amountToDelete > 1)
                        {
                            Console.WriteLine($"Вы продали [{inventory.GetInventoryCell(index).thisItem.name}] в кол-ве {amountToDelete}\nи получили за это [{plusMoney}] монет.");
                        }
                        else
                        {
                            Console.WriteLine($"Вы продали [{inventory.GetInventoryCell(index).thisItem.name}] за [{plusMoney}] монет.");
                        }
                        
                        Console.WriteLine($"Нажмите любую клавишу, чтобы продолжить.");
                        Console.ReadKey();
                        inventory.removeItemFromInventory(inventory.GetInventoryCell(index).thisItem, amountToDelete);
                        Console.Clear();
                        InventorySelectMenu(inventory, armorAndWeapon, canSell, backAction, currentGame);
                    }
                    else
                    {
                        Console.WriteLine($"Не удалось {action.ToLower()} предмет. Нажмите любую клавишу, чтобы продолжить.");
                        Console.ReadKey();
                        Console.Clear();
                        InventorySelectMenu(inventory, armorAndWeapon, canSell, backAction, currentGame);
                    }
                    break;

                case "Удалить":
                    if (amountToDelete <= inventory.CountItem(inventory.GetInventoryCell(index).thisItem) && amountToDelete > 0)
                    {
                        inventory.removeItemFromInventory(inventory.GetInventoryCell(index).thisItem, amountToDelete);
                        Console.Clear();
                        InventorySelectMenu(inventory, armorAndWeapon, canSell, backAction, currentGame);
                    }
                    else
                    {
                        Console.WriteLine("Не удалось удалить предмет. Нажмите любую клавишу, чтобы продолжить.");
                        Console.ReadKey();
                        Console.Clear();
                        InventorySelectMenu(inventory, armorAndWeapon, canSell, backAction, currentGame);
                    }
                    break;
            }
            
        }
        /// <summary>
        /// Действие с выбранным предметом в инвентаре торговца
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="trader"></param>
        /// <param name="index"></param>
        /// <param name="currentGame"></param>
        /// <param name="armorAndWeapon"></param>
        /// <param name="backAction"></param>
        static void TraderItemRemoveAction(InventorySystem inventory,Trader trader, int index, Game currentGame, ArmorAndWeapon armorAndWeapon, string backAction)
        {
            int amountToDelete = 0;
            if (inventory.GetInventoryCell(index).Quantity > 1)
            {
                
                Console.WriteLine($"Введите количество, которое хотите купить.\n(Всего {inventory.GetInventoryCell(index).thisItem.name} доступно: {inventory.CountItem(inventory.GetInventoryCell(index).thisItem)}.)");
                if (!Int32.TryParse(Console.ReadLine(), out amountToDelete))
                {
                    Console.Clear();
                    TraderInventorySelectMenu(inventory, backAction, currentGame, armorAndWeapon, trader);
                }
            }
            else
            {
                amountToDelete = 1;
            }


            if (amountToDelete <= inventory.CountItem(inventory.GetInventoryCell(index).thisItem)
                && amountToDelete > 0
                && currentGame.Character.Money >= (amountToDelete * inventory.GetInventoryCell(index).thisItem.buyPrice)
                && currentGame.Character.characterInventory.isCapableOfAdding(inventory.GetInventoryCell(index).thisItem, amountToDelete))
            {

                Item itemToSell = inventory.GetInventoryCell(index).thisItem;
                Console.Clear();
                int minusMoney = amountToDelete * itemToSell.buyPrice;
                if (amountToDelete > 1)
                {
                    Console.WriteLine($"Вы купили [{inventory.GetInventoryCell(index).thisItem.name}] в кол-ве {amountToDelete}\n и потратили на это [{minusMoney}] монет.");
                }
                else
                {
                    Console.WriteLine($"Вы купили [{inventory.GetInventoryCell(index).thisItem.name}] за [{minusMoney}] монет.");
                }
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                Console.ReadKey();

                currentGame.Character.Money -= minusMoney;
                currentGame.Character.characterInventory.addItemToInventory(itemToSell, amountToDelete);
                inventory.removeItemFromInventory(itemToSell, amountToDelete);
                Console.Clear();
                TraderInventorySelectMenu(inventory, backAction, currentGame, armorAndWeapon, trader);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Не удалось купить предмет. Нажмите любую клавишу, чтобы продолжить.");
                Console.ReadKey();
                Console.Clear();
                TraderInventorySelectMenu(inventory, backAction, currentGame, armorAndWeapon, trader);
            }
        }

        static public void DisplayShortCharInfo(Character character)
        {
            systemInterface.ColorWrite(" o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o\n", "o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o", ConsoleColor.DarkGray);
            systemInterface.ColorWrite(" |", " |", ConsoleColor.DarkGray);
            systemInterface.ColorWrite($"{character.Name,14}", character.Name, ConsoleColor.Magenta);
            Console.Write(" . ");
            Console.Write("[");
            DisplayHealthBar(character);
            Console.Write($"] {character.CurrentHealth,3}/{character.MaxHealth,-3} ХП");
            Console.Write(" . ");
            systemInterface.ColorWrite($"АТК:{character.CurrentAttack,-2}", character.CurrentAttack.ToString(), ConsoleColor.DarkGreen);
            Console.Write(" . ");
            systemInterface.ColorWrite($"ЗАЩ:{character.CurrentDefence,-2}", character.CurrentDefence.ToString(), ConsoleColor.DarkGreen);
            Console.Write(" . ");
            systemInterface.ColorWrite($"УР:{character.CharLVL,-2}", character.CharLVL.ToString(), ConsoleColor.DarkGreen);
            Console.Write(" . ");
            systemInterface.ColorWrite($"До след УР:{character.ExpToNextLvl,-4}", character.ExpToNextLvl.ToString(), ConsoleColor.Green);
            Console.Write(" . ");
            systemInterface.ColorWrite($"ДЕНЬГ:{character.Money,-4}", character.Money.ToString(), ConsoleColor.DarkYellow);
            systemInterface.ColorWrite("|", "|", ConsoleColor.DarkGray);
            Console.WriteLine();
            systemInterface.ColorWrite(" o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o\n", "o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o", ConsoleColor.DarkGray);
            Console.WriteLine();
        }

        static public void DisplayHealthBar(Character character)
        {
            double percentage = ((double)character.CurrentHealth / (double)character.MaxHealth);
            percentage = Math.Round(percentage, 2);
            percentage *= 100;
            for(int i = 0; i < 10; i++)
            {
                if(percentage > i * 10)
                {
                    systemInterface.ColorWrite("█", "█", ConsoleColor.Red);
                }
                else if(percentage < i * 10 && percentage > (i-1) * 10)
                {
                    systemInterface.ColorWrite("█", "█", ConsoleColor.DarkRed);
                }
                else
                {
                    Console.Write("_");
                }
            }
        }

        public static int GetRandomNumberInInterval(int start, int end)
        {
            Random rand = new Random();
            return rand.Next(start,end);
        }
    }

}

