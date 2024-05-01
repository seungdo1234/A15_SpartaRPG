
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
                if (int.TryParse(Console.ReadLine(), out int input) && input >= 0 && input <= dm.PlayerEquipItems.Count)
                {

                    if (input == 0)
                    {
                        return;
                    }

                    EquipItem item = dm.PlayerEquipItems[input - 1];

                    item.IsSell = false; // 판매

                    if (item.IsEquip) // 장착 중인 아이템을 팔 경우 장착 해제
                    {
                        if(item.EquipmenttType == EEquipmentType.WEAPON)
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

            for (int i = 0; i < dm.PlayerEquipItems.Count; i++) // 판매 목록 출력
            {
                EquipItem equipItem = dm.PlayerEquipItems[i];

                string equip = equipItem.IsEquip ? "[E]" : "";

                Console.Write($"- {i + 1} {equip}{equipItem.ItemName} ({equipItem.GetEquipItemClassName()})\t| ");


                if (equipItem.AtkValue != 0)
                {
                    Console.Write($"공격력 {equipItem.AtkValue} ");
                }

                if (equipItem.DefValue != 0)
                {
                    Console.Write($"방어력 {equipItem.DefValue} ");
                }

                Console.WriteLine($"|\t{equipItem.Desc} | {MathF.Floor((float)equipItem.Gold * 0.8f)} G ");
            }

            Console.WriteLine();

            Console.WriteLine("0. 나가기");

            Console.WriteLine();
        }

    }
}
