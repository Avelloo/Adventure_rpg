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
                        Thread.Sleep(delay * 4);
                        Console.Write(ch1);
                    }
                }
            }
        }

        static void DrawMenu(string[] options, int index, bool erase)
        {
             if(erase) ClearLines(options.Length);
            string prefix = " ";
            for(int i = 0; i < options.Length; i++)
            {
                if (options[i] == options[index])
                {
                    prefix = ">";
                    Console.WriteLine(prefix + options[i]);
                }
                else
                {
                    prefix = " ";
                    Console.WriteLine(prefix + options[i]);
                }
            }
        }

        /// <summary>
        /// Метод для навигации в виде меню
        /// </summary>
        /// <param name="options"> варианты навигации (ДОЛЖНЫ БЫТЬ РАВНЫ КОЛИЧЕСТВУ МЕТОДОВ)</param>
        /// <param name="method1">Метод 1</param>
        /// <param name="method2">Метод 2</param>
        public static void SelectMenu(string[] options, Delegate method1, Delegate method2)
        {
            int currentIndex = 0;
            bool pressed = false;

            method1();
            DrawMenu(options, currentIndex, false);
            while(pressed == false)
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
                    default:
                        break;
                }
                if(currentIndex > options.Length - 1)
                {
                    currentIndex = 0;
                }
                else if(currentIndex < 0)
                {
                    currentIndex = options.Length - 1;
                }

               DrawMenu(options, currentIndex, true);
            }
        }

 
        public static void AddToInventory(InventorySystem inventory,string itemname, int amount, string errorMsg)//Функция добавки в инветарь(с проверкой)
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

        public static void DrawInventory(InventorySystem inventory)
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

        public static void ClearLines(int lines)
        {
            for(int i = 1; i <= lines; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
        }

    }
}
