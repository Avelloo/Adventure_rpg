namespace Adventure_rpg
{
    public class Battle
    {
        private bool escaped;

        public bool Escaped { get => escaped; set => escaped = value; }

        public void StartBattle(Character character, int difficulty)
        {
            Escaped = false;
            Dictionary<string, Enemy> enemiesToChoose;
            int numberOfEnemies = 0;
            Enemy[] enemiesToFight;
            switch (difficulty)
            {
                case 1:
                    numberOfEnemies = Battle.ChooseNumberOfEnemies(30, 20, 15);
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
            for (int i = 0; i< numberOfEnemies;i++)
            {
                enemiesToFight[i] = enemiesToChoose.ElementAt(
                                        systemInterface.GetRandomNumberInInterval(0,
                                                        (enemiesToChoose.Count))).Value;
            }
            int phase = 1;
            while(character.CurrentHealth >= 1 || escaped)
            {
                Console.WriteLine("Фаза боя:" + phase);
                BattlePhase(character, enemiesToFight);
                phase++;
            }
        }
        void BattlePhase(Character character, Enemy[] enemiesToFight)
        {

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
}
