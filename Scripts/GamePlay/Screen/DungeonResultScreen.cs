
namespace TextRPG
{
    public class DungeonResultScreen : Screen
    {
        private Random rand;

        private int dif; // 던전의 난이도
        private int prevHealth; // 던전 진행 전 체력
        private int prevExp; // 던전 진행 전 체력
        private int prevGold; // 던전 진행 전 Gold
        private int prevLevel; // 던전 진행 전 Gold

        private Reward reward;
        public DungeonResultScreen()
        {
            rand = new Random();
        }

        public void DungeonResultScreenOn(DungeonDifficulty dif)
        {

            reward = gm.Dungeon.GetDungeonReward(dif);
            Console.Clear();

            while (true)
            {

                MyActionText();

                // 1, 2, 3만 입력 받을 수 있게 함 
                if (int.TryParse(Console.ReadLine(), out int input) && input == 0)
                {

                    Console.Clear();
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다! 숫자를 다시 입력하세요.\n");
                }

            }
        }

        // Text
        private void FailText()
        {
            Console.WriteLine();

            Console.WriteLine("던전 클리어 실패");
            Console.WriteLine("적이 너무 강했습니다...");
            Console.WriteLine($"{gm.Dungeon.GetDungeonName(dif)}을 클리어하지 못했습니다.");

            Console.WriteLine();

            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {prevHealth} -> {gm.Player.Health}");

            Console.WriteLine();

            Console.WriteLine("0. 나가기");
        }

        private void SuccessText()
        {
            Console.WriteLine();

            Console.WriteLine("Victory");
            Console.WriteLine();

            Console.WriteLine("던전에서 몬스터 n마리를 잡았습니다.");
            Console.WriteLine();

            Console.WriteLine("[캐릭터 정보]");
            Console.WriteLine($"Lv.{gm.Player.Level} {gm.Player.Name}-> Lv.{gm.Player.Level} {gm.Player.Name}");
            Console.WriteLine($"HP {prevHealth} ->  {gm.Player.Health}");
            Console.WriteLine($"exp {prevExp} ->  {gm.Player.Exp}");

            Console.WriteLine();

            Console.WriteLine("[획득 아이템]");
            Console.WriteLine($"{reward.gold} Gold");
            Console.WriteLine($"{reward.dungeonResultItem}");
            Console.WriteLine($"Gold {prevGold} G -> {gm.Player.Gold} G");

            Console.WriteLine();

            Console.WriteLine("0. 나가기");
        }
    }
}
