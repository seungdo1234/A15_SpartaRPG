
using System;

namespace TextRPG
{
    public class ItemSellScreen :Screen
    {
        // 아이템을 판매하기 위해 보유하고 있는 소비 아이템 정보를 저장하는 리스트
        private List<ConsumableItem > playerConsumableItems = new List<ConsumableItem>();

        public override void ScreenOn()
        {

            while (true)
            {
                Console.Clear();
                playerConsumableItems.Clear();
                ItemSellScreenText();
                MyActionText();

                // 0: 뒤로가기  아이템 번호 : 구매
                if (int.TryParse(Console.ReadLine(), out int input) && input >= 0 && input <= gm.Player.PlayerEquipItems.Count + 2)
                {

                    if (input == 0)
                    {
                        return;
                    }
                    else if(input <= gm.Player.PlayerEquipItems.Count) 
                    {
                        SellEquipItem(input);
                    }
                    else
                    {
                        SellConsumableItem(input);
                    }

                    SystemMessageText(EMessageType.SELL);
                }
                else
                {
                    SystemMessageText(EMessageType.ERROR);
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

            Console.WriteLine("[ 보유 장비 목록 ]");

            for (int i = 0; i < gm.Player.PlayerEquipItems.Count; i++) // 판매 목록 출력
            {
                EquipItem equipItem = gm.Player.PlayerEquipItems[i];

                string equip = equipItem.IsEquip ? "[E]" : "";

                Console.Write($"- {i + 1} {equip}{equipItem.ItemName} ({equipItem.GetEquipItemClassName()}) ");

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

                Console.WriteLine($"|\t{equipItem.Desc} | {MathF.Floor((float)equipItem.Gold * 0.8f)} G ");

            }


            Console.WriteLine("\n") ;

            Console.WriteLine("[ 보유 물약 ]");

            int num = 1; // 보유하고있는 소비 아이템 탐색
            foreach(var itemName in gm.Player.PlayerConsumableItems.Keys) 
            {
                ConsumableItem cItem =  dm.ConsumableItemDB.Find(obj => obj.ItemName == itemName);
                playerConsumableItems.Add(cItem);
                Console.Write($"- {gm.Player.PlayerEquipItems.Count + num} {cItem.ItemName} ");
                cItem.GetItemRankName();
                Console.WriteLine($"\t| {cItem.Desc}\t| {MathF.Floor((float)cItem.Gold * 0.8f)} G ({gm.Player.PlayerConsumableItems[cItem.ItemName]}개 보유중) ");
                num++;
            }

            Console.WriteLine();

            Console.WriteLine("0. 나가기");

            Console.WriteLine();
        }

        private void SellEquipItem(int input) // 장비 판매
        {
            EquipItem equipItem = gm.Player.PlayerEquipItems[input - 1];

            equipItem.IsSell = false; // 판매

            if (equipItem.IsEquip) // 장착 중인 아이템을 팔 경우 장착 해제
            {
                gm.Player.EquipItemFlag &= ~equipItem.EquipItemType;
                equipItem.IsEquip = !equipItem.IsEquip;
                gm.Player.SwitchingEquipItem(equipItem);
            }

            gm.Player.Gold += (int)((float)equipItem.Gold * 0.8f); // 골드 ++
            gm.Player.RemoveEquipItem(equipItem); // 아이템 삭제
        }

        private void SellConsumableItem(int input) // 소비 아이템 판매
        {
            ConsumableItem consumableItem = playerConsumableItems[input- gm.Player.PlayerEquipItems.Count - 1];

            gm.Player.Gold += (int)((float)consumableItem.Gold * 0.8f); ;
            gm.Player.RemoveConsumableItem(consumableItem.ItemName);
        }
    }
}
