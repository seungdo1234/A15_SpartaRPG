namespace TextRPG
{
    public class DungeonBattle : Unit
    {
        private Player player;
        private Enemy enemy;

        // 이벤트로 사망 처리
        public event Action PlayerDied;
        public event Action EnemyDied;

        public DungeonBattle()
        {
            player = new Player(Name);
            enemy = new Enemy();
        }
        public void BattleStart()
        {
            enemy = new Enemy();  // 새 적 생성
            player = new Player(Name);
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
                Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.PlayerClass})");
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
                    Console.WriteLine("힘이 다햇다.");
                    PlayerDied?.Invoke();
                }
            }
        }
    }
}