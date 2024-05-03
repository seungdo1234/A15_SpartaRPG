

namespace TextRPG
{
    public class GameProgram
    {
        private GameManager gm;
        private LoginScreen loginScreen;

        public GameProgram()
        {
            gm = GameManager.instance;
            loginScreen = new LoginScreen();
        }
        // 게임 시작
        public void GameStart()
        {
            gm.Init();
            
            loginScreen.ScreenOn();
        }

        static void Main(string[] args)
        {

            GameProgram program = new GameProgram();

            program.GameStart();
        }
    }

}
