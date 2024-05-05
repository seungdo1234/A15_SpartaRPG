﻿
using System.ComponentModel.Design;

namespace TextRPG
{
    public class DungeonResultScreen : Screen
    {
        private int prevExp; // 던전 진행 전 체력
        private int prevLevel; // 던전 진행 전 Level

        private Reward reward;


        public override void ScreenOn()
        {
            playerInput = -1; // 5.5 A 플레이어 인풋 값 초기화

            if (gm.Dungeon.DungeonResultType != EDungeonResultType.RETIRE)
            {
                DungeonReward();
            }

            Console.Clear();

            while (true)
            {

                switch (gm.Dungeon.DungeonResultType)
                {
                    case EDungeonResultType.VICTORY:
                        VictoryText();
                        break;
                    case EDungeonResultType.RETIRE:
                        RetireText();
                        break;
                }
                MyActionText();

                // 1, 2, 3만 입력 받을 수 있게 함 
                if (int.TryParse(Console.ReadLine(), out int input) && (input == 0 || (input == 1 && gm.Dungeon.DungeonResultType == EDungeonResultType.VICTORY)))
                {
                    Console.Clear();

                    if(input == 0) // 로비로 돌아갈 때 체력 및 마나 회복
                    {
                        gm.Player.RecoveryMana(gm.Player.MaxMana);
                        gm.Player.RecoveryHealth(gm.Player.MaxHealth);

                        // 5.5 A 부활 전투 지속 현상 해결하기 위한, 로비돌아가기 처리 추가
                        LobbyScreen lobbyScreen = new LobbyScreen();
                        lobbyScreen.ScreenOn();
                    }

                    playerInput = input; // Input 값 저장

                    // 5.4 A : 배틀 계속이 아닌 로비로 돌아가는 현상 고치기 위해 넣음
                    if(input == 1 && gm.Dungeon.IsBossFightAvailable == false)
                    {
                        DungeonBattleScreen dungeonBattle = new DungeonBattleScreen();
                        dungeonBattle.BattleStart(); ; // 바로 다음 스테이지로 이동
                    }
                    // 5.5 A : 던전 보스 도전 조건 설정
                    else if(input == 1 && gm.Dungeon.IsBossFightAvailable == true)
                    {
                        DungeonBattleScreen dungeonBattle = new DungeonBattleScreen();
                        while (true) // 사용자가 유효한 선택을 할 때까지 반복
                        {
                            Console.Clear();
                            Console.WriteLine("보스전에 도전하시겠습니까?");
                            Console.WriteLine("1. 도전");
                            Console.WriteLine("0. 던전 입구로");
                            string choice = Console.ReadLine().ToUpper();

                            if (choice == "1")
                            {
                                dungeonBattle.TriggerBossBattle(); // 보스전 시작
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
                    //
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다! 숫자를 다시 입력하세요.\n");
                }

            }
        }
        private void DungeonReward() // 던전 보상
        {
            reward = gm.Dungeon.GetDungeonReward();

            gm.Player.AddEquipItem(reward.rewardEquipItem);
            if(reward.rewardConsumableItem != null)
            {
                gm.Player.AddConsumableItem(reward.rewardConsumableItem.ItemName);
            }
            gm.Player.Gold += reward.gold;
            prevLevel = gm.Player.Level;
            prevExp = gm.Player.Exp;
            gm.Player.ExpUp(gm.Dungeon.BattleExp);
        }
       
        private void TitleText()
        {
            Console.WriteLine();

            Console.WriteLine("Battle!! - Result !");

            Console.WriteLine();
        }

        private void VictoryText()
        {
            TitleText();

            Console.WriteLine("Victory !");
            Console.WriteLine();
            // 5.3 A GetSpawnMonsters에 인자 추가
            Console.WriteLine($"던전에서 몬스터 {EnemyDataManager.instance.GetSpawnMonsters(gm.Dungeon.CurrentDungeonLevel, EDungeonDifficulty.EASY).Count}마리를 잡았습니다.");
            Console.WriteLine();

            Console.WriteLine("[캐릭터 정보]");
            Console.WriteLine($"Lv.{prevLevel} {gm.Player.Name}-> Lv.{gm.Player.Level} {gm.Player.Name}");
            Console.WriteLine($"HP {gm.Dungeon.PrevHealth} ->  {gm.Player.Health}");
            Console.WriteLine($"exp {prevExp} ->  {gm.Player.Exp}");

            Console.WriteLine();

            Console.WriteLine("[획득 아이템]");
            Console.WriteLine($"{reward.gold} Gold");

            if (reward.rewardEquipItem != null)
            {
                Console.WriteLine($"{reward.rewardEquipItem.ItemName} x 1");
            }
            if (reward.rewardConsumableItem != null)
            {
                Console.WriteLine($"{reward.rewardConsumableItem.ItemName} x 1");
            }

            Console.WriteLine();

            Console.WriteLine("1. 다음 스테이지로");
            Console.WriteLine("0. 마을로 귀환하기");

            Console.WriteLine();
        }

        private void RetireText()
        {
            TitleText();

            Console.WriteLine("Retire");

            Console.WriteLine();

            Console.WriteLine($"클리어 실패 스테이지 : {gm.Dungeon.CurrentDungeonLevel}");

            Console.WriteLine();

            Console.WriteLine("몬스터의 일격에 당하셨습니다.");
            Console.WriteLine("눈 앞이 깜깜해집니다...");

            Console.WriteLine();

            Console.WriteLine("0. 마을로 귀환하기");

            Console.WriteLine();
        }
    }
}
