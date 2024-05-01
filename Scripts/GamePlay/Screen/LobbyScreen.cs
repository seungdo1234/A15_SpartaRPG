
namespace TextRPG
{
    public class LobbyScreen : Screen
    {
        private StatusScreen statusScreen;
        private InventoryScreen inventoryScreen;
        private ShopScreen shopScreen;
        private DungeonResultScreen dungeonScreen;
        private RestScreen restScreen;

        // 던전 전투 초기화
        private DungeonBattle dungeonBattle;
        public LobbyScreen()
        {
            statusScreen = new StatusScreen();
            inventoryScreen = new InventoryScreen();
            shopScreen = new ShopScreen();
            dungeonScreen = new DungeonResultScreen();
            restScreen = new RestScreen();

            // 던전 배틀 초기화 및 이벤트
            dungeonBattle = new DungeonBattle();
            
            dungeonBattle.PlayerDied += PlayerDiedHandler;
            dungeonBattle.EnemyDied += EnemyDiedHandler;
        }

        // 로비 화면
        public void LobbyScreenOn()
        {
            Console.Clear();

            while (true)
            {
                LobbyText();
                MyActionText();

                // 1, 2, 3만 입력 받을 수 있게 함 , 테스트를 위해 범위를 6 에서 7로 조정
                if (int.TryParse(Console.ReadLine(), out int input) && input >= 0 && input < 7)
                {
                    switch (input)
                    {
                        case 0:
                            gm.SaveSystem.Save();
                            return;
                        case 1:
                            statusScreen.StatusScreenOn();
                            break;
                        case 2:
                            inventoryScreen.InventoryScreenOn();
                            break;
                        case 3:
                            shopScreen.ShopScreenOn();
                            break;
                        case 4:
                            dungeonScreen.DungeonResultScreenOn(EDungeonResultType.VICTORY , EDungeonDifficulty.HARD);
                            break;
                        case 6:
                            dungeonBattle.CheckforBattle();
                            break;
                        case 5:
                            restScreen.RestScreenOn();
                            break;

                    }
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다! 1부터 3까지의 숫자를 다시 입력하세요.\n");
                }

            }
        }

        // 로비화면 텍스트 출력
        private void LobbyText()
        {
            Console.WriteLine();

            Console.WriteLine($"스파르타 마을에 오신 {gm.Player.Name}님 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다. \n");

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전입장");
            Console.WriteLine("5. 휴식하기");

            Console.WriteLine();

            Console.WriteLine("0. 저장 및 나가기");

            Console.WriteLine();
        }



        // 사망, 적처리 이벤트

        private void PlayerDiedHandler()
        {
            LobbyScreenOn();
        }

        private void EnemyDiedHandler()
        {
            LobbyScreenOn();
        }

    }
}
