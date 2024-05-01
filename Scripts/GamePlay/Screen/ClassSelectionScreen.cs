
namespace TextRPG
{
    public class ClassSelectionScreen : Screen
    {

        //  Enum.GetNames(typeof(PlayerClass)).Length => PlayerClass 열거형의 길이를 반환
        public void ClassSelectionScreenOn()
        {
            Console.Clear();

            while (true)
            {
                ClassSelectionText();

                // 직업을 선택
                if (int.TryParse(Console.ReadLine(), out int input) && input >= 1 && input < Enum.GetNames(typeof(UnitType)).Length - 1 )
                {
                    // 4.30 J => 플레이어 직업 선택 수정
                    gm.Player.ChangePlayerClass((UnitType)input);

                    Console.Clear();

                    Console.WriteLine($"\n{gm.Player.GetPlayerClass((UnitType)input)}를 선택하셨습니다. ");
                    Console.WriteLine("\n게임을 시작합니다...") ;

                    Thread.Sleep(1000);
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다! 숫자를 다시 입력하세요.\n");
                }
            }

            
        }

        private void ClassSelectionText() // 텍스트
        {
            Console.WriteLine();

            Console.WriteLine($"안녕하세요. {gm.Player.Name}님");

            Console.WriteLine() ;

            Console.Write($"플레이하실 직업을 선택해주세요. ");

            Console.Write("( ");
            for ( int i = 1; i < Enum.GetNames(typeof(UnitType)).Length - 1; i++)
            {
                Console.Write($"{i}.{gm.Player.GetPlayerClass((UnitType)i) } ");
            }
            Console.Write(" )");

            Console.WriteLine("\n");

            Console.Write(">> ");

        }
    }
}
