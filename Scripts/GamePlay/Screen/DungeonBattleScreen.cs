using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using TextRPG.Scripts;

namespace TextRPG
{
    public class DungeonBattleScreen : Screen
    {
        private List<Enemy> enemies;  // 여러 몬스터를 저장할 리스트
        public int enemyNum;
        private bool returnToChooseEnemy = false; // 스킬 예외처리
        private bool BossClear = false; // 5.5 A 보스 클리어 여부
        private CreditScreen creditScreen; // 5.5 A 보스 클리어 추가, 크레딧 BattleEnd에 연결함
        private Dictionary<Enemy, bool> skillWarnings = new Dictionary<Enemy, bool>(); // 5.6 A 스킬 발동 확률을 위한 변수


        private static DungeonBattleScreen instance;


        private DungeonResultScreen dungeonResultScreen;      

        public static DungeonBattleScreen Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DungeonBattleScreen();
                }

                return instance;
            }
        }

        public DungeonBattleScreen()
        {
            dungeonResultScreen = new DungeonResultScreen();
            enemies = new List<Enemy>(); ;  // 몬스터를 저장할 리스트 초기화
            creditScreen = new CreditScreen();
        }

        public override void ScreenOn()
        {
            while (true)
            {
                Console.Clear();
                PrintTitle("스파르타 던전 입구");

                Console.WriteLine("\n정말 던전에 진입하시겠습니까? ");
                PrintWarning("스테이지를 클리어하거나, 죽기 전까지 탈출하실 수 없습니다.\n\n");
                Console.WriteLine("1. 들어간다");
                Console.WriteLine("0. 나간다\n");

                MyActionText();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        gm.Dungeon.DungeonInit();
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
            // 몬스터 데이터 매니저에서 몬스터 리스트 가져오기, 5.3 A : 배수 증가 매개변수 추가
            enemies = EnemyDataManager.instance.GetSpawnMonsters(gm.Dungeon.CurrentDungeonLevel, gm.Dungeon.dif);

            enemyNum = enemies.Count;
        }

        // 5.3 A : 던전 난이도 확인 추가
        private void CheckForDifficulty()
        {
            int input;

            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("난이도를 선택하세요:");
                Console.WriteLine("난이도 별로 시작 층수가 상이합니다.\n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1. 쉬움 (EASY)");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("2. 보통 (NORMAL)");
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("3. 어려움 (HARD)");
                Console.ResetColor();
                Console.Write("\n선택: ");


                if (int.TryParse(Console.ReadLine(), out input) && input >= 1 && input <= 3)
                {
                    gm.Dungeon.dif = (EDungeonDifficulty)input;
                    gm.Dungeon.DungeonLevelUp();
                    break;
                }
                else
                {
                    SystemMessageText(EMessageType.ERROR);
                }
            }

            Console.Write($"선택된 난이도:");
            PrintNotice(gm.Dungeon.dif.ToString());
            Console.WriteLine();
            gm.Dungeon.PrevHealth = gm.Player.Health;
        }

        public void BattleStart() // 전투 시작, 5.4 A 결과창 보스전 순서 조정을 위한, 보스전 트리거 BattleStart로 이동
        {
            Console.Clear();

            if (playerInput == 1)
            {
                if (gm.Dungeon.CurrentDungeonLevel >= 10)
                {
                    while (true) // 사용자가 유효한 선택을 할 때까지 반복
                    {
                        Console.Clear();
                        Console.WriteLine("당신은 어느덧 던전의 가장 깊숙한 곳에 다다랐습니다.");
                        Thread.Sleep(500);
                        Console.WriteLine("거대한 위압감이 느껴지는 문이 당신의 눈 앞에 보입니다");
                        Thread.Sleep(500);
                        Console.Write(".");
                        Thread.Sleep(500);
                        Console.Write(".");
                        Thread.Sleep(500);
                        Console.WriteLine(".");
                        Thread.Sleep(1000);

                        PrintWarning("보스전");
                        Console.WriteLine(" 에 도전하시겠습니까?\n");
                        Console.WriteLine("1. 도전");
                        Console.WriteLine("0. ...포기한다.");
                        string choice = Console.ReadLine().ToUpper();

                        if (choice == "1")
                        {
                            TriggerBossBattle(); // 보스전 시작
                            return;
                        }
                        else if (choice == "0")
                        {
                            return; // 던전 입구로 돌아갈 경우 추가 처리
                        }
                        else
                        {
                            SystemMessageText(EMessageType.ERROR);
                        }
                    }
                }
            }

            CheckForDifficulty();

            //던전 진입 로딩화면
            PrintLogo();
            Console.Write($"스파르타 던전, ");
            PrintNotice(gm.Dungeon.CurrentDungeonLevel.ToString());
            Console.Write(" 층으로 진입합니다");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.WriteLine(".");
            Thread.Sleep(1000);
            Console.Clear();

            AppearEnemy();
            dungeonBattle();
        }

        private void dungeonBattle()
        {
            gm.Player.DispelAllDebuff(); // 05.06 W 새로운 스테이지 시 디버프 초기화
            while ((enemies.Any(e => e.Health > 0) && gm.Player.Health > 0))
            {
                int targetIndex = ChooseEnemy();
                if (targetIndex == -1) continue;

                int actionResult = PlayerAction(enemies[targetIndex]);
                if (actionResult == -1) continue;                    

                if (!enemies.Any(e => e.Health > 0))
                {
                    BattleEnd(true);  // 모든 적이 사망했으므로 승리 처리
                    return;
                }

                gm.Player.OnDebuffActive(); // 05.06 W 플레이어 디버프 표시

                foreach (var enemy in enemies.Where(e => e.Health > 0))
                {   
                    if (enemy.CheckCrowdControl(ECrowdControlType.STUN)) // 05.05 W 몬스터 스턴 적용
                    {
                        PrintNotice($"{enemy.Name}는 아직 정신을 못차리고 있다..\n");
                        enemy.OnDebuffActive();
                        continue;
                    }
                    enemy.OnDebuffActive();
                    EnemyTurn(enemy);

                    if (gm.Player.Health <= 0)  // 플레이어가 사망한 경우, 패배 처리
                    {
                        BattleEnd(false);
                        return;
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
                Console.WriteLine("3. 아이템 사용");
                Console.WriteLine();
                MyActionText();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        return -1;  // 다른 적을 선택하게 하기 위해 특별한 값을 반환
                    case "1":
                        if (gm.Player.CheckCrowdControl(ECrowdControlType.STUN)) // 05.06 W 플레이어 스턴 적용
                        {
                            PrintWarning($"머리가 어지러워 당신은 공격을 할 수 없었습니다...\n");
                        }
                        else PlayerTurn(enemy);
                        return 0;
                    case "2":
                        if (gm.Player.CheckCrowdControl(ECrowdControlType.STUN)) // 05.06 W 플레이어 스턴 적용
                        {
                            PrintWarning($"머리가 어지러워 당신은 스킬을 사용할 수 없었습니다...\n");
                        }
                        else if (gm.Player.CheckCrowdControl(ECrowdControlType.SILENCE)) // 05.05 w 플레이어 침묵 적용
                        {
                            PrintWarning("스킬을 외치려 했으나, 당신의 입은 떨어지지 않습니다.\n");
                        }
                        else UseSkill(enemy);
                        if (returnToChooseEnemy)
                        {
                            returnToChooseEnemy = false; // 상태 초기화
                            return -1; // 다른 적을 선택하도록 플로우 변경
                        }
                        return 0;
                    case "3":
                        if (!UsePotion())
                        {
                            continue;  // 포션 사용 취소시, 다시 행동 선택
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
            PrintName(gm.Player.Name);
            Console.Write(" 은(는) ");
            PrintNotice(enemy.Name);
            Console.WriteLine("을(를) 향해 공격을 날렸습니다!");

            
            string attackResult = gm.Player.Attack(enemy);
            Console.WriteLine(attackResult);
            Thread.Sleep(2000);

            if (enemy.Health <= 0)
            {
                PrintNotice($"[{enemy.Name}이(가) 쓰러졌습니다!!]");
                Console.WriteLine();
                Thread.Sleep(750);
                gm.QuestManager.SetMonsterQuest(enemy);
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
                
                string input = Console.ReadLine();
                if (input == "0")
                {
                    returnToChooseEnemy = true;
                    return; // 다른 적을 선택하도록 하기 위해 메서드 종료
                }

                if (!int.TryParse(input, out int selectedSkillIndex) || selectedSkillIndex < 1 || selectedSkillIndex > gm.Player.Phase) // 05.03 W Skill.Count > Phase
                {
                    SystemMessageText(EMessageType.ERROR);
                    Console.SetCursorPosition(3, inputLine); // 커서를 입력 줄의 시작 위치로 설정
                    Console.WriteLine("                                 ");
                    Console.WriteLine();
                    Console.WriteLine("                                 ");
                    Console.SetCursorPosition(3, inputLine); // 커서를 입력 줄의 시작 위치로 설정
                    continue;
                }

                selectedSkillIndex -= 1;
                Skill selectedSkill = gm.Player.Skills[selectedSkillIndex];
                if (gm.Player.Mana < selectedSkill.ManaCost)
                {
                    SystemMessageText(EMessageType.MANALESS);
                    Console.SetCursorPosition(3, inputLine); // 커서를 입력 줄의 시작 위치로 설정
                    Console.WriteLine("                                 ");
                    Console.WriteLine();
                    Console.WriteLine("                                 ");
                    Console.SetCursorPosition(3, inputLine); // 커서를 입력 줄의 시작 위치로 설정
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
                // 첫 번째로 지정된 타겟에 스킬 적용
                PrintName(gm.Player.Name);
                Console.Write(" 의 스킬이 ");
                PrintNotice(target.Name);
                Console.Write(" 에게 적중했습니다!");
                string initialSkillResult = skill.CastSkill(gm.Player, target);
                Console.WriteLine(initialSkillResult);
                Thread.Sleep(1500);

                // 5.5 A 스킬 공격시 사망에도 퀘스트 카운팅 추적
                if (target.Health <= 0)
                {
                    Console.Write("[");
                    PrintNotice(target.Name);
                    Console.WriteLine(" 이(가) 쓰러졌습니다.]");
                    Thread.Sleep(750);
                    gm.QuestManager.SetMonsterQuest(target);
                }

                // 나머지 타겟들에게 스킬 적용
                int targetsHit = 1; // 첫 번째 타겟이 이미 공격받았으므로 1로 시작
                foreach (var enemy in enemies.Where(e => e.Health > 0 && e != target))
                {
                    if (targetsHit >= skill.MaxTargetCount)
                        break;

                    Console.Write("이어서");
                    PrintNotice(enemy.Name);
                    Console.WriteLine("에게 스킬이 적중했습니다!");
                    string skillResult = skill.CastSkill(gm.Player, enemy);
                    Console.WriteLine(skillResult);
                    Thread.Sleep(1500);

                    // 5.5 A 만약 나머지 타겟의 체력이 0 이하라면 퀘스트 진행 업데이트
                    if (enemy.Health <= 0)
                    {
                        Console.Write("[");
                        PrintNotice(enemy.Name);
                        Console.WriteLine(" 이(가) 쓰러졌습니다.]");
                        Thread.Sleep(750);
                        gm.QuestManager.SetMonsterQuest(enemy);
                    }

                    targetsHit++;
                }
            }
            else
            {
                // 단일 대상 스킬 사용
                PrintName(gm.Player.Name);
                Console.Write(" 은 ");
                PrintNotice(target.Name);
                Console.WriteLine("을(를) 공격했습니다!");
                string skillResult = skill.CastSkill(gm.Player, target);
                Console.WriteLine(skillResult);
                Thread.Sleep(2000);
                // 5.5 A 퀘스트 추적
                if (target.Health <= 0)
                {
                    Console.Write("[");
                    PrintNotice(target.Name);
                    Console.WriteLine(" 이(가) 쓰러졌습니다.]");
                    Thread.Sleep(750);
                    gm.QuestManager.SetMonsterQuest(target);
                }
            }
            Console.Clear();
        }


        private void EnemyTurn(Enemy enemy)
        {
            Console.Clear();

            // 5.7 A 같은 몬스터더라도 번호를 통해 구별할 수 있도록 수정
            int enemyIndex = enemies.IndexOf(enemy) + 1;
            // 5.6 A 적의 스킬 경고 문구 및 확률 발동 코드, 시작
            if (skillWarnings.ContainsKey(enemy) && skillWarnings[enemy])
            {
                PrintNotice(enemy.Name);
                Console.Write(" 는 스킬, ");
                PrintWarning(enemy.Skills[0].Name);
                Console.WriteLine(" 을(를) 사용했다!");
                string attackResult = enemy.Skills[0].CastSkill(enemy, gm.Player);
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(attackResult);
                Console.ResetColor();

                // 스킬 발동 후, 경고 상태 초기화
                skillWarnings[enemy] = false;

                // 대기 시간 추가
                Thread.Sleep(1500);

                return;
            }

            // 5.6 A : 50%확률로 수행 테스트 용이로 50% 설정, 추후 조정 가능
            bool shouldWarn = new Random().NextDouble() < 0.5;

            if (shouldWarn)
            {
                // 적이 경고를 한다면, 경고 메시지만 출력하고 아무 행동도 하지 않습니다.
                PrintWarning($"{enemyIndex}. {enemy.Name}의 동태가 심상치 않습니다!");
                Console.WriteLine();
                Thread.Sleep(1500);

                // 경고 상태를 true로 설정
                skillWarnings[enemy] = true;
                return;
            }

            // 경고 상태가 아니므로 일반 공격을 수행
            PrintNotice(enemy.Name);
            Console.WriteLine(" 이(가) 당신을 공격합니다!");

            string normalAttackResult = enemy.Attack(gm.Player);
            Console.WriteLine(normalAttackResult);

            // 대기 시간 추가
            Thread.Sleep(1500);
        }

        public void TriggerBossBattle() // 5.5 A private > public
        {
            BossClear = true;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("불길한 기운이 도사리고 있습니다...");
            Thread.Sleep(500);
            Console.WriteLine("거대한 존재의 기척이 느껴집니다.");
            Thread.Sleep(1000);
            Console.ResetColor();
            Enemy boss = EnemyDataManager.instance.GetBoss();  // 보스 데이터 가져오기
            enemies.Clear();
            enemies.Add(boss);  // 현재 전투 몬스터 리스트에 보스 추가
            dungeonBattle();
        } 


        private void BattleEnd(bool isWin)
        {
            gm.Dungeon.DungeonResultType = isWin ? EDungeonResultType.VICTORY : EDungeonResultType.RETIRE;
            if (BossClear == true)
            {
                Console.WriteLine("오랜 시간, 스파르타 던전을 지배하던 발록이 사라졌습니다.");
                Thread.Sleep(500);
                Console.WriteLine("...이제 스파르타 마을은 안전해진 걸까요?\n");
                Thread.Sleep(1000);
                Console.WriteLine(".");
                Thread.Sleep(500);
                Console.WriteLine(".");
                Thread.Sleep(500);
                Console.WriteLine(".");
                Thread.Sleep(500);
                creditScreen.ScreenOn();
            }

            dungeonResultScreen.ScreenOn();
        }

        private void BattleLogText()
        {
            Console.Clear();
            PrintNotice($"{gm.Dungeon.CurrentDungeonLevel} 스테이지");
            Console.WriteLine("\n");

            PrintTitle("=== 전투 중인 몬스터 목록 ===");
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].Health > 0)
                {
                    Console.Write($"{i + 1}. {enemies[i].Name} (HP: {enemies[i].Health}/{enemies[i].MaxHealth}) ATK: {enemies[i].Atk}");
                    PrintCrowdControl(enemies[i]);
                }
            }

            Console.WriteLine();
            PrintTitle("=== 내 정보 ===");
            Console.Write("Lv.");
            PrintNotice(gm.Player.Level.ToString());
            PrintName(" " + gm.Player.Name);
            Console.Write(" (");
            PrintNotice(gm.Player.GetPlayerClass(gm.Player.ePlayerClass));
            Console.Write(")");
            PrintCrowdControl(gm.Player);

            Console.Write("HP ");
            PrintNotice($"{gm.Player.Health}/{gm.Player.MaxHealth}");
            Console.WriteLine();
            Console.Write("MP ");
            PrintNotice($"{gm.Player.Mana}/{gm.Player.MaxMana}");

            Console.WriteLine("\n");
            Console.WriteLine("\n공격할 몬스터를 선택하세요:\n");
        }

        private ConsumableItem SelectPotion()
        {
            int index = 1;
            Dictionary<int, ConsumableItem> potionOptions = new Dictionary<int, ConsumableItem>();

            // 사용 가능한 포션 목록 출력
            foreach (var item in gm.Player.PlayerConsumableItems)
            {
                ConsumableItem potion = dm.ConsumableItemDB.Find(p => p.ItemName == item.Key);
                if (potion != null && item.Value > 0)
                {
                    Console.Write($"{index}: {potion.ItemName} - {potion.Desc} 보유량: ");
                    PrintNotice(item.Value.ToString());
                    Console.WriteLine();

                    potionOptions[index] = potion;
                    index++;
                }
            }

            while (true)
            {
                Console.WriteLine("번호를 입력하세요 (0으로 취소):");
                string input = Console.ReadLine();
                // 취소 조건 확인
                if (input == "0")
                {
                    return null; // 취소 선택
                }

                // 올바른 숫자 입력 확인
                if (int.TryParse(input, out int choice) && potionOptions.ContainsKey(choice))
                {
                    return potionOptions[choice];
                }

                SystemMessageText(EMessageType.ERROR);
            }
        }

        private bool UsePotion()
        {
            ConsumableItem selectedPotion = null;

            while (true)
            {
                selectedPotion = SelectPotion();
                if (selectedPotion != null)
                {
                    // 포션 사용 조건 검사
                    if ((selectedPotion.ConsumableType == EConsumableType.HEALTH && GameManager.instance.Player.Health < GameManager.instance.Player.MaxHealth) ||
                        (selectedPotion.ConsumableType == EConsumableType.MANA && GameManager.instance.Player.Mana < GameManager.instance.Player.MaxMana))
                    {
                        selectedPotion.UseItem();
                        Console.WriteLine($"{selectedPotion.ItemName} 사용: {selectedPotion.Desc}");
                        return true;  // 성공적으로 포션 사용
                    }
                    else
                    {
                        SystemMessageText(EMessageType.FULLCONDITION);
                        // 포션 사용 조건이 충족되지 않았으므로 계속 포션 선택을 유도
                    }
                }
                else
                {
                    Console.WriteLine("포션 선택이 취소되었습니다.");
                    return false;  // 포션 사용 취소
                }
            }
        }
    }
}