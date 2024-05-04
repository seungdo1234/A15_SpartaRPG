
namespace TextRPG
{
    public class ClassSelectionScreen : Screen
    {

        //  Enum.GetNames(typeof(PlayerClass)).Length => PlayerClass 열거형의 길이를 반환
        public override void ScreenOn()
        {
            while (true)
            {
                Console.Clear();
                ClassSelectionText();

                // 직업을 선택
                if (int.TryParse(Console.ReadLine(), out int input) && input >= 1 && input < Enum.GetNames(typeof(EUnitType)).Length - 1 )
                {
                    // 4.30 J => 플레이어 직업 선택 수정
                    gm.Player.ChangePlayerClass((EUnitType)input);

                    Console.Clear();

                    Console.WriteLine($"\n{gm.Player.GetPlayerClass((EUnitType)input)}를 선택하셨습니다. ");
                    Console.WriteLine("\n게임을 시작합니다...") ;

                    Thread.Sleep(1000);
                    return;
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다! 숫자를 다시 입력하세요.\n");
                    Thread.Sleep(750);
                }
            }

            
        }

        private void ClassSelectionText() // 텍스트
        {
            PrintLogo();

            Console.Write($"안녕하세요. ");
            PrintName(gm.Player.Name);
            Console.WriteLine("님");

            Console.Write($"플레이하실 ");
            PrintNotice("직업");
            Console.WriteLine("을 선택해주세요. \n");

            Console.Write("( ");
            for ( int i = 1; i < Enum.GetNames(typeof(EUnitType)).Length - 2; i++)
            {
                Console.Write($"{i}.{gm.Player.GetPlayerClass((EUnitType)i) } ");
            }
            Console.Write(" )");

            Console.WriteLine("\n");

            Console.Write(">> ");

        }
    }
}
