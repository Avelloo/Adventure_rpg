namespace Adventure_rpg
{

    internal class Character
    {
      
        string name = "";
        string proffesion = "";
        int strength = 1;
        int agility = 1;
        int intelligence = 1;
        int currentExp = 0;
        int charLVL = 1;
        int money = 0;

        public InventorySystem inventory = new InventorySystem();



        bool physicWeapon = false;
        bool magicWeapon = false;
        bool bowWeapon = false;

        int startPoints = 1;








        public void CreateCharacter() //создание персонажа
        {
            beginning:
            Console.Clear();
            Console.WriteLine("Для начала нужно ввести имя. Сам(0) или рандомно(1)?");
            switch (Console.ReadLine())
            {
                case "0":
                    Console.WriteLine("\nВведите имя: ");
                    name = Console.ReadLine();
                    break;
                case "1":
                    name = GeneratingName();
                    break;
                default:
                    goto beginning;
            }
           

            

            if (name == null || name == "" || name.Trim().Length<=1) CreateCharacter();

            selection:
            Console.Clear();
            Console.WriteLine("Выберите класс:");
            systemInterface.ColorWrite(" Воин     - герой ближнего боя,\n\t +1 к силе со старта игры, может использовать мечи и щиты.\n", "Воин", ConsoleColor.Red);
            systemInterface.ColorWrite("\n Маг      - герой дальнего боя,\n\t +1 к инт. со старта игры, может использовать магическое оружие.\n", "Маг", ConsoleColor.Blue);
            systemInterface.ColorWrite("\n Лучник   - герой дальнего боя,\n\t +1 к ловкости со старта игры, может использовать лук.\n", "Лучник", ConsoleColor.Green);
            systemInterface.ColorWrite("\n Странник - герой универсал,\n\t -1 к навыкам для распределения, может использовать все типы оружия.", "Странник", ConsoleColor.DarkMagenta);
            Console.WriteLine("\n\n 1 - воин, 2 - маг\n 3 - лучник, 4 - странник");
            Console.WriteLine("\nТвой выбор: ");
            string choise = Console.ReadLine();

            switch (choise)
            {
                case "1":
                    proffesion = "Воин";
                    strength += 1;
                    physicWeapon = true;
                    break;
                case "2":
                    proffesion = "Маг";
                    intelligence += 1;
                    magicWeapon = true;
                    break;
                case "3":
                    proffesion = "Лучник";
                    agility += 1;
                    bowWeapon = true;
                    break;
                case "4":
                    proffesion = "Странник";
                    startPoints -= 1;
                    physicWeapon = true;
                    magicWeapon = true;
                    bowWeapon = true;
                    break;
                default:
                    goto selection;
                 


            }

            SpreadingPoints(startPoints);


        }
        void SpreadingPoints(int points) //распределение очков
        {
            Console.Clear();
            while (points > 0)
            {
                selectPoints:
                Console.Clear();
                Console.WriteLine("Что будем качать?\nОчков осталось: {0}.\n", points);
                
                DisplayCharacterPoints();

                Console.WriteLine("\n1 - Качать силу, 2 - Качать ловкость, 3 - Качать интеллект");
                switch (Console.ReadLine())
                {
                    case "1":
                        strength += 1;
                        points -= 1;
                        break;
                    case "2":
                        agility += 1;
                        points -= 1;
                        break;
                    case "3":
                        intelligence += 1;
                        points -= 1;
                        break;
                    default :
                        goto selectPoints;
                }
                if(strength > 10)
                {
                    strength = 10;
                    points += 1;
                    Console.WriteLine("Сила вкачана на максимум!");
                    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                    Console.ReadLine();
                    goto selectPoints;
                }
                if (agility > 10)
                {
                    agility = 10;
                    points += 1;
                    Console.WriteLine("Ловкость вкачана на максимум!");
                    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                    Console.ReadLine();
                    goto selectPoints;
                }
                if (intelligence > 10)
                {
                    intelligence = 10;
                    points += 1;
                    Console.WriteLine("Интеллект вкачан на максимум!");
                    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                    Console.ReadLine();
                    goto selectPoints;
                }


            }
            
            Console.Clear();
            Console.WriteLine();
            DisplayCharacterPoints();
            Console.WriteLine("\n\nУ вас нет очков для распределения, нажмите любую клавишу, чтобы продолжить");
            startPoints = 0;
            Console.ReadKey();
        }

        void DisplayCharacterPoints()
        {
            
            Console.Write("{0,-11}", "Сила:");
            for (int i = 1; i <= 10; i++)
            {   
                if(strength >= i)
                {
                    Console.Write("(+)");
                }
                else
                {
                    Console.Write("( )");
                }
            }
            Console.Write("\n{0,-11}", "Ловкость:");
            for (int i = 1; i <= 10; i++)
            {
                if (agility >= i)
                {
                    Console.Write("(+)");
                }
                else
                {
                    Console.Write("( )");
                }
            }
            Console.Write("\n{0,-11}", "Интеллект:");
            for (int i = 1; i <= 10; i++)
            {
                if (intelligence >= i)
                {
                    Console.Write("(+)");
                }
                else
                {
                    Console.Write("( )");
                }
            }
        }

        


        public void Greetings() // вывод информации о игроке
        {

            systemInterface.AddToInventory(inventory, "oldSword", 2, "Не хватает места!");
            systemInterface.AddToInventory(inventory, "apple", 20, "Не хватает места!");
            systemInterface.AddToInventory(inventory, "leatherGloves", 2, "Не хватает места!");
            systemInterface.RemoveFromInventory(inventory, "apple", 21, "Нет столько предметов!");

            ConsoleColor color = ConsoleColor.White;
            switch (proffesion)
            {
                case "Воин":
                    color = ConsoleColor.Red;
                    break;
                case "Маг":
                    color = ConsoleColor.Blue;
                    break;
                case "Лучник":
                    color = ConsoleColor.Green;
                    break;
                case "Странник":
                    color = ConsoleColor.DarkMagenta;
                    break;

            }
            
            systemInterface.ColorWrite($"Привет, {name}.", name, ConsoleColor.Blue);
            systemInterface.ColorWrite($" Твой класс {proffesion}.\n", proffesion, color);
            Console.Write("Твои статы:\n");
            systemInterface.ColorWrite($"Сила: {strength}\n", "Сила", ConsoleColor.DarkRed);
            systemInterface.ColorWrite($"Ловкость: {agility}\n", "Ловкость", ConsoleColor.DarkGreen);
            systemInterface.ColorWrite($"Интеллект: {intelligence}\n", "Интеллект", ConsoleColor.DarkBlue);









            systemInterface.InventorySelectMenu(inventory, false);



            Console.WriteLine("\nЛюбая клавиша - продолжить.");
            
            Console.ReadKey();
        }

        static string GeneratingName() //генерация имени(1+2 слоги) и фамилии(3+4 слоги)
        {
            string[] firstSyllabels = {"Ами","Ана", "Арт","Бле","Бог","Бру","Вал","Вла","Вен","Гав","Ген","Гле",
                                        "Дав","Дан","Джо","Ев","Ели","Ерем","Иго","Ирак","Инно","Лук","Люд","Ленар",
                                        "Мак","Ник","Осм","Пав","Пер","Рав","Руб","Рудол","Тим","Фар","Филл","Хак",
                                        "Цез","Чин","Шам","Эдг","Эрм","Юлин","Ян","Яро"};

            string[] secondSyllabels = {"рон","нап","гап","иан","мат","лан","маз","он","дий","сек","мий","ан","мет",
                                        "слав","слев","жамин","алий","орис","улат","ор","ад","ас","дан","димир","ван",
                                        "ндер","гиз","орь","тан","аак","арл","ма","енс","омир","офер","овик","ётр","ид",
                                        "епан","урен","ихон"};

            string[] thirdSyllabels = {"Ива","Сми","Пет","Нов","Вол","Мор","Але","Фед","Мон","Лебе","Сем","Его","Кор","Степ",
                                        "Орл","Пав","Нико","Анд","Мак","Ники","Зах","Соло","Зай","Бори","Вино","Кова","Бело",
                                        "Медве","Анто","Тара","Жук","Бара","Фили","Гера","Богда","Сидо","Матве","Тито","Миро",
                                        "Крыл","Черн","Ефи","Федо","Щерб","Наза"};

            string[] fourthSyllabels = {"нов","цов","ов","озов","лов","льев","ев","ров","цев","влев","рьев","дров","лев","лёв",
                                        "ин","мов","ков","дов","ев","нов","им","ин","ев"};

            Random rand = new Random();


            string result = firstSyllabels[rand.Next(0, firstSyllabels.Length)] +
                            secondSyllabels[rand.Next(0, secondSyllabels.Length)] + " " +
                            thirdSyllabels[rand.Next(0, thirdSyllabels.Length)] +
                            fourthSyllabels[rand.Next(0, fourthSyllabels.Length)];
            return result;

        }


        

    }
}

