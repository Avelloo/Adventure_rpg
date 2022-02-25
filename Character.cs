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
        int strength = 0;
        int agility = 0;
        int intelligence = 0;

        int startPoints = 5;

        public void CreateCharacter() //создание персонажа
        {
         
            Console.Clear();    
            Console.WriteLine("Вы в меню создания персонажа. Введите имя: ");
            name = Console.ReadLine();
            if (name == null || name == "") CreateCharacter();

            selection:
            Console.Clear();
            Console.WriteLine("Выберите класс:");
            Write(" Воин     - герой ближнего боя,\n\t +1 к силе со старта игры, может использовать двуручное оружие.\n", "Воин", ConsoleColor.Red);
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
                    break;
                case "2":
                    proffesion = "Маг";
                    intelligence += 1;
                    break;
                case "3":
                    proffesion = "Лучник";
                    agility += 1;
                    break;
                case "4":
                    proffesion = "Странник";
                    startPoints -= 1;
                    break;
                default:
                    goto selection;
                 


            }

            SpreadingStartPoints(startPoints);


        }
        void SpreadingStartPoints(int points) //распределение стартовых очков
        {
            Console.Clear();
            do
            {
                selectPoints:
                Console.Clear();
                Console.WriteLine("Что будем качать?\n У вас осталось {0} свободных очков.\n", points);

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
                
            }
            while(points > 0);
            Console.Clear();
            Console.WriteLine();
            DisplayCharacterPoints();
            Console.WriteLine("\n\nУ вас осталось {0} свободных очков. Нажмите любую клавишу, чтобы продолжить.\n", points);
            Console.ReadLine();
        }

        void DisplayCharacterPoints()
        {
            
            Console.Write("{0,-11}", "Сила:");
            for (int i = 1; i <= strength; i++)
            {
                Console.Write("(+)");
            }
            Console.Write("\n{0,-11}", "Ловкость:");
            for (int i = 1; i <= agility; i++)
            {
                Console.Write("(+)");
            }
            Console.Write("\n{0,-11}", "Интеллект:");
            for (int i = 1; i <= intelligence; i++)
            {
                Console.Write("(+)");
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
            Console.WriteLine("\nЛюбая клавиша - продолжить.");
            Console.ReadLine();
        }

    }
}
