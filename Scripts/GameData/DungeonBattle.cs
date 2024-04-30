namespace TextRPG
{
    public class DungeonBattle:Unit
    {
        private Random random = new Random();
        private Player player;
        private Enemy enemy;

        public void dungeonBattle()
        {
            Console.WriteLine("몬스터");
            Console.WriteLine($"몬스터: {enemy.Name}");
            Console.WriteLine($"HP: {enemy.Health}");
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

        }

        public void PlayerTurn()
        {
            while(true)
            {
                int playerONAtackDamage = (int)player.GetAtkValue();
                enemy.EnemyOnDamage(playerONAtackDamage);

                if (player.Health <= 0)
                {
                    Console.WriteLine("사망");
                    break;
                }
            }
        }

        public void EnemyTurn()
        {
            while(true)
            {
                int enemyOnAtackDamage = (int)enemy.Atk;
                player.IsDamaged(enemyOnAtackDamage);

                if (enemy.Health <= 0)
                {
                    Console.WriteLine("승리");
                    Console.WriteLine($"남은 체력: {player.Health}");
                    break;
                }
            }
        }
    }
}
