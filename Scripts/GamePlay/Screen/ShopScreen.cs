
namespace TextRPG
{
    public class ShopScreen : Screen
    {
        private ItemBuyScreen itemBuyScreen;
        private ItemSellScreen itemSellScreen;
        public ShopScreen()
        {
            itemBuyScreen = new ItemBuyScreen();
            itemSellScreen = new ItemSellScreen();
        }

        // 상점 화면
        public void ShopScreenOn()
        {
            Console.Clear();

            while (true)
            {
                ShopText();
                MyActionText();

                // 0.뒤로 가기  1.아이템 구매  2.아이템 판매
                if (int.TryParse(Console.ReadLine(), out int input) && input >= 0 && input <= 2)
                {
                    switch (input)
                    {
                        case 1:
                            itemBuyScreen.ItemBuyScreenOn();
                            break;
                        case 2:
                            itemSellScreen.ItemSellScreenOn();
                            break;
                        case 0:
                            return;
                    }
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다! 로비로 돌아갈려면 0번을 입력하세요. \n");
                }
            }

        }

        // 상점 텍스트 출력
        private void ShopText()
        {
            Console.WriteLine();

            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");

            Console.WriteLine();

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{gm.Player.Gold} G");

            Console.WriteLine();

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < dm.ShopItemsCount(); i++)
            {
                Item item = dm.GetShopItem(i);
                string itemType = item.Itemtype == ItemTypes.Attack ? "공격력" : "방어력";
                string sell = item.IsSell ? "구매 완료" : $"{item.Gold} G";
                Console.WriteLine($"- {item.ItemName}\t| {itemType} +{item.Value} |\t{item.Desc} | {sell}");
            }

            Console.WriteLine();

            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
        }
    }
}
