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

        // 이벤트로 사망 처리, LobbyScreen에서 구독 했음 확인 필요
        public event Action PlayerDied;
        public event Action EnemyDied;

        public DungeonBattle()
        {
            player = new Player("Tester");
            enemies = new List<Enemy>(); ;  // 몬스터를 저장할 리스트 초기화
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
            List<Enemy> originalEnemies = EnemyDataManager.instance.GetSpawnMonsters(currentDungeonLevel);  // 몬스터 데이터 매니저에서 몬스터 리스트 가져오기
            enemies = new List<Enemy>(originalEnemies.Select(e => new Enemy(e))); // 깊은 복사를 통해 리스트 복제

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

            while (enemies.Any(e => e.Health > 0) && player.Health > 0)
            {
                BattleText();
                int targetIndex = ChooseEnemy();
                if (targetIndex == -1) continue;

                PlayerTurn(enemies[targetIndex]);

                foreach (var enemy in enemies.Where(e => e.Health > 0))
                {
                    EnemyTurn(enemy);
                    if (player.Health <= 0)
                    {
                        BettlePlayerLoseEnd();
                        return;
                    }
                }

                if (!enemies.Any(e => e.Health > 0))
                {
                    Console.WriteLine("모든 적이 패배했습니다. 마을로 돌아갑니다.");
                    return;
                }
            }
            if (player.Health > 0)
                Console.WriteLine("모든 적을 처리했습니다. 마을로 돌아갑니다.");
        }

        private int ChooseEnemy()
        {
            Console.WriteLine("공격할 몬스터를 선택하세요:");
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].Health > 0)
                {
                    Console.WriteLine($"{i + 1}. {enemies[i].Name} (HP: {enemies[i].Health}/{enemies[i].MaxHealth})");
                }
            }
            string input = Console.ReadLine();
            int selected = int.Parse(input) - 1;
            if (selected < 0 || selected >= enemies.Count || enemies[selected].Health <= 0)
            {
                Console.WriteLine("잘못된 선택입니다.");
                return -1;
            }
            return selected;
        }


        private void PlayerTurn(Enemy enemy)
        {
            // 선택한 몬스터의 이름을 포함하여 공격 메시지 출력
            Console.WriteLine($"{player.Name}의 {enemy.Name}를 향한 공격!");
            
            if (player.Health <= 0)
            {
                BettlePlayerLoseEnd();
                return;
            }

            string attackResult = player.Attack(enemy);
            Console.WriteLine(attackResult);
            Thread.Sleep(2000);

            if (enemy.Health <= 0)
            {
                BettlePlayerWinEnd();
                Console.WriteLine($"[{enemy.Name}이(가) 쓰러졌습니다.]");
            }
            else
            {
                Console.Clear();
            }
        }

        private void EnemyTurn(Enemy enemy)
        {
            if (enemy.Health <= 0)
            {
                BettlePlayerWinEnd();
                return;
            }

            Console.WriteLine($"{enemy.Name}의 공격!");
            string attackResult = enemy.Attack(player);
            Console.WriteLine(attackResult);

            if (player.Health <= 0)
            {
                BettlePlayerLoseEnd();
            }
            else
            {
                Thread.Sleep(2000);
            }
        }

        private void BattleText()
        {
            Console.Clear();
            Console.WriteLine("=== 전투 중인 몬스터 목록 ===");
            foreach (var en in enemies.Where(e => e.Health > 0))
            {
                Console.WriteLine($"몬스터: {en.Name}, HP: {en.Health}/{en.MaxHealth}, 공격력: {en.Atk}");
            }
            Console.WriteLine("=== 내 정보 ===");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.GetPlayerClass(player.ePlayerClass)})");
            Console.WriteLine($"HP {player.Health}/{player.MaxHealth}\n");
        }

        private void BettlePlayerWinEnd()
        {
            if (enemies.All(e => e.Health <= 0))
            {
                Console.WriteLine($"{player.Name}이(가) 승리 하였습니다. 마을로 돌아갑니다.");
                Thread.Sleep(500);
                EnemyDied?.Invoke();
            }
        }

        private void BettlePlayerLoseEnd()
        {
            Console.WriteLine($"{player.Name}이(가) 패배 하였습니다. 마을로 돌아갑니다.");
            int RestoreHP = player.MaxHealth;

            // 임시 회복
            player.RecoveryHealth(RestoreHP);
            Thread.Sleep(2000);

            PlayerDied?.Invoke();
        }
    }
}