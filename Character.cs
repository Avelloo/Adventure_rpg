namespace Adventure_rpg
{

    public class Character
    {

        public InventorySystem characterInventory = new InventorySystem(10);
        private ArmorAndWeapon armorAndWeapon = new ArmorAndWeapon();
        string name = "";
        string proffesion = "";
        int strength = 1;
        int agility = 1;
        int intelligence = 1;

        int charLVL = 7;
        int currentExp = 0;
        int expToNextLvl = 0;

        int currentAttack = 0;
        int currentDefence = 0;

        
        int money = 100;

        int baseMaxHealth = 95;
        int bonusHP = 0;
        int maxHealth;

        int evasionPercentage = 0;
        int critChangePercentage = 0;
        int critDamage = 150;

        int initiative = 1;
        int priceDiscount = 0;

        int currentHealth = 0;
        int playerDMG = 0;
        double playerIntakeDamage = 0;
        double playerDefenceReduction = 0;
        int skillPoints = 1;

        public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int CurrentExp { get => currentExp; set => currentExp = value; }
        public int PlayerDMG { get => playerDMG; set => playerDMG = value; }
        public double PlayerIntakeDamage { get => playerIntakeDamage; set => playerIntakeDamage = value; }
        public string Proffesion { get => proffesion; set => proffesion = value; }
        public int CharLVL { get => charLVL; set => charLVL = value; }
        public int Money { get => money; set => money = value; }
        public ArmorAndWeapon ArmorAndWeapon { get => armorAndWeapon; set => armorAndWeapon = value; }
        public int EvasionPercentage { get => evasionPercentage; set => evasionPercentage = value; }
        public int CritChangePercentage { get => critChangePercentage; set => critChangePercentage = value; }
        public int CritDamage { get => critDamage; set => critDamage = value; }
        public int Initiative { get => initiative; set => initiative = value; }
        public int PriceDiscount { get => priceDiscount; set => priceDiscount = value; }
        public string Name { get => name; set => name = value; }
        public int ExpToNextLvl { get => expToNextLvl; set => expToNextLvl = value; }
        public int CurrentAttack { get => currentAttack; set => currentAttack = value; }
        public int CurrentDefence { get => currentDefence; set => currentDefence = value; }

        public void CreateCharacter() //создание персонажа
        {
            bool nameSelected = false;
            while (nameSelected == false)
            {
                Console.Clear();
                Console.WriteLine(@"               _   _                _____                      
     /\       | | (_)              / ____|                     
    /  \   ___| |_ _  ___  _ __   | |  __  __ _ _ __ ___   ___ 
   / /\ \ / __| __| |/ _ \| '_ \  | | |_ |/ _` | '_ ` _ \ / _ \
  / ____ | (__| |_| | (_) | | | | | |__| | (_| | | | | | |  __/
 /_/    \_\___|\__|_|\___/|_| |_|  \_____|\__,_|_| |_| |_|\___|
                                                           suck some dick    
                                                               ");
                Console.WriteLine("Привет! Начинаем создание персонажа.");
                
                switch (systemInterface.DrawMenuAndReturnAction(new string[] { "Ввести имя", "Сгенерировать имя" }))
                {

                    case "Ввести имя":
                        Console.WriteLine("\nВведите имя (не более 15 символов): ");
                        name = Console.ReadLine();
                        break;
                    case "Сгенерировать имя":
                        name = GeneratingName();
                        break;
                    default:
                        break;
                }




                if (name != null && name != "" && name.Length <= 15 && name.Trim().Length > 1) nameSelected = true;            
            
            }



            Console.Clear();
            Console.WriteLine("Выберите класс:");
            systemInterface.ColorWrite(" Воин     - герой ближнего боя,\n\t +1 к силе со старта игры, может использовать только мечи.\n", "Воин", ConsoleColor.Red);
            systemInterface.ColorWrite("\n Маг      - герой дальнего боя,\n\t +1 к инт. со старта игры, может использовать только магическое оружие.\n", "Маг", ConsoleColor.Blue);
            systemInterface.ColorWrite("\n Лучник   - герой дальнего боя,\n\t +1 к ловкости со старта игры, может использовать только лук.\n", "Лучник", ConsoleColor.Green);
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


            }

            SpreadingPoints(skillPoints);
            RecalculateStats(ArmorAndWeapon);
            currentHealth = 1;


        }
        void SpreadingPoints(int points) //распределение очков
        {
            Console.Clear();
            while (points > 0)
            {
            selectPoints:
                Console.Clear();
                systemInterface.ColorWrite(@"
 Каждое очко навыков даёт вашему персонажу определённые бонусы:
         За каждое очко силы персонаж получает +5 хп
             за каждые 2 -> +1 к урону.
         За каждое очко ловкости персонаж получает +2% к шансу крит. удара
             За каждые 2 -> +2% к шансу уклонения.
         За каждое очко интеллекта персонаж получает +1 к инициативе
             за каждые 2 -> +5% к скидке у торговца (с округлением
                                                    в большую сторону)","->",ConsoleColor.DarkYellow);
                Console.WriteLine("\n На что потратите очки?\n Очков осталось: {0}.\n", points);

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
                    Console.ReadKey();
                    goto selectPoints;
                }
                if (agility > 10)
                {
                    agility = 10;
                    points += 1;
                    Console.WriteLine("Ловкость вкачана на максимум!");
                    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                    Console.ReadKey();
                    goto selectPoints;
                }
                if (intelligence > 10)
                {
                    intelligence = 10;
                    points += 1;
                    Console.WriteLine("Интеллект вкачан на максимум!");
                    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                    Console.ReadKey();
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

            Console.Write("{0,-11}", " Сила:");
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
            Console.Write("\n{0,-11}", " Ловкость:");
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
            Console.Write("\n{0,-11}", " Интеллект:");
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



        public void RecalculateStats(ArmorAndWeapon armorAndWeapon)
        {
            bonusHP = strength * 5;
            maxHealth = baseMaxHealth + bonusHP;
            playerDMG = ForEveryNth(strength, 2);

            critChangePercentage = agility*2;
            evasionPercentage = ForEveryNth(agility, 2) * 2;

            initiative = intelligence;
            priceDiscount = ForEveryNth(intelligence, 2) * 5;

            playerDefenceReduction = 0.015f * armorAndWeapon.GetDefenceCombined();

            
            playerDefenceReduction = Math.Round(playerDefenceReduction, 2, MidpointRounding.AwayFromZero) * 100;
            playerIntakeDamage = 1 - playerDefenceReduction;

            currentAttack = armorAndWeapon.GetWeaponDamage() + playerDMG;
            currentDefence = armorAndWeapon.GetDefenceCombined();

            
        }
        public void Greetings(Game game) // вывод информации о игроке
        {
            Console.Clear();
            RecalculateStats(ArmorAndWeapon);
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

            systemInterface.ColorWrite($" Привет, {name}.\n", name, ConsoleColor.Blue);
            systemInterface.ColorWrite($" У тебя сейчас {currentHealth}/{maxHealth} хп.\n", currentHealth.ToString() + "/" + maxHealth.ToString(), ConsoleColor.Red); ;
            systemInterface.ColorWrite($" Твой класс {proffesion}.\n\n", proffesion, color);
            systemInterface.ColorWrite($" Всего урона: {currentAttack}.\n", currentAttack.ToString(), ConsoleColor.DarkRed);
            systemInterface.ColorWrite($" Всего защиты: {currentDefence}. [-{playerDefenceReduction}%] к получаемому урону.\n"," " +  currentDefence.ToString() + ".", ConsoleColor.Cyan);
            systemInterface.ColorWrite($" Сейчас у тебя {charLVL} уровень.\n", charLVL.ToString(), ConsoleColor.DarkGray);
            systemInterface.ColorWrite($" Опыта до следующего уровня: {expToNextLvl}.\n", expToNextLvl.ToString(), ConsoleColor.Gray);
            Console.Write(" Твои статы:");
            systemInterface.ColorWrite($"\n Сила: {strength}/10\n Бонус к макс ХП: [{bonusHP-5}], бонус к урону: [{playerDMG}].\n\n", "Сила", ConsoleColor.DarkRed);
            systemInterface.ColorWrite($" Ловкость: {agility}/10\n Шанс крит. удара: [{critChangePercentage}%], множитель: [{critDamage}%].\n\n", "Ловкость", ConsoleColor.DarkGreen);
            systemInterface.ColorWrite($" Интеллект: {intelligence}/10\n Инициатива: [{initiative}], скидка у торговца: [{priceDiscount}%].\n\n", "Интеллект", ConsoleColor.DarkBlue);
            if (Money > 0)
            {
                systemInterface.ColorWrite($" Твой кошель наполнен на [{Money}] золота.\n", "золота", ConsoleColor.Yellow);
            }
            else
            {
                systemInterface.ColorWrite($" У тебя нет золота. Совсем.\n", "золота", ConsoleColor.Yellow);
            }


            Console.WriteLine("\n");
            switch (systemInterface.DrawMenuAndReturnAction(new string[] { "Инвентарь", "Снаряжение", "Назад" }))
            {
                case "Инвентарь":
                    Console.Clear();
                    systemInterface.InventorySelectMenu(characterInventory, armorAndWeapon, false, "Информация", game);
                    break;
                case "Снаряжение":
                    Console.Clear();
                    systemInterface.DisplayWearingSelectMenu(characterInventory, armorAndWeapon, "Информация", game);
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

        int ForEveryNth(int number, int nth)
        {
            int result = 0;
            for(int i = 1; i <= number; i++)
            {
                if(i % nth == 0)
                {
                    result++;
                }
            }
            return result;
        }
    }


}

