
namespace TextRPG
{
    public class ItemSellScreen :Screen
    {

        public void ItemSellScreenOn()
        {
            Console.Clear();

            while (true)
            {
                ItemSellScreenText();
                MyActionText();

                // 0: 뒤로가기  아이템 번호 : 구매
                if (int.TryParse(Console.ReadLine(), out int input) && input >= 0 && input <= dm.PlayerItemsCount())
                {

                    if (input == 0)
                    {
                        return;
                    }

                    Item item = dm.GetPlayerItem(input - 1);

                    item.IsSell = false; // 판매

                    if (item.IsEquip) // 장착 중인 아이템을 팔 경우 장착 해제
                    {
                        if(item.Itemtype == ItemTypes.Attack)
                        {
                            gm.Player.EquipAtkItem = null;
                        }
                        else
                        {
                            gm.Player.EquipDefItem = null;
                        }
                    }

                    gm.Player.Gold += (int)((float)item.Gold * 0.8f); // 골드 ++
                    dm.RemovePlayerItem(item); // 아이템 삭제
                    

                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다! 숫자를 제대로 입력하세요. \n");
                }
            }
        }

        private void ItemSellScreenText()
        {
            Console.WriteLine();

            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("보유중인 아이템을 판매할 수 있습니다.");

            Console.WriteLine();

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{gm.Player.Gold} G");

            Console.WriteLine();

            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < dm.PlayerItemsCount(); i++) // 판매 목록 출력
            {
                Item item = dm.GetPlayerItem(i);
                string itemType = item.Itemtype == ItemTypes.Attack ? "공격력" : "방어력";
                Console.WriteLine($"- {i + 1} {item.ItemName}\t| {itemType} +{item.Value} |\t{item.Desc} | {(int)((float)item.Gold * 0.8f)} G");
            }

            Console.WriteLine();

            Console.WriteLine("0. 나가기");

            Console.WriteLine();
        }

    }
}
