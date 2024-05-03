
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
        public override void ScreenOn() //24.05.03 데이터 로드 방식 변경 - C
        {
            bool hasLoad = gm.SaveSystem.CheckLoad();
            string playerName;

            if (hasLoad)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("과거 데이터가 존재합니다! 불러오시겠습니까?");
                    Console.Write("1. 예\t2. 아니오 >> ");

                    if (int.TryParse(Console.ReadLine(), out int input) && (input == 1 || input == 2))
                    {
                        if (input == 1)
                        {
                            gm.Player = gm.SaveSystem.Load(""); //이름 입력 무시
                        }else
                        {
                            playerName = NameCheck();
                            gm.Player = gm.SaveSystem.Load(playerName); //새로운 이름 입력받기
                        }
                        break;
                    }
                    else
                    {
                        SystemMessageText(EMessageType.ERROR);
                    }
                }
            }
            else
            {
                playerName = NameCheck();
                gm.Player = gm.SaveSystem.Load(playerName); //새로운 이름 입력받기

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

                if (name == "")
                {
                    Console.WriteLine("\n잘못된 입력입니다! 한 글자 이상 입력해주세요.");
                    Thread.Sleep(1000);
                    continue;
                }

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
                        SystemMessageText(EMessageType.ERROR);
                        Console.SetCursorPosition(0, Console.CursorTop - 2);
                    }
                }
            } while (!(yesOrNo == 1));

            return name;
        }
    }
}
