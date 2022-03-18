namespace Adventure_rpg
{
    public class Battle
    {
        private bool escaped;
        private string endBattle;
        public EnemyCell[] enemyCells;
        public Enemy[] enemiesToFight;
        public bool Escaped { get => escaped; set => escaped = value; }
        public int phase = 1;
        public int goldAward = 0;
        public void StartBattle(Character character, int difficulty)
        {
            Escaped = false;
            Dictionary<string, Enemy> enemiesToChoose;
            int numberOfEnemies = 0;
            
            switch (difficulty)
            {
                case 1:
                    numberOfEnemies = Battle.ChooseNumberOfEnemies(20, 15, 10);
                    enemiesToChoose = EnemyInitialization.easyEnemies;
                    break;
                case 2:
                    numberOfEnemies = Battle.ChooseNumberOfEnemies(30, 20, 20);
                    enemiesToChoose = EnemyInitialization.easyEnemies;
                    break;
                case 3:
                    numberOfEnemies = Battle.ChooseNumberOfEnemies(40, 30, 20);
                    enemiesToChoose = EnemyInitialization.easyEnemies;
                    break;
                default: throw new Exception("Нет такой сложности!");
            }
            enemiesToFight = new Enemy[numberOfEnemies];
            enemyCells = new EnemyCell[numberOfEnemies];
            for (int i = 0; i < numberOfEnemies; i++)
            {
                enemiesToFight[i] = enemiesToChoose.ElementAt(
                                        systemInterface.GetRandomNumberInInterval(0,
                                                        (enemiesToChoose.Count))).Value;
            }
            for (int i = 0; i < enemiesToFight.Length; i++)
            {
                enemyCells[i] = new EnemyCell(enemiesToFight[i]);
            }


            while (endBattle == null)
            {

                BattlePhase(character, enemyCells);
                Console.Clear();

            }

            switch (endBattle)
            {
                case "Победа":
                    Console.Clear();
                    systemInterface.ColorWrite($"Вы победили! Награда за сражение - {goldAward} золота.\n", goldAward.ToString(), ConsoleColor.DarkYellow);
                    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                    character.Money += goldAward;
                    Console.ReadKey();
                    break;
                case "Поражение":
                    Console.Clear();
                    systemInterface.ColorWrite($"Вы проиграли! Вы лишаетесь всех денег!\n", "всех денег", ConsoleColor.DarkRed);
                    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                    character.CurrentHealth = character.MaxHealth / 2;
                    character.Money = 0;
                    Console.ReadKey();
                    break;
            }

        }
        void BattlePhase(Character character, EnemyCell[] enemyCell)
        {
            //Enemy[] aliveEnemies = (Enemy[])enemiesToFight
            //                            .Where(x => x.enemyCurrentHP > 0)
            //                            .ToArray();
            //string[] options = new string[aliveEnemies.Length];
            //options = aliveEnemies.Select(x => x.enemyName + " хп:" + x.enemyMaxHP).ToArray();
            
            if (systemInterface.CheckForAliveEnemiesAndUpdateAttackStatus(enemyCells))
            {
                
                Console.WriteLine("Фаза боя:" + phase);
                Console.WriteLine($"У вас сейчас {character.CurrentHealth}/{character.MaxHealth} ХП\n");
                phase++;
                Console.WriteLine("Кого атаковать:");
                EnemyCell chosenEnemy = systemInterface.DrawEnemiesAndReturnChosen(enemyCells);
                chosenEnemy.EnemyCurrentHP -= character.CurrentAttack;
                if (chosenEnemy.EnemyCurrentHP < 0)
                {
                    chosenEnemy.CanAttack = false;
                    Console.Clear();
                    systemInterface.ColorWrite($"{character.Name} наносит {chosenEnemy.thisEnemy.enemyName} {character.CurrentAttack} урона!\n", character.Name, ConsoleColor.Blue);
                    Console.WriteLine($"{chosenEnemy.thisEnemy.enemyName} умер!");
                    Thread.Sleep(250);
                    systemInterface.ClearLines(1);
                }
                else
                {
                    systemInterface.ClearLines(enemyCells.Length + 4);
                    systemInterface.ColorWrite($"{character.Name} наносит {chosenEnemy.thisEnemy.enemyName} {character.CurrentAttack} урона!\n", character.Name, ConsoleColor.Blue);
                    Thread.Sleep(250);
                }

               


            }
            else
            {
                foreach(Enemy n in enemiesToFight)
                {
                    goldAward += n.goldDrop;
                }
                endBattle = "Победа";
            }
            for (int i = 0; i < enemyCells.Length; i++)
            {
                if (enemyCells[i].CanAttack)
                {
                    Thread.Sleep(250);
                    double intakeDamage = enemyCells[i].thisEnemy.enemyATK * character.PlayerIntakeDamage;
                    Console.WriteLine($"[#{i+1}] {enemyCells[i].thisEnemy.enemyName} наносит вам {(int)intakeDamage}" +
                        $"(-{enemyCells[i].thisEnemy.enemyATK - (int)intakeDamage}) урона!");
                    character.CurrentHealth -= (int)intakeDamage;
                    Thread.Sleep(250);
                }

            }
            Thread.Sleep(500);
            if (character.CurrentHealth <= 0)
            {

                endBattle = "Поражение";
            }



        }


        public static int ChooseNumberOfEnemies(int chance1, int chance2, int chance3)
        {
            int totalEnemies = 1;
            int currRandom;
            currRandom = systemInterface.GetRandomNumberInInterval(0, 100);
            if (currRandom <= chance1) totalEnemies++;
            //Console.WriteLine($"Выпал рандом {currRandom}, всего врагов {totalEnemies}");
            currRandom = systemInterface.GetRandomNumberInInterval(0, 100);
            if (currRandom <= chance2) totalEnemies++;
            //Console.WriteLine($"Выпал рандом {currRandom}, всего врагов {totalEnemies}");
            currRandom = systemInterface.GetRandomNumberInInterval(0, 100);
            if (currRandom <= chance3) totalEnemies++;
            //Console.WriteLine($"Выпал рандом {currRandom}, всего врагов {totalEnemies}");
            //Console.WriteLine("Ты будешь сражаться с " + totalEnemies + " врагами.");
            // Console.WriteLine("___");
            return totalEnemies;
        }
    }

    public class EnemyCell //Класс инстанции врага(индивидуальный экземпляр экземпляра (: )
    {
        public int EnemyCurrentHP { get; set; }
        public bool CanAttack { get; set; }
        public Enemy thisEnemy { get; set; }

        public bool currentTurnAttacked { get; set; }
        public EnemyCell(Enemy enemy)
        {
            this.thisEnemy = enemy;
            this.EnemyCurrentHP = enemy.enemyMaxHP;
            this.CanAttack = true;
            currentTurnAttacked = false;
        }
    }
}
