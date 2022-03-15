using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_rpg
{
    public class Enemy
    {
        public int enemyHP;
        public int enemyATK;
        public int enemyInitiative;
        public string enemyName;
        public string enemyType;
        public string enemyImage;
        public string enemyDescription;

        public Enemy(int enemyHP, int enemyATK, int enemyInitiative, string enemyName, string enemyType,string enemyDescription, string enemyImage)
        {
            this.enemyHP = enemyHP;
            this.enemyATK = enemyATK;
            this.enemyInitiative = enemyInitiative;
            this.enemyName = enemyName;
            this.enemyType = enemyType;
            this.enemyDescription = enemyDescription;
            this.enemyImage = enemyImage;

        }
    }

    public class EnemyInitialization
    {
        static Enemy skeleton = new Enemy(10, 3, 3, "Скелет", "Undead", "Раньше был живым.. теперь нет (:", @"      .-.
     (o.o)
      |=|
     __|__
   //.=|=.\\
  // .=|=. \\
  \\ .=|=. //
   \\(_=_)//
    (:| |:)
     || ||
     () ()
     || ||
     || ||
   (==' '==");
        static Enemy pidoras = new Enemy(10, 3, 3, "Пидрила", "Undead", "Раньше был живым.. теперь нет (:", @"      .-.
     (o.o)
      |=|
     __|__
   //.=|=.\\
  // .=|=. \\
  \\ .=|=. //
   \\(_=_)//
    (:| |:)
     || ||
     () ()
     || ||
     || ||
   (==' '==");

        public static Dictionary<string, Enemy> easyEnemies = new Dictionary<string, Enemy>
        {
            {"skeleton",skeleton },
            {"pidoras",pidoras }
        };
    }
}
