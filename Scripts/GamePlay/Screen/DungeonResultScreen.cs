
namespace TextRPG
{
    public class DungeonResultScreen : Screen
    {
        private int prevHealth; // 던전 진행 전 체력
        private int prevExp; // 던전 진행 전 체력
        // private int prevLevel; // 던전 진행 전 Level

        private Reward reward;


        public override void ScreenOn()
        {
            if (gm.Dungeon.resultType != EDungeonResultType.RETIRE)
            {
                DungeonReward();
            }

            Console.Clear();

            while (true)
            {

                switch (gm.Dungeon.resultType)
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
                if (int.TryParse(Console.ReadLine(), out int input) && (input == 0 || (input == 1 && gm.Dungeon.resultType == EDungeonResultType.VICTORY)))
                {
                    Console.Clear();

                    if(input == 0) // 로비로 돌아갈 때 체력 및 마나 회복
                    {
                        gm.Player.RecoveryMana(gm.Player.MaxMana);
                        gm.Player.RecoveryHealth(gm.Player.MaxHealth);
                    }

                    playerInput = input; // Input 값 저장
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다! 숫자를 다시 입력하세요.\n");
                }

            }
        }
        private void DungeonReward()
        {
            reward = gm.Dungeon.GetDungeonReward();

            gm.Player.AddEquipItem(reward.rewardEquipItem);
            gm.Player.AddConsumableItem(reward.rewardConsumableItem.ItemName);
            gm.Player.Gold += reward.gold;
         //   gm.Player.ExpUp(reward.exp); 

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
            Console.WriteLine($"Lv.{gm.Player.Level} {gm.Player.Name}-> Lv.{gm.Player.Level} {gm.Player.Name}");
            Console.WriteLine($"HP {prevHealth} ->  {gm.Player.Health}");
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
