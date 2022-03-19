using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_rpg
{
    public class Enemy
    {
        public int enemyMaxHP;
        public int enemyATK;
        public int enemyInitiative;
        public string enemyName;
        public string enemyType;
        public string enemyImage;
        public string enemyDescription;
        public int goldDrop;

        public Enemy(int enemyHP, int enemyATK, int enemyInitiative,int goldDrop, string enemyName, string enemyType,string enemyDescription, string enemyImage)
        {
            this.enemyMaxHP = enemyHP;
            this.enemyATK = enemyATK;
            this.enemyInitiative = enemyInitiative;
            this.enemyName = enemyName;
            this.enemyType = enemyType;
            this.enemyDescription = enemyDescription;
            this.enemyImage = enemyImage;
            this.goldDrop = goldDrop;
        }
    }

    public class EnemyInitialization
    {
        static Enemy skeleton = new Enemy(10, 3, 3,3, "Скелет", "Undead", "Раньше был живым.. теперь нет (:", @"      .-.
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
        static Enemy spider = new Enemy(10, 3, 3,3, "Паук", "Undead", "Раньше был живым.. теперь нет (:", @"      .-.
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
            {"spider",spider }
        };
    }
}
