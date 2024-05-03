﻿using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace TextRPG
{
    public class DungeonBattleScreen : Screen
    {
        private List<Enemy> enemies;  // 여러 몬스터를 저장할 리스트
        private bool isWin;
        private int winCounter = 0;  // 승리 횟수 카운터
        private bool returnToChooseEnemy = false; // 스킬 예외처리
        private EDungeonDifficulty selectedDifficulty = EDungeonDifficulty.NORMAL; // 난이도 반환

        private DungeonResultScreen dungeonResultScreen;



        public DungeonBattleScreen()
        {
            dungeonResultScreen = new DungeonResultScreen();
            enemies = new List<Enemy>(); ;  // 몬스터를 저장할 리스트 초기화
        }

        public override void ScreenOn()
        {
            while (true)
            {
                winCounter = 0;  // 게임 시작 시 승리 카운터 초기화

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

                    case "0":
                        return;

                    default:
                        SystemMessageText(EMessageType.ERROR);
                        continue;
                }
            }
        }
        private void AppearEnemy()
        {
            int currentDungeonLevel = gm.Player.Level; // 임시로 집어 넣음, 원래는 던전 난이도를 집어 넣어야함

            // 몬스터 데이터 매니저에서 몬스터 리스트 가져오기, 5.3 A : 배수 증가 매게변수 추가
            enemies = EnemyDataManager.instance.GetSpawnMonsters(currentDungeonLevel, selectedDifficulty); 
        }

        // 5.3 A : 던전 난이도 확인 추가
        private void CheckForDifficulty()
        {
            Console.WriteLine();
            Console.WriteLine("난이도를 선택하세요:");
            Console.WriteLine("1. 쉬움 (EASY)");
            Console.WriteLine("2. 보통 (NORMAL)");
            Console.WriteLine("3. 어려움 (HARD)");
            Console.Write("\n선택: ");
            string input = Console.ReadLine();

            //5.3 A : 던전 난이도 Enum활용
            switch (input)
            {
                case "1":
                    selectedDifficulty = EDungeonDifficulty.EASY;
                    break;
                case "2":
                    selectedDifficulty = EDungeonDifficulty.NORMAL;
                    break;
                case "3":
                    selectedDifficulty = EDungeonDifficulty.HARD;
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 보통 난이도로 개시합니다.");
                    selectedDifficulty = EDungeonDifficulty.NORMAL;
                    break;
            }
            Console.WriteLine($"선택된 난이도: {selectedDifficulty}");
        }

        public void BattleStart() // 전투 시작
        {
            CheckForDifficulty();
            AppearEnemy();
            dungeonBattle();
        }

        private void dungeonBattle()
        {
            while ((enemies.Any(e => e.Health > 0) && gm.Player.Health > 0))
            {
                winCounter++;
                int targetIndex = ChooseEnemy();
                if (targetIndex == -1) continue;

                int actionResult = PlayerAction(enemies[targetIndex]);
                if (actionResult == -1) continue;

                if (!enemies.Any(e => e.Health > 0))
                {
                    winCounter++;  // 승리 카운터 증가
                    BattleEnd(true);  // 모든 적이 사망했으므로 승리 처리
                    return;
                }

                foreach (var enemy in enemies.Where(e => e.Health > 0))
                {
                    EnemyTurn(enemy);

                    if (gm.Player.Health <= 0)  // 플레이어가 사망한 경우, 패배 처리
                    {
                        BattleEnd(false);
                    }
                }
            }
        }

        private int ChooseEnemy()
        {
            while (true)
            {
                BattleLogText();
                MyActionText();
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int selected))
                {
                    SystemMessageText(EMessageType.ERROR);

                    continue;
                }

                selected -= 1;

                if (selected < 0 || selected >= enemies.Count || enemies[selected].Health <= 0)
                {
                    SystemMessageText(EMessageType.ERROR);

                    continue;
                }

                return selected; // 유효한 선택일 경우 선택한 몬스터의 인덱스를 반환
            }
        }

        private int PlayerAction(Enemy enemy)
        {
            while (true)
            {
                BattleLogText();
                Console.WriteLine("\n행동을 선택하세요:");
                Console.WriteLine("0. 다른 적 선택");
                Console.WriteLine("1. 기본 공격");
                Console.WriteLine("2. 스킬 사용");
                Console.WriteLine();
                MyActionText();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        return -1;  // 다른 적을 선택하게 하기 위해 특별한 값을 반환
                    case "1":
                        PlayerTurn(enemy);
                        return 0;
                    case "2":
                        UseSkill(enemy);
                        if (returnToChooseEnemy)
                        {
                            returnToChooseEnemy = false; // 상태 초기화
                            return -1; // 다른 적을 선택하도록 플로우 변경
                        }
                        return 0;
                    default:
                        SystemMessageText(EMessageType.ERROR);
                        continue;
                }
            }
        }

        private void PlayerTurn(Enemy enemy)
        {
            // 선택한 몬스터의 이름을 포함하여 공격 메시지 출력
            Console.WriteLine($"{gm.Player.Name}의 {enemy.Name}를 향한 공격!");

            string attackResult = gm.Player.Attack(enemy);
            Console.WriteLine(attackResult);
            Thread.Sleep(2000);

            if (enemy.Health <= 0)
            {
                Console.WriteLine($"[{enemy.Name}이(가) 쓰러졌습니다.]");
            }

        }

        private void UseSkill(Enemy enemy)
        {
            Console.WriteLine();
            Console.WriteLine("사용할 스킬을 선택하세요 (0을 누르면 다른 적 선택):");
            for (int i = 0; i < gm.Player.Phase; i++) // 05.03 W > Phase : 해금되는 스킬의 갯수
            {
                var skill = gm.Player.Skills[i];
                Console.WriteLine($"{i + 1}. {skill.Name} (MP: {skill.ManaCost}) - {skill.Content}");
            }

            Console.WriteLine();
            MyActionText();
            int inputLine = Console.CursorTop; // 현재 입력 줄 위치 저장

            while (true)
            {
                Console.SetCursorPosition(3, inputLine); // 커서를 입력 줄의 시작 위치로 설정
                string input = Console.ReadLine();
                if (input == "0")
                {
                    returnToChooseEnemy = true;
                    return; // 다른 적을 선택하도록 하기 위해 메서드 종료
                }

                if (!int.TryParse(input, out int selectedSkillIndex) || selectedSkillIndex < 1 || selectedSkillIndex > gm.Player.Phase) // 05.03 W Skill.Count > Phase
                {
                    SystemMessageText(EMessageType.ERROR);
                    continue;
                }

                selectedSkillIndex -= 1;
                Skill selectedSkill = gm.Player.Skills[selectedSkillIndex];
                if (gm.Player.Mana < selectedSkill.ManaCost)
                {
                    SystemMessageText(EMessageType.MANALESS);
                    continue;
                }

                gm.Player.CostMana(selectedSkill.ManaCost);
                UseSelectedSkill(selectedSkill, enemy);
                break; // 스킬 사용 후 정상 종료
            }
        }

        private void UseSelectedSkill(Skill skill, Enemy target)
        {
            if (skill.IsMultiTarget)
            {
                Console.WriteLine("다중 대상 스킬 사용 중...");

                // 첫 번째로 지정된 타겟에 스킬 적용
                Console.WriteLine($"{gm.Player.Name}의 {target.Name}을(를) 향한 공격!");
                string initialSkillResult = skill.CastSkill(gm.Player, target);
                Console.WriteLine(initialSkillResult);
                Thread.Sleep(1500);

                // 나머지 타겟들에게 스킬 적용
                int targetsHit = 1; // 첫 번째 타겟이 이미 공격받았으므로 1로 시작
                foreach (var enemy in enemies.Where(e => e.Health > 0 && e != target))
                {
                    if (targetsHit >= skill.MaxTargetCount)
                        break;

                    Console.WriteLine($"이어지는 {enemy.Name}을(를) 향한 공격!");
                    string skillResult = skill.CastSkill(gm.Player, enemy);
                    Console.WriteLine(skillResult);
                    Thread.Sleep(1500);

                    targetsHit++;
                }
            }
            else
            {
                // 단일 대상 스킬 사용
                Console.WriteLine($"{gm.Player.Name} {target.Name}을 향한 공격!");
                string skillResult = skill.CastSkill(gm.Player, target);
                Console.WriteLine(skillResult);
                Thread.Sleep(2000);
            }
            Console.Clear();
        }


        private void EnemyTurn(Enemy enemy)
        {
            Console.WriteLine($"{enemy.Name}의 공격!");


            string attackResult = enemy.Attack(gm.Player);
            Console.WriteLine(attackResult);

            Thread.Sleep(1500);
        }

        private void TriggerBossBattle()
        {
            Console.WriteLine("보스가 등장했습니다!");
            Enemy boss = EnemyDataManager.instance.GetBoss();  // 보스 데이터 가져오기
            enemies.Clear();
            enemies.Add(boss);  // 현재 전투 몬스터 리스트에 보스 추가
            dungeonBattle();

        }


        private void BattleEnd(bool isWin)
        {
            if (isWin)
            {
                gm.Dungeon.resultType = EDungeonResultType.VICTORY;
                if (winCounter >= 10) // 10번 승리 후 보스전 조건 체크
                {
                    while (true)  // 사용자가 유효한 선택을 할 때까지 반복
                    {
                        Console.Clear() ;
                        Console.WriteLine("보스전에 도전하시겠습니까?");
                        Console.WriteLine("1. 도전");
                        Console.WriteLine("0. 던전 입구로");
                        string choice = Console.ReadLine().ToUpper();

                        if (choice == "1")
                        {
                            TriggerBossBattle(); // 보스전 시작
                            winCounter = 0; // 승리 카운터 초기화
                            return;
                        }
                        else if (choice == "0")
                        {
                            dungeonResultScreen.ScreenOn(); // 결과 화면으로 이동
                            return;
                        }
                        else
                        {
                            SystemMessageText(EMessageType.ERROR);
                        }
                    }
                }
                else
                {
                    dungeonResultScreen.ScreenOn(); // 조건 미충족 시 결과 화면으로 이동
                }
            }
            else
            {
                gm.Dungeon.resultType = EDungeonResultType.RETIRE;
                dungeonResultScreen.ScreenOn(); // 패배 시 결과 화면으로 이동
            }
        }

        private void BattleLogText()
        {
            Console.Clear();
            Console.WriteLine("=== 전투 중인 몬스터 목록 ===");
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].Health > 0)
                {
                    Console.WriteLine($"{i + 1}. {enemies[i].Name} (HP: {enemies[i].Health}/{enemies[i].MaxHealth}) ATK: {enemies[i].Atk}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("=== 내 정보 ===");
            Console.WriteLine($"Lv.{gm.Player.Level} {gm.Player.Name} ({gm.Player.GetPlayerClass(gm.Player.ePlayerClass)})");
            Console.WriteLine($"HP {gm.Player.Health}/{gm.Player.MaxHealth}");
            Console.WriteLine($"MP {gm.Player.Mana}/{gm.Player.MaxMana}");
            Console.WriteLine();
            Console.WriteLine("\n공격할 몬스터를 선택하세요:\n");
        }
    }
}