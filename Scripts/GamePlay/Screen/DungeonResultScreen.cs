namespace TextRPG
{
    public class DungeonResultScreen : Screen
    {
        private int prevExp; // 던전 진행 전 체력
        private int prevLevel; // 던전 진행 전 Level

        private Reward reward;

        private EquipItem panaltyEquipItem;
        private int panaltyGold;

        public void RewardInit() // 보상 초기화, 던전에 입장할 때 호출
        {
            gm.Dungeon.RewardInit();
            panaltyEquipItem = null;
            panaltyGold = 0;
        }

        private void GameOverPanalty() // 게임 오버 시 패널티
        {
            if (gm.Dungeon.PlayerRewards.rewardEquipItems.Count != 0)
            {
                panaltyEquipItem = dm.GetRandomEquipItem(gm.Dungeon.PlayerRewards.rewardEquipItems);
                gm.Player.AddEquipItem(panaltyEquipItem);
                panaltyGold = gm.Dungeon.PlayerRewards.totalGold * 20 / 100;
                gm.Player.Gold += panaltyGold;
            }
        }
        public override void ScreenOn()
        {

            if (gm.Dungeon.DungeonResultType != EDungeonResultType.RETIRE)
            {
                DungeonReward();
            }
            else
            {
                GameOverPanalty();
            }

            Console.Clear();

            while (true)
            {
                Console.Clear();

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

                    if (input == 0) // 로비로 돌아갈 때 체력 및 마나 회복
                    {
                        gm.Player.RecoveryMana(gm.Player.MaxMana);
                        gm.Player.RecoveryHealth(gm.Player.MaxHealth);
                        if(gm.Dungeon.DungeonResultType == EDungeonResultType.VICTORY)
                        {
                            SetDungeonReward();
                        }
                    }

                    playerInput = input; // Input 값 저장

                    // 5.4 A : 배틀 계속이 아닌 로비로 돌아가는 현상 고치기 위해 넣음
                    if (input == 1)
                    {
                        DungeonBattleScreen dungeonBattle = new DungeonBattleScreen();
                        dungeonBattle.BattleStart(); ; // 바로 다음 스테이지로 이동
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
        private void DungeonReward() // 던전 보상 (골드 및 장비 아이템 X)
        {
            reward = gm.Dungeon.GetDungeonReward();

            if (reward.rewardConsumableItem != null)
            {
                gm.Player.AddConsumableItem(reward.rewardConsumableItem.ItemName);
            }
            prevLevel = gm.Player.Level;
            prevExp = gm.Player.Exp;
            gm.Player.ExpUp(gm.Dungeon.BattleExp);
        }
        private void SetDungeonReward() // 골드 및 장비 아이템 획득
        {
            gm.Player.Gold += gm.Dungeon.PlayerRewards.totalGold;
            
            for(int i = 0; i < gm.Dungeon.PlayerRewards.rewardEquipItems.Count; i++)
            {
                gm.Player.AddEquipItem(gm.Dungeon.PlayerRewards.rewardEquipItems[i]);
            }
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

            Console.WriteLine("[던전 보상]");
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

            if (panaltyEquipItem != null)
            {
                Console.WriteLine();
                Console.WriteLine("던전에서 얻은 대부분의 골드와 장비를 잃었습니다.");

                Console.WriteLine("[던전 보상]");
                Console.WriteLine($"{panaltyGold} Gold");
                Console.WriteLine($"{panaltyEquipItem.ItemName} x 1");
            }

            Console.WriteLine();

            Console.WriteLine("몬스터의 일격에 당하셨습니다.");
            Console.WriteLine("눈 앞이 깜깜해집니다...");

            Console.WriteLine();

            Console.WriteLine("0. 마을로 귀환하기");

            Console.WriteLine();
        }
    }
}
