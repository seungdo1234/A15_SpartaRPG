using System.Collections.Generic;

namespace TextRPG
{
    public class DungeonBattleScreen : Screen
    {
        private List<Enemy> enemies;  // 여러 몬스터를 저장할 리스트


        private bool isEnd;
        private bool isWin;
        private DungeonResultScreen dungeonResultScreen;
        public DungeonBattleScreen()
        {
            dungeonResultScreen = new DungeonResultScreen();
            enemies = new List<Enemy>(); ;  // 몬스터를 저장할 리스트 초기화
        }

        public void CheckforBattle()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n정말 던전에 진입하시겠습니까? 끝을 보시거나, 죽기 전까지 탈출하실 수 없습니다.");
                Console.WriteLine();
                Console.WriteLine("1. 들어간다");
                Console.WriteLine("0. 나간다\n");

                MyActionText();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BattleStart();
                        return;
                    default:
                        return;
                }
            }

        }
        private void AppearEnemy()
        {
            int currentDungeonLevel = gm.Player.Level; // 임시로 집어 넣음, 원래는 던전 난이도를 집어 넣어야함
            List<Enemy> originalEnemies = EnemyDataManager.instance.GetSpawnMonsters(currentDungeonLevel);  // 몬스터 데이터 매니저에서 몬스터 리스트 가져오기
            enemies = new List<Enemy>(originalEnemies.Select(e => new Enemy(e))); // 깊은 복사를 통해 리스트 복제

            foreach (var enemy in enemies)
            {
                Console.WriteLine($"{enemy.Name}가 나타났습니다!");
            }
        }

        public void BattleStart() // 전투 시작
        {
            isWin = false;
            isEnd = false;
            // 5.2 J => 전투 결과 창에서 불러올 수 있도록 함
            AppearEnemy();
            dungeonBattle(); // 전투 시작
        }

        private void dungeonBattle()
        {
            Console.Clear();

            while ((enemies.Any(e => e.Health > 0) && gm.Player.Health > 0))
            {
                BattleText();
                int targetIndex = ChooseEnemy();
                if (targetIndex == -1) continue;

                PlayerAction(enemies[targetIndex]);

                if (isEnd)
                {
                    break;
                }


                foreach (var enemy in enemies.Where(e => e.Health > 0))
                {
                    EnemyTurn(enemy);
                }
            }

            // 5.2 j => 배틀 재시작, 로비로 가기 수정
            EDungeonResultType dungeonResultType = isWin ? EDungeonResultType.VICTORY : EDungeonResultType.RETIRE;

            if(dungeonResultScreen.DungeonResultScreenOn(dungeonResultType, EDungeonDifficulty.NORMAL))
            {
                BattleStart();
            }
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

            Console.Write("\n>>  ");
            
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
            Console.WriteLine("\n행동을 선택하세요:");
            Console.WriteLine("1. 기본 공격");
            Console.WriteLine("2. 스킬 사용\n");
            MyActionText();


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
                BettleEnd(false);
                return;
            } 

            string attackResult = gm.Player.Attack(enemy);
            Console.WriteLine(attackResult);
            Thread.Sleep(2000);

            if (enemy.Health <= 0)
            {
                BettleEnd( true);
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
                BettleEnd(true);
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
                BettleEnd(true);
                return;
            }

            Console.WriteLine($"{enemy.Name}의 공격!");


            string attackResult = enemy.Attack(gm.Player);
            Console.WriteLine(attackResult);


            if (gm.Player.Health <= 0)
            {
                BettleEnd(false);
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

        private void BettleEnd(bool isWin)
        {
            isEnd = true;
            this.isWin = isWin;
        }
    }
}