using System.Reflection.Emit;

namespace TextRPG
{
    public class DungeonBattle
    {
        public Player player;
        private Enemy enemy;
        private DungeonResultScreen dungeonResultScreen;


        private int currentEnemyIndex;

        // 이벤트로 사망 처리
        public event Action PlayerDied;
        public event Action EnemyDied;

        public DungeonBattle()
        {
            player = new Player("Tester");
        }
        public void BattleStart()
        {
            enemy = new Enemy();
            dungeonBattle();
        }

        public void dungeonBattle()
        {
            Console.Clear();
            while (true)
            {
                BattleText();
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
                    BettlePlayerWinEnd();
                }
            }

            else
            {
                BettlePlayerLoseEnd();
            }
            Console.Clear();
        }

        public void EnemyTurn()
        {
            if (enemy.Health > 0)
            {
                int enemyOnAtackDamage = (int)enemy.Atk;
                player.IsDamaged(enemyOnAtackDamage);

                if (player.Health <= 0)
                {
                    BettlePlayerWinEnd();
                }
            }

            else
            {
                BettlePlayerLoseEnd();
            }
        }

        private void BattleText()
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

        }

        private void BettlePlayerWinEnd()
        {
            Console.WriteLine($"{player.Name}이(가) 승리 하였습니다. 마을로 돌아갑니다.");
            Thread.Sleep(500);
            EnemyDied?.Invoke();
        }

        private void BettlePlayerLoseEnd()
        {
            Console.WriteLine($"{player.Name}이(가) 패배 하였습니다. 마을로 돌아갑니다.");
            Thread.Sleep(500);
            PlayerDied?.Invoke();
        }
    }
}