
using System.Diagnostics;

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
                if (int.TryParse(Console.ReadLine(), out int input) && input >= 0 && input <= dm.ShopEquipItems.Count + 3)
                {

                    if (input == 0)
                    {
                        return;
                    }
                    else if (input == 1)
                    {
                        if(gm.Player.Gold >= resetGold) // 상점 초기화.
                        {
                            gm.Player.Gold -= resetGold;
                            dm.ShopItemReset();
                            SystemMessageText(EMessageType.SHOPRESET);
                        }
                        else
                        {
                            SystemMessageText(EMessageType.GOLD);
                        }
                    }
                    else if (dm.ShopEquipItems.Count + 1 >= input)
                    {
                        BuyEquipItem(input);
                    }
                    else
                    {
                        BuyConsumableItem(input);
                    }

                }
                else
                {
                    SystemMessageText(EMessageType.ERROR);
                }
            }

        }

        // 아이템 구매 텍스트 출력
        private void ItemBuyScreenText()
        {
            Console.WriteLine();

            PrintTitle("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            PrintTitle("보유 골드");
            PrintNotice(gm.Player.Gold.ToString());
            Console.WriteLine(" G\n");


            PrintTitle("장비 목록");
            for (int i = 0; i < dm.ShopEquipItems.Count; i++) // 판매 목록 출력
            {
                EquipItem equipItem = dm.ShopEquipItems[i];

                Console.Write($"- {i + 2} {equipItem.ItemName} ({equipItem.GetEquipItemClassName()}) ");

                equipItem.GetItemRankName();

                Console.Write("\t| ");

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

            PrintTitle("물약");
            for (int i = 0; i < dm.ShopConsumableItems.Length; i++)
            {
                int num = gm.Player.PlayerConsumableItems.ContainsKey(dm.ShopConsumableItems[i].ItemName) ? gm.Player.PlayerConsumableItems[dm.ShopConsumableItems[i].ItemName] : 0;
                Console.Write($"- {dm.ShopEquipItems.Count + i + 2} {dm.ShopConsumableItems[i].ItemName} ");
                dm.ShopConsumableItems[i].GetItemRankName();

                Console.WriteLine($"\t| {dm.ShopConsumableItems[i].Desc}\t| {dm.ShopConsumableItems[i].Gold}G ({num}개 보유중)");
            }

            Console.WriteLine();

            Console.WriteLine($"1. 상점 아이템 초기화 ({resetGold} G)");
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
        }

        private void BuyEquipItem(int input) // 장비 구매
        {
            EquipItem equipItem = dm.ShopEquipItems[input - 2];

            // 아이템 구매 및 실패
            if (equipItem.IsSell)
            {
                SystemMessageText(EMessageType.ALREADYBUYITEM);
            }
            else
            {
                if (gm.Player.Gold >= equipItem.Gold)
                {
                    SystemMessageText(EMessageType.BUYITEM);
                    dm.BuyShopItem(equipItem);
                }
                else // 실패
                {
                    SystemMessageText(EMessageType.GOLD);
                }
            }
        }

        private void BuyConsumableItem(int input) // 소비 아이템 구매
        {
            ConsumableItem consumableItem = dm.ShopConsumableItems[input - dm.ShopEquipItems.Count - 2];
             if (gm.Player.Gold >= consumableItem.Gold)
            {
                SystemMessageText(EMessageType.BUYITEM);
                gm.Player.Gold -= consumableItem.Gold;
                gm.Player.AddConsumableItem(consumableItem.ItemName);
            }
            else
            {
                SystemMessageText(EMessageType.GOLD);
            }
        }
    }
}
