namespace TextRPG
{
    public class DungeonBattle : Unit
    {
        public Player player;
        private Enemy enemy;
      //  private List<Enemy> enemies;
     //   private DungeonData dungeonData;
      //  private EnemyDataManager enemyDataManager;

        private int currentEnemyIndex;

        // 이벤트로 사망 처리
        public event Action PlayerDied;
        public event Action EnemyDied;

        public DungeonBattle()
        {           
            player = new Player("Screen");
         //   enemies = new List<Enemy>();
        //    dungeonData = new DungeonData();
         //   enemyDataManager = new EnemyDataManager();
        }
        public void BattleStart()
        {

            //   currentEnemyIndex = dungeonData.CurrentDungeonLevel;
            /*
               enemies = EnemyDataManager.instance.GetSpawnMonsters(currentEnemyIndex);  // 해당 던전 레벨에 맞는 몬스터 로드
               if (enemies == null || enemies.Count == 0)
               {
                   Console.WriteLine("적을 생성할 수 없습니다.");
                   return; // 더 이상 진행하지 않고 종료
               }
               Random random = new Random();
               currentEnemyIndex = random.Next(enemies.Count);
            */
            enemy = new Enemy();
            dungeonBattle();
        }

        public void dungeonBattle()
        {
            while (true)
            {
                Console.WriteLine("몬스터");
                Console.WriteLine($"몬스터: {enemy.Name}");
                Console.WriteLine($"HP: {enemy.Health}/{enemy.MaxHealth}");
                Console.WriteLine($"공격력: {enemy.Atk}");

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("[내정보]");
                Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.ePlayerClass})");
                Console.WriteLine($"HP {player.Health}/{player.MaxHealth}");

                Console.WriteLine("1. 공격");
                Console.WriteLine();

                Console.WriteLine("원하시는 행동을 입력해 주세요");
                Console.WriteLine(">> ");

                string choice = Console.ReadLine();


                switch (choice)
                {
                    case "1":
                        PlayerTurn();
                        EnemyTurn();
                        break;


                    default:
                        Console.WriteLine("잘못 된 입력입니다.");
                        break;
                }

                if (enemy.Health <= 0 || player.Health <= 0)
                {
                    return;  // 전투 종료 조건
                }
            }
        }

        public void PlayerTurn()
        {
            Console.WriteLine($"{player.Name}의 공격!");

            if (player.Health > 0)
            {
                int playerONAtackDamage = player.Attack();
                enemy.IsDamaged(playerONAtackDamage);

                if (enemy.Health <= 0)
                {
                    Console.WriteLine("승리");
                    Console.WriteLine($"남은 체력: {player.Health}");
                    EnemyDied?.Invoke();
                }
            }

            else
            {
                Console.WriteLine("힘이 다했다.");
                PlayerDied?.Invoke();
            }

        }

        public void EnemyTurn()
        {
            if (enemy.Health > 0)
            {
                int enemyOnAtackDamage = (int)enemy.Atk;
                player.IsDamaged(enemyOnAtackDamage);

                if (player.Health <= 0)
                {
                    Console.WriteLine("힘이 다했다.");
                    PlayerDied?.Invoke();
                }
            }
        }
    }
}