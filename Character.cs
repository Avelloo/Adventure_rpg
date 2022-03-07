namespace Adventure_rpg
{

    public class Character
    {
        public InventorySystem inventory = new InventorySystem(10);
        public ArmorAndWeapon armorAndWeapon = new ArmorAndWeapon();
        string name = "";
        string proffesion = "";
        int strength = 1;
        int agility = 1;
        int intelligence = 1;
        int currentExp = 0;
        int currentAttack = 0;
        int currentDefence = 0;
        int charLVL = 1;
        int money = 0;
        int maxHealth = 100;

        int currentHealth = 0;
        int playerDMG = 0;
        int playerDefencePercentage = 0;

        int skillPoints = 1;

        public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int CurrentExp { get => currentExp; set => currentExp = value; }
        public int PlayerDMG { get => playerDMG; set => playerDMG = value; }
        public int PlayerDefencePercentage { get => playerDefencePercentage; set => playerDefencePercentage = value; }
        public string Proffesion { get => proffesion; set => proffesion = value; }

        public void CreateCharacter() //создание персонажа
        {


            Console.Clear();
            Console.WriteLine(@"               _   _                _____                      
     /\       | | (_)              / ____|                     
    /  \   ___| |_ _  ___  _ __   | |  __  __ _ _ __ ___   ___ 
   / /\ \ / __| __| |/ _ \| '_ \  | | |_ |/ _` | '_ ` _ \ / _ \
  / ____ | (__| |_| | (_) | | | | | |__| | (_| | | | | | |  __/
 /_/    \_\___|\__|_|\___/|_| |_|  \_____|\__,_|_| |_| |_|\___|
                                                               
                                                               ");
            Console.WriteLine("Привет! Начинаем создание персонажа.");
            switch (systemInterface.DrawMenuAndReturnAction(new string[] { "Ввести имя", "Сгенерировать имя" }))
            {

                case "Ввести имя":
                    Console.WriteLine("\nВведите имя: ");
                    name = Console.ReadLine();
                    break;
                case "Сгенерировать имя":
                    name = GeneratingName();
                    break;
                default:
                    CreateCharacter();
                    break;
            }




            if (name == null || name == "" || name.Trim().Length <= 1) CreateCharacter();

            selection:
            Console.Clear();
            Console.WriteLine("Выберите класс:");
            systemInterface.ColorWrite(" Воин     - герой ближнего боя,\n\t +1 к силе со старта игры, может использовать мечи.\n", "Воин", ConsoleColor.Red);
            systemInterface.ColorWrite("\n Маг      - герой дальнего боя,\n\t +1 к инт. со старта игры, может использовать магическое оружие.\n", "Маг", ConsoleColor.Blue);
            systemInterface.ColorWrite("\n Лучник   - герой дальнего боя,\n\t +1 к ловкости со старта игры, может использовать лук.\n", "Лучник", ConsoleColor.Green);
            systemInterface.ColorWrite("\n Странник - герой универсал,\n\t -1 к навыкам для распределения, может использовать все типы оружия.", "Странник", ConsoleColor.DarkMagenta);
            Console.WriteLine("\n\nТвой выбор: \n");

            switch (systemInterface.DrawMenuAndReturnAction(new string[] { "Воин", "Маг", "Лучник", "Странник" }))
            {
                case "Воин":
                    proffesion = "Воин";
                    strength += 1;

                    break;
                case "Маг":
                    proffesion = "Маг";
                    intelligence += 1;

                    break;
                case "Лучник":
                    proffesion = "Лучник";
                    agility += 1;

                    break;
                case "Странник":
                    proffesion = "Странник";
                    skillPoints -= 1;

                    break;
                default:
                    goto selection;



            }

            SpreadingPoints(skillPoints);
            CurrentHealth = maxHealth;



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
                Console.WriteLine("\n");
                switch (systemInterface.DrawMenuAndReturnAction(new string[] { "Сила", "Ловкость", "Интеллект" }))
                {
                    case "Сила":
                        strength += 1;
                        points -= 1;
                        break;
                    case "Ловкость":
                        agility += 1;
                        points -= 1;
                        break;
                    case "Интеллект":
                        intelligence += 1;
                        points -= 1;
                        break;
                    default:
                        goto selectPoints;
                }
                if (strength > 10)
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
            skillPoints = 0;
            Console.ReadKey();
        }

        void DisplayCharacterPoints()
        {

            Console.Write("{0,-11}", "Сила:");
            for (int i = 1; i <= 10; i++)
            {
                if (strength >= i)
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




        public void Greetings(Game game) // вывод информации о игроке
        {
            Console.Clear();


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

            systemInterface.ColorWrite($"Привет, {name}.\n", name, ConsoleColor.Blue);
            systemInterface.ColorWrite($"У тебя сейчас {currentHealth}/{maxHealth} хп.\n", maxHealth.ToString(), ConsoleColor.Red);
            systemInterface.ColorWrite($"Твой класс {proffesion}.\n", proffesion, color);
            Console.Write("Твои статы:\n");
            systemInterface.ColorWrite($"Сила: {strength}/10\n", "Сила", ConsoleColor.DarkRed);
            systemInterface.ColorWrite($"Ловкость: {agility}/10\n", "Ловкость", ConsoleColor.DarkGreen);
            systemInterface.ColorWrite($"Интеллект: {intelligence}/10\n", "Интеллект", ConsoleColor.DarkBlue);
            if (money > 0)
            {
                systemInterface.ColorWrite($"Твой кошель наполнен на [{money}] золота.\n", "золота", ConsoleColor.Yellow);
            }
            else
            {
                systemInterface.ColorWrite($"У тебя нет золота. Совсем.\n", "золота", ConsoleColor.Yellow);
            }


            Console.WriteLine("\n");
            switch (systemInterface.DrawMenuAndReturnAction(new string[] { "Инвентарь", "Снаряжение", "Назад" }))
            {
                case "Инвентарь":
                    Console.Clear();
                    systemInterface.InventorySelectMenu(inventory, armorAndWeapon, false, "Информация", game);
                    break;
                case "Снаряжение":
                    Console.Clear();
                    systemInterface.DisplayWearingSelectMenu(inventory, armorAndWeapon, "Информация", game);
                    break;
                case "Назад":
                    Console.Clear();
                    game.ChooseAction();
                    break;
            }


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

