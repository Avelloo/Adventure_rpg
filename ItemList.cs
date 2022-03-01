using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_rpg
{
    public class ItemList //Инициализация всех предметов в игре
    {

        static Weapon testWeapon = new Weapon("Меч пуканов", "пердит-воняет", "weapon", 123);
        static Consumables potion = new Consumables("potiontest", "Portiondescription", "porion",10);


        public static Dictionary<string, Item> allItems = new Dictionary<string, Item>()
        {

            {"test", testWeapon },
            {"test2", potion }
        };



    }
}
