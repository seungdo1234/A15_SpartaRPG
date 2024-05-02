
namespace TextRPG
{
    public class ItemBuyScreen :Screen
    {
        private int resetGold = 500;

        // 아이템 구매 화면
        public override void ScreenOn()
        {

            while (true)
            {
                Console.Clear();
                ItemBuyScreenText();
                MyActionText();

                // 0: 뒤로가기  아이템 번호 : 구매
                if (int.TryParse(Console.ReadLine(), out int input) && input >= 0 && input <= dm.ShopEquipItems.Count + 1)
                {

                    if (input == 0)
                    {
                        return;
                    }
                    else if (input == 1)
                    {
                        if(gm.Player.Gold >= resetGold)
                        {
                            gm.Player.Gold -= resetGold;
                            dm.ShopItemReset();
                        }
                        else
                        {
                            Console.WriteLine("Gold가 부족합니다 !\n");
                        }
                    }
                    else
                    {
                        EquipItem equipItem = dm.ShopEquipItems[input - 2];

                        // 아이템 구매 및 실패
                        if (equipItem.IsSell)
                        {
                            Console.WriteLine("이미 구매한 아이템입니다. \n");
                        }
                        else
                        {
                            if (gm.Player.Gold >= equipItem.Gold) // 아이템 구매
                            {
                                Console.WriteLine("구매를 완료했습니다. \n");
                                dm.BuyShopItem(equipItem);
                            }
                            else // 실패
                            {
                                Console.WriteLine("Gold가 부족합니다 !\n");
                            }
                        }
                    }

                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다! 숫자를 제대로 입력하세요. \n");
                }
            }

        }

        // 아이템 구매 텍스트 출력
        private void ItemBuyScreenText()
        {
            Console.WriteLine();

            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");

            Console.WriteLine();

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{gm.Player.Gold} G");

            Console.WriteLine();

            Console.WriteLine("[장비 목록]");

            for (int i = 0; i < dm.ShopEquipItems.Count; i++) // 판매 목록 출력
            {
                EquipItem equipItem = dm.ShopEquipItems[i];

                Console.Write($"- {i + 2} {equipItem.ItemName} ({equipItem.GetEquipItemClassName()})\t| ");


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

            Console.WriteLine($"1. 상점 아이템 초기화 ({resetGold} G)");
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
        }
    }
}
