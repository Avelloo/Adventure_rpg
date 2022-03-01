using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_rpg
{
    
    
    
   public class Item //базовый класс предмета(любого)
    {
        public string name = "Default name";
        public string description = "Default description";
        public string type = string.Empty;
        public int maxSTACK = 0;

        public Item(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

    }

     public class Weapon : Item //класс оружия
    {
        public int damage = 0;

        public Weapon(string name, string description, string type, int damage)  : base(name,description)
        {
            this.damage = damage;
            this.type = type;
            this.maxSTACK = 1;
        }
        
        

    }

    class Consumables : Item //класс применяемых предметов (зельки-хуельки)
    {
        public Consumables(string name, string description, string type, int maxStack) : base(name, description)
        {
            this.maxSTACK = maxStack;
        }

    }




    





}
