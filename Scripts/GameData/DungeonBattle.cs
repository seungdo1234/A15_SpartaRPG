using System.Reflection.Emit;
using System.Linq;

namespace TextRPG
{
    public class DungeonBattle
    {
        public Player player;
        private Enemy enemy;
        private List<Enemy> enemies;  // 여러 몬스터를 저장할 리스트
        
        // 던전 몬스터 랜덤 추출
        private int currentEnemyIndex;
        private Random random = new Random();

        // 이벤트로 사망 처리
        public event Action PlayerDied;
        public event Action EnemyDied;

        public DungeonBattle()
        {
            player = new Player("Tester");
            enemies = new List<Enemy>().ToList(); ;  // 몬스터를 저장할 리스트 초기화
        }

        public void CheckforBattle()
        {

            while (true)
            {
                Console.WriteLine("정말 던전에 진입하시겠습니까? 끝을 보시거나, 죽기 전까지 탈출하실 수 없습니다.");
                Console.WriteLine();
                Console.WriteLine("1. 들어간다");
                Console.WriteLine("0. 나간다");
                
                string choice = Console.ReadLine();

                switch(choice)
                {
                    case "1":
                        enemies.Clear();  // 이전 몬스터 목록을 클리어
                        AppearEnemy();
                        if (enemies.Any())  // 몬스터가 있는지 확인 후 전투 시작
                        {
                            BattleStart();
                        }
                        else
                        {
                            Console.WriteLine("전투할 몬스터가 없습니다.");
                        }
                        break;

                    default:
                        return;
                }
            } 

        }

        public void AppearEnemy()
        {
            int currentDungeonLevel = player.Level; // 임시로 집어 넣음, 원래는 던전 난이도를 집어 넣어야함
            enemies = EnemyDataManager.instance.GetSpawnMonsters(currentDungeonLevel);  // 몬스터 데이터 매니저에서 몬스터 리스트 가져오기

            foreach (var enemy in enemies)
            {
                Console.WriteLine($"{enemy.Name}가 나타났습니다!");
            }
        }

        public void BattleStart()
        {
            dungeonBattle(); // 전투 시작
        }

        public void dungeonBattle()
        {
            Console.Clear();
            currentEnemyIndex = 0;

            while (currentEnemyIndex < enemies.Count)
            {
                enemy = enemies[currentEnemyIndex]; // 현재 적 업데이트
                while (enemy.Health > 0 && player.Health > 0)
                {
                    BattleText(enemy);
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            PlayerTurn(enemy);
                            if (enemy.Health <= 0)
                            {
                                BettlePlayerWinEnd();
                                break; // 내부 while 루프 벗어남
                            }
                            EnemyTurn(enemy);
                            break;

                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }

                    if (player.Health <= 0)
                    {
                        BettlePlayerLoseEnd();
                    }
                }

                currentEnemyIndex++; // 다음 적으로 이동
                if (currentEnemyIndex >= enemies.Count)
                {
                    Console.WriteLine("모든 적이 패배했습니다. 마을로 돌아갑니다.");
                }
            }
        }

        public void PlayerTurn(Enemy enemy)
        {
            Console.WriteLine($"{player.Name}의 공격!");

            if (player.Health > 0)
            {
                // 플레이어의 공격이 성공적으로 적을 타격하면 true, 회피되면 false를 반환
                bool hitSuccess = player.Attack(enemy);

                if (hitSuccess && enemy.Health <= 0)
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

        public void EnemyTurn(Enemy enemy)
        {
            if (enemy.Health > 0)
            {
                Console.WriteLine($"{enemy.Name}의 공격!");

                bool hitSuccess = enemy.Attack(player);

                if (!hitSuccess)
                {
                    Console.WriteLine($"{player.Name}은(는) 공격을 회피했습니다!");
                }

                // 공격이 성공했을 때만 피해를 출력합니다.
                if (hitSuccess && player.Health <= 0)
                {
                    Console.WriteLine($"{player.Name}은(는) 사망했습니다!");
                    BettlePlayerLoseEnd();
                }
            }
        }

        private void BattleText(Enemy enemy)
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