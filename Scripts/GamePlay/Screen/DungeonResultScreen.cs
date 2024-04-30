
namespace TextRPG
{
    public class DungeonResultScreen : Screen
    {
        private Random rand;

        private int dif; // 던전의 난이도
        private int prevHealth; // 던전 진행 전 체력
        private int prevGold; // 던전 진행 전 Gold
        private int prevLevel; // 던전 진행 전 Gold

        public DungeonResultScreen()
        {
            rand = new Random();
        }

        public void DungeonResultScreenOn(int dif)
        {
            this.dif = dif;
            bool isClear = DungeonClear();


            Console.Clear();

            while (true)
            {
                if (isClear) // 클리어 여부
                {
                    SuccessText();
                }
                else
                {
                    FailText();
                }

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


        private bool DungeonClear() // 던전 클리어 여부 판별 함수
        {
            prevHealth = gm.Player.Health; // 현재 체력 저장

            if (gm.Player.Def >= gm.Dungeon.GetRecommandedDef(dif)) // 클리어
            {
                DungeonClearReward();
                return true;
            }
            else
            {
                int per = rand.Next(0, 101);

                if (per <= 40) // 실패
                {
                    gm.Player.OnDamaged(prevHealth / 2);
                    return false;
                }
                else // 클리어
                {
                    DungeonClearReward();
                    return true;
                }
            }
        }

        private void DungeonClearReward() // 던전 클리어 보상 지급 함수
        {
            int reduce = rand.Next(20, 36) + (gm.Dungeon.GetRecommandedDef(dif) - (int)gm.Player.Def );
            gm.Player.OnDamaged(reduce);
            
            prevGold = gm.Player.Gold; 
            gm.Player.Gold += gm.Dungeon.GetDungeonReward(dif) + (gm.Dungeon.GetDungeonReward(dif) * (int)(gm.Player.Atk * 100) / 10000);

            prevLevel = gm.Player.Level;
            gm.Player.ExpUp(); // 경험치 상승
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

            Console.WriteLine("던전 클리어");
            Console.WriteLine("축하합니다 !!");
            Console.WriteLine($"{gm.Dungeon.GetDungeonName(dif)}을 클리어 하였습니다.");

            Console.WriteLine();

            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"Lv. {prevLevel} -> {gm.Player.Level}");
            Console.WriteLine($"체력 {prevHealth} -> {gm.Player.Health}");
            Console.WriteLine($"Gold {prevGold} G -> {gm.Player.Gold} G");

            Console.WriteLine();

            Console.WriteLine("0. 나가기");
        }
    }
}
