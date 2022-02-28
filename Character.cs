using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        bool physicWeapon = false;
        bool magicWeapon = false;
        bool bowWeapon = false;

        int startPoints = 1;

        List<object> items = new List<object>();

        



        public void CreateCharacter() //создание персонажа
        {
            
            Console.Clear();    
            Console.WriteLine("\nВы в меню создания персонажа. Введите имя: ");
            name = Console.ReadLine();

            if (name == null || name == "" || name.Trim().Length<=1) CreateCharacter();

            selection:
            Console.Clear();
            Console.WriteLine("\nВыберите класс:");
            Write(" Воин     - герой ближнего боя,\n\t +1 к силе со старта игры, может использовать мечи и щиты.\n", "Воин", ConsoleColor.Red);
            Write("\n Маг      - герой дальнего боя,\n\t +1 к инт. со старта игры, может использовать магическое оружие.\n", "Маг", ConsoleColor.Blue);
            Write("\n Лучник   - герой дальнего боя,\n\t +1 к ловкости со старта игры, может использовать лук.\n", "Лучник", ConsoleColor.Green);
            Write("\n Странник - герой универсал,\n\t -1 к навыкам для распределения, может использовать все типы оружия.", "Странник", ConsoleColor.DarkMagenta);
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

        static void Write(string text, string coloredWord, ConsoleColor color) //Вывод слова с цветом
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


        public void Greetings() // вывод информации о игроке
        {
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
            Console.Clear();
            Write($"Привет, {name}.", name, ConsoleColor.Blue);
            Write($" Твой класс {proffesion}.\n", proffesion, color);
            Console.Write("Твои статы:\n");
            Write($"Сила: {strength}\n", "Сила", ConsoleColor.DarkRed);
            Write($"Ловкость: {agility}\n", "Ловкость", ConsoleColor.DarkGreen);
            Write($"Интеллект: {intelligence}\n", "Интеллект", ConsoleColor.DarkBlue);

            
            
            items.Add(testBow);
            items.Add(testSword);

            Console.WriteLine("Ваше оружие:");
            foreach (Weapon i in items)
            {

                Console.Write(i.type + " -> " + i.name);
                Console.Write(". Урон: " + i.damage + "\n");
                Console.WriteLine("Описание: " + i.description);
               
                
                
            }

            Console.WriteLine("\nЛюбая клавиша - продолжить.");
            Console.ReadKey();
        }

        
            

        

    }
}

