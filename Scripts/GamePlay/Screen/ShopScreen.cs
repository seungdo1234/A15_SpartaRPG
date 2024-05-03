
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
        public override void ScreenOn()
        {
            Console.Clear();

            while (true)
            {
                Console.Clear();
                ShopText();
                MyActionText();

                // 0.뒤로 가기  1.아이템 구매  2.아이템 판매
                if (int.TryParse(Console.ReadLine(), out int input) && input >= 0 && input <= 2)
                {
                    switch (input)
                    {
                        case 1:
                            itemBuyScreen.ScreenOn();
                            break;
                        case 2:
                            itemSellScreen.ScreenOn();
                            break;
                        case 0:
                            return;
                    }
                    Console.Clear();
                }
                else
                {
                    SystemMessageText(EMessageType.ERROR);
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

            Console.WriteLine("[ 장비 ]");
            for (int i = 0; i < dm.ShopEquipItems.Count; i++)
            {
                EquipItem equipItem = dm.ShopEquipItems[i];

                Console.Write($"- {equipItem.ItemName} ({equipItem.GetEquipItemClassName()})\t| ");


                if (equipItem.AtkValue != 0)
                {
                    Console.Write($"공격력 {equipItem.AtkValue} ");
                }

                if (equipItem.DefValue != 0)
                {
                    Console.Write($"방어력 {equipItem.DefValue} ");
                }

                string sell = equipItem.IsSell ? "구매 완료" : $"{equipItem.Gold} G";

                Console.WriteLine($"|\t{equipItem.Desc} | {sell}");

            }

            Console.WriteLine();

            Console.WriteLine("[ 물약 ]");

            for(int i = 0; i< dm.ShopConsumableItems.Length; i++)
            {
                int num = gm.Player.PlayerConsumableItems.ContainsKey(dm.ShopConsumableItems[i]) ? gm.Player.PlayerConsumableItems[dm.ShopConsumableItems[i]] : 0;
                Console.WriteLine($"- {dm.ShopConsumableItems[i].ItemName}\t| {dm.ShopConsumableItems[i].ItemRank} | {dm.ShopConsumableItems[i].Desc}\t| " +
                    $"{dm.ShopConsumableItems[i].Gold}G ({num}개 보유중)");
            }

            Console.WriteLine();

            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
        }
    }
}
