
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
            bool hasLoad = gm.SaveSystem.CheckLoad();
            string playerName;

            if (hasLoad)
            {
                while (true)
                {
                    Console.Clear();
                    PrintLogo();
                    PrintNotice("과거 데이터");
                    Console.WriteLine("가 존재합니다! 불러오시겠습니까?");
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
                            classSelectionScreen.ScreenOn();
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
                classSelectionScreen.ScreenOn();

            }

            
            lobbyScreen.ScreenOn();
        }

        private void LoginText()
        {
            PrintLogo();

            Console.Write("닉네임을 입력하세요 >> ");

        }

        private string NameCheck()
        {
            string name;
            int yesOrNo = 2;

            do
            {
                LoginText();

                name = Console.ReadLine();

                if (name == "")
                {
                    SystemMessageText(EMessageType.ERROR);
                    continue;
                }

                while (true)
                {
                    Console.WriteLine();
                    PrintName(name);
                    Console.WriteLine(" 이(가) 맞습니까?");
                    Console.Write("1. 예\t2. 아니오 >> ");

                    if (int.TryParse(Console.ReadLine(), out yesOrNo) && (yesOrNo == 1 || yesOrNo == 2))
                    {
                        break;
                    }
                    else
                    {
                        SystemMessageText(EMessageType.ERROR);
                        Console.SetCursorPosition(0, Console.CursorTop - 4);
                        Console.WriteLine("                                                  ");
                        Console.WriteLine("                                                  ");
                        Console.WriteLine("\n                                                  ");
                        Console.SetCursorPosition(0, Console.CursorTop - 5);
                    }
                }
            } while (!(yesOrNo == 1));

            return name;
        }
    }
}
