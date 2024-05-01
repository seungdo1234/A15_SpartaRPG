
namespace TextRPG
{
    public class DungeonResultScreen : Screen
    {

        private EDungeonDifficulty dif; // 던전의 난이도
        private int prevHealth; // 던전 진행 전 체력
        private int prevExp; // 던전 진행 전 체력
        // private int prevLevel; // 던전 진행 전 Level

        private Reward reward;


        public void DungeonResultScreenOn(EDungeonResultType resultType , EDungeonDifficulty dif)
        {
            if (resultType != EDungeonResultType.RETIRE)
            {
                this.dif = dif;
                DungeonReward();
            }

            Console.Clear();

            while (true)
            {

                switch (resultType)
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
                if (int.TryParse(Console.ReadLine(), out int input) && (input == 0 || (input == 1 && resultType == EDungeonResultType.VICTORY)))
                {
                    if (input == 0) // 되돌아 가기
                    {
                        return;
                    }
                    else // 계속
                    {
                        // 코드 작성해야함
                    }
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다! 숫자를 다시 입력하세요.\n");
                }

            }
        }
        private void DungeonReward()
        {
            reward = gm.Dungeon.GetDungeonReward(dif);

            dm.DungeonDropItem(reward.dungeonRewardItem);
            gm.Player.Gold += reward.gold;
            gm.Player.ExpUp(reward.exp); 

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

            Console.WriteLine($"던전에서 몬스터 {gm.Dungeon.GetMonsterEncount(gm.Dungeon.CurrentDungeonLevel).Count}마리를 잡았습니다.");
            Console.WriteLine();

            Console.WriteLine("[캐릭터 정보]");
            Console.WriteLine($"Lv.{gm.Player.Level} {gm.Player.Name}-> Lv.{gm.Player.Level} {gm.Player.Name}");
            Console.WriteLine($"HP {prevHealth} ->  {gm.Player.Health}");
            Console.WriteLine($"exp {prevExp} ->  {gm.Player.Exp}");

            Console.WriteLine();

            Console.WriteLine("[획득 아이템]");
            Console.WriteLine($"{reward.gold} Gold");

            if (reward.dungeonRewardItem != null)
            {
                Console.WriteLine($"{reward.dungeonRewardItem.ItemName} x 1");
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
