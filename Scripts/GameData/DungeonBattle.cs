namespace TextRPG
{
    public class DungeonBattle:Unit
    {
        private Random random = new Random();
        private Player player;
        private Enemy enemy;

        public void dungeonBattle()
        {
            while (true)
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
