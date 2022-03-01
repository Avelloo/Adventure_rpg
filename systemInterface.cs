using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                foreach(char ch in normalParts[i])
                {
                    Thread.Sleep(delay);
                    Console.Write(ch);
                }
                
                if (i != normalParts.Length - 1)
                {
                    Console.ForegroundColor = color;

                    foreach (char ch1 in coloredWord)
                    {
                        Thread.Sleep(delay*7);
                        Console.Write(ch1);
                    }
                }
            }
        }

    }
}
