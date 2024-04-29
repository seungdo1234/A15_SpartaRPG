
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
            LoginText();

            // 닉네임을 입력받고 해당 닉네임의 데이터를 받아오고 게임 시작
            gm.Player = gm.SaveSystem.Load(Console.ReadLine());

            if(gm.Player.PlayerClass == PlayerClass.defalut) // 새로 생성한 플레이어 데이터라면
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
    }
}
