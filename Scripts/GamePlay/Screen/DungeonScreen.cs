namespace TextRPG
{
    public class DungeonScreen : Screen
    {
        private DungeonResultScreen dungeonResultScreen;

        public DungeonScreen()
        {
            dungeonResultScreen = new DungeonResultScreen();

        }

        public void DungeonScreenOn()
        {
            Console.Clear();

            while (true)
            {
                DungeonText();
                MyActionText();

                // 1, 2, 3만 입력 받을 수 있게 함 
                if (int.TryParse(Console.ReadLine(), out int input) && input >= 0 && input < 4)
                {
                    if(input == 0)
                    {
                        return;
                    }

                    dungeonResultScreen.DungeonResultScreenOn(input - 1);


                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다! 숫자를 다시 입력하세요.\n");
                }

            }
        }


        public void DungeonText()
        {

            Console.WriteLine();

            Console.WriteLine("던전입장");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

            Console.WriteLine();

            Console.WriteLine($"1. {gm.Dungeon.GetDungeonName(0)}\t | 방어력 {gm.Dungeon.GetRecommandedDef(0)} 이상 권장");
            Console.WriteLine($"2. {gm.Dungeon.GetDungeonName(1)}\t | 방어력 {gm.Dungeon.GetRecommandedDef(1)} 이상 권장");
            Console.WriteLine($"3. {gm.Dungeon.GetDungeonName(2)}\t | 방어력 {gm.Dungeon.GetRecommandedDef(2)} 이상 권장");
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
        }
    }
}
