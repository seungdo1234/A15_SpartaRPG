
namespace TextRPG
{
    public class LoginScreen : Screen
    {
        private LobbyScreen lobbyScreen;
        private ClassSelectionScreen classSelectionScreen;

        public LoginScreen()
        {
            classSelectionScreen = new ClassSelectionScreen();
            lobbyScreen = new LobbyScreen();
        }
        // 로그인
        public override void ScreenOn()
        {
            string playerName = NameCheck();


            // 닉네임을 입력받고 해당 닉네임의 데이터를 받아오고 게임 시작
            gm.Player = gm.SaveSystem.Load(playerName);


            if(gm.Player.ePlayerClass == EUnitType.DEFAULT) // 새로 생성한 플레이어 데이터라면
            {
                classSelectionScreen.ScreenOn(); // 직업 선택
            }
            else
            {
                bool isValidInput = false;

                while (!isValidInput)
                {
                    Console.Clear();
                    Console.WriteLine("같은 이름의 데이터가 있습니다! 불러오시겠습니까?");
                    Console.Write("1. 예\t2. 아니오 >> ");

                    if (int.TryParse(Console.ReadLine(), out int input) && (input == 1 || input == 2))
                    {
                        isValidInput = true;

                        if (input == 2)
                        {
                            classSelectionScreen.ScreenOn();
                        }
                        else { }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다! 1 또는 2를 다시 입력하세요.\n");
                        Thread.Sleep(1000);
                    }
                }
            }

            lobbyScreen.ScreenOn();
        }

        private void LoginText()
        {
            Console.WriteLine();

            Console.WriteLine("┌-----------------------------------------------┐");
            Console.WriteLine("│                 스파르타 던전                 │");
            Console.WriteLine("└-----------------------------------------------┘");

            Console.WriteLine("\n");

            Console.Write("닉네임을 입력하세요 >> ");

        }

        private string NameCheck()
        {
            string name;
            int yesOrNo = 2;

            do
            {
                Console.Clear();
                LoginText();

                name = Console.ReadLine();

                while (true)
                {
                    Console.WriteLine("                                                  ");
                    Console.WriteLine("                                                  ");
                    Console.SetCursorPosition(0, 8);
                    Console.WriteLine($"{name}이 맞습니까?");
                    Console.Write("1. 예\t2. 아니오 >> ");

                    if (int.TryParse(Console.ReadLine(), out yesOrNo) && (yesOrNo == 1 || yesOrNo == 2))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다! 1 또는 2를 다시 입력하세요.\n");
                        Thread.Sleep(1000);
                        Console.SetCursorPosition(0, 9);
                    }
                }
                

            } while (!(yesOrNo == 1));


            return name;
        }
    }
}
