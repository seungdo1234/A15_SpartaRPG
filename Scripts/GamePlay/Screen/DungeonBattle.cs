using System.Reflection.Emit;
using System.Linq;
using System.Threading.Channels;

namespace TextRPG
{
    public class DungeonBattle:Screen
    {
        private List<Enemy> enemies;  // 여러 몬스터를 저장할 리스트

        // 이벤트로 사망 처리, LobbyScreen에서 구독 했음 확인 필요
        public event Action PlayerDied;
        public event Action EnemyDied;


        public DungeonBattle()
        {
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
                        BattleStart();
                        break;

                    default:
                        return;
                }
            } 

        }

        public void AppearEnemy()
        {
            int currentDungeonLevel = gm.Player.Level; // 임시로 집어 넣음, 원래는 던전 난이도를 집어 넣어야함
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

            while (enemies.Any(e => e.Health > 0) && gm.Player.Health > 0)
            {
                BattleText();
                int targetIndex = ChooseEnemy();
                if (targetIndex == -1) continue;

                PlayerAction(enemies[targetIndex]);

                foreach (var enemy in enemies.Where(e => e.Health > 0))
                {
                    EnemyTurn(enemy);
                    if (gm.Player.Health <= 0)
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
            if (gm.Player.Health > 0)
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

        private void PlayerAction(Enemy enemy)
        {
            Console.WriteLine();
            Console.WriteLine("행동을 선택하세요:");
            Console.WriteLine("1. 기본 공격");
            Console.WriteLine("2. 스킬 사용");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PlayerTurn(enemy);
                    break;
                case "2":
                    UseSkill(enemy);
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다.");
                    break;
            }
        }

        private void PlayerTurn(Enemy enemy)
        {
            // 선택한 몬스터의 이름을 포함하여 공격 메시지 출력
            Console.WriteLine($"{gm.Player.Name}의 {enemy.Name}를 향한 공격!");

            if (gm.Player.Health <= 0)
            {
                BettlePlayerLoseEnd();
                return;
            }

            string attackResult = gm.Player.Attack(enemy);
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

        private void UseSkill(Enemy enemy)
        {
            // 플레이어가 보유한 스킬 목록 출력
            Console.WriteLine("사용할 스킬을 선택하세요:");
            for (int i = 0; i < gm.Player.Skills.Count; i++)
            {
                var skill = gm.Player.Skills[i];
                Console.WriteLine($"{i + 1}. {skill.Name} (MP: {skill.ManaCost}) - {skill.Content}");
            }

            // 사용자 입력을 받아 선택된 스킬을 확인
            string input = Console.ReadLine();
            int selectedSkillIndex = int.Parse(input) - 1;
            if (selectedSkillIndex < 0 || selectedSkillIndex >= gm.Player.Skills.Count)
            {
                Console.WriteLine("잘못된 선택입니다.");
                return;
            }

            SkillData selectedSkill = gm.Player.Skills[selectedSkillIndex];

            // 마나가 부족하면 스킬 사용 불가
            if (gm.Player.Mana < selectedSkill.ManaCost)
            {
                Console.WriteLine("마나가 부족합니다.");
                return;
            }

            // 마나 소모하고 스킬 사용
            gm.Player.CostMana(selectedSkill.ManaCost);

            // 다중 대상 스킬인지 확인
            if (selectedSkill.IsMultiTarget)
            {
                Console.WriteLine("다중 대상 스킬 사용 중...");
                // 다중 공격 대상에 대한 로직 작성 (예시: MaxTargetCount까지)
                int targetsHit = 0;
                foreach (var target in enemies)
                {
                    if (target.Health > 0)
                    {
                        // 공격받은 대상의 이름을 함께 출력
                        Console.WriteLine($"{target.Name}을(를) 공격합니다...");
                        string skillResult = selectedSkill.CastSkill(gm.Player, target);
                        Console.WriteLine(skillResult);
                        Thread.Sleep(2000);

                        if (++targetsHit >= selectedSkill.MaxTargetCount)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                // 단일 대상 스킬 사용
                string skillResult = selectedSkill.CastSkill(gm.Player, enemy);
                Console.WriteLine(skillResult);
                Thread.Sleep(2000);
            }

            // 남은 적이 없으면 전투 승리 처리
            if (enemies.All(e => e.Health <= 0))
            {
                BettlePlayerWinEnd();
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

            /*
            // 적이 스킬을 보유하고 있는지 확인하고 스킬 사용
            if (enemy.SkillList.Any())
            {
                Random rnd = new Random();
                int skillIndex = rnd.Next(enemy.SkillList.Count);
                EnemySkill skill = enemy.SkillList[skillIndex];
                skill.Activate(player);
                Console.WriteLine($"{enemy.Name}의 {enemy.Skills}!");
                Thread.Sleep(2000);
            }
            */
            
            
                string attackResult = enemy.Attack(gm.Player);
                Console.WriteLine(attackResult);
            

            if (gm.Player.Health <= 0)
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
            Console.WriteLine($"Lv.{gm.Player.Level} {gm.Player.Name} ({gm.Player.GetPlayerClass(gm.Player.ePlayerClass)})");
            Console.WriteLine($"HP {gm.Player.Health}/{gm.Player.MaxHealth}\n");
            Console.WriteLine($"MP {gm.Player.Mana}/{gm.Player.MaxMana}");
        }

        private void BettlePlayerWinEnd()
        {
            if (enemies.All(e => e.Health <= 0))
            {
                Console.WriteLine($"{gm.Player.Name}이(가) 승리 하였습니다. 마을로 돌아갑니다.");
                Thread.Sleep(500);
                EnemyDied?.Invoke();
            }
        }

        private void BettlePlayerLoseEnd()
        {
            Console.WriteLine($"{gm.Player.Name}이(가) 패배 하였습니다. 마을로 돌아갑니다.");
            int RestoreHP = gm.Player.MaxHealth;

            // 임시 회복
            Thread.Sleep(2000);

            PlayerDied?.Invoke();
        }
    }
}