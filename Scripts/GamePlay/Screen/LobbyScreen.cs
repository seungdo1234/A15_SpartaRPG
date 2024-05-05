
namespace TextRPG
{
    public class LobbyScreen : Screen
    {
        private StatusScreen statusScreen;
        private InventoryScreen inventoryScreen;
        private ShopScreen shopScreen;
        private DungeonResultScreen dungeonScreen;
        private QuestScreen questScreen;
        private DungeonBattleScreen dungeonBattle;
        // 5.5 A 크레딧 스크린 삭제, DungeonBattle클래스로 이동
        public LobbyScreen()
        {
            statusScreen = new StatusScreen();
            inventoryScreen = new InventoryScreen();
            shopScreen = new ShopScreen();
            dungeonScreen = new DungeonResultScreen();
            questScreen = new QuestScreen();
            dungeonBattle = new DungeonBattleScreen();
            // 5.5 A 크레딧 스크린 삭제, DungeonBattle클래스로 이동
        }

        // 로비 화면
        public override void ScreenOn()
        {
            while (true)
            {   
                Console.Clear();
                // 5.5 A 크레딧 스크린 삭제, DungeonBattle클래스로 이동

                LobbyText();
                MyActionText();

                // 1, 2, 3만 입력 받을 수 있게 함 , 테스트를 위해 범위를 6 에서 7로 조정
                if (int.TryParse(Console.ReadLine(), out int input) && input >= 0 && input < 6)
                {   
                    switch (input)
                    {
                        case 0:
                            gm.SaveSystem.Save();
                            return;
                        case 1:
                            statusScreen.ScreenOn();
                            break;
                        case 2:
                            inventoryScreen.ScreenOn();
                            break;
                        case 3:
                            shopScreen.ScreenOn();
                            break;
                        case 4:
                            dungeonBattle.ScreenOn();
                            break;
                        case 5:
                            questScreen.ScreenOn();
                            break;
                    }                    
                }
                else
                {
                    //Console.WriteLine("잘못된 입력입니다! 1부터 3까지의 숫자를 다시 입력하세요.\n");
                    SystemMessageText(EMessageType.ERROR);
                }

            }
        }

        // 로비화면 텍스트 출력
        private void LobbyText()
        {
            Console.WriteLine();
            Console.Write("이곳은 ");
            PrintNotice("스파르타 던전");
            Console.WriteLine("에 가기 전 들를 수 있는 마지막 마을,");
            PrintNotice("스파르타 마을");
            Console.WriteLine("이다.\n");

            PrintName(gm.Player.Name);
            Console.WriteLine(" 은(는) 지금 무엇을 할지 고민하고 있다...\n");
            
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리 열기");
            Console.WriteLine("3. 상점 가기");
            Console.WriteLine("4. 던전 탐험하기");
            Console.WriteLine("5. 퀘스트 보드 확인");

            Console.WriteLine();

            Console.WriteLine("0. 저장 및 나가기");

            Console.WriteLine();            
        }
    }
}
