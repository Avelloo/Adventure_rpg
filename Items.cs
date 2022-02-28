using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_rpg
{

    class Items
    {   
        
        
    }

    class Weapon : Items
    {
        public string name = string.Empty;
        public string description = string.Empty;
        public string type = string.Empty;
        public int damage = 0;

        public Weapon(string name, string description, string type, int damage)
        {
            this.name = name;
            this.description = description;
            this.damage = damage;
            this.type = type;
        }

       
    }
    
    
    
        
    





}
