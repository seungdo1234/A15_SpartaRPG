
using System;
using System.Reflection;

namespace TextRPG
{
    public class InventoryScreen : Screen
    {
        private EquipScreen equipScreen;
        public InventoryScreen()
        {
            equipScreen = new EquipScreen();
        }

        // 인벤토리 화면
        public override void ScreenOn()
        {            
            while (true)
            {   
                Console.Clear();
                InventoryText();
                MyActionText();

                // 1: 장착 관리, 2: 나가기
                if (int.TryParse(Console.ReadLine(), out int input) && input >= 0 && input <= 1)
                {
                    switch (input)
                    {
                        case 1:
                            equipScreen.ScreenOn();                            
                            break;
                        case 0:                            
                            return;                        
                    }                    
                }
                else
                {
                    //Console.WriteLine("\n잘못된 입력입니다! 로비로 돌아갈려면 0번을 입력하세요. \n");
                    SystemMessageText(EMessageType.ERROR);
                }
            }
        }

        // 인벤토리 텍스트 출력
        private void InventoryText()
        {
            Console.WriteLine();

            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[장비 목록]");
            for (int i = 0; i < gm.Player.PlayerEquipItems.Count; i++)
            {
                Console.Write("- ");
                InventoryItemText(gm.Player.PlayerEquipItems[i]);
            }

            Console.WriteLine();

            Console.WriteLine("[ 물약 ]");
            foreach (var itemName in gm.Player.PlayerConsumableItems.Keys)
            {
                ConsumableItem cItem = dm.ConsumableItemDB.Find(obj => obj.ItemName == itemName);
                Console.Write($"- {cItem.ItemName}");
                cItem.GetItemRankName();
                Console.WriteLine($"\t| {cItem.Desc} ({gm.Player.PlayerConsumableItems[cItem.ItemName]}개 보유중)");
            }

            Console.WriteLine();

            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");

            Console.WriteLine();            
        }

    }
}
