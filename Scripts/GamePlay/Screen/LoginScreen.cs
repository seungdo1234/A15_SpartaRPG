
namespace TextRPG
{
    public class LoginScreen :Screen
    {
        private LobbyScreen lobbyScreen;
        private ClassSelectionScreen classSelectionScreen;

        public LoginScreen()
        {
            classSelectionScreen = new ClassSelectionScreen();
            lobbyScreen = new LobbyScreen();
        }
        // 로그인
        public void LoginScreenOn()
        {
            string playerName = NameCheck();


            // 닉네임을 입력받고 해당 닉네임의 데이터를 받아오고 게임 시작
            gm.Player = gm.SaveSystem.Load(playerName);

            if(gm.Player.ePlayerClass == PlayerClass.DEFAULT) // 새로 생성한 플레이어 데이터라면
            {
                classSelectionScreen.ClassSelectionScreenOn(); // 직업 선택
            }

            lobbyScreen.LobbyScreenOn();
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
            bool recheckName = true;

            do
            {
                Console.Clear();
                LoginText();

                name = Console.ReadLine();

                Console.WriteLine("이 닉네임이 맞습니까?");
                Console.Write("1. 예\t2. 아니오 >>");
                recheckName = Console.ReadLine() == "1" ? false : true; 
            } while (recheckName);
            

            return name;
        }

        private void LoadCheck()
        {

        }
    }
}
