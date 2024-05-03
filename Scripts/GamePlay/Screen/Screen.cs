
using System.Runtime.InteropServices;

namespace TextRPG
{

    // 각종 스크린 클래스의 부모클래스
    public  abstract class Screen
    {
        protected ItemDataManager dm;
        protected GameManager gm;        

        protected static int playerInput;
        
        public Screen()
        {
            dm = ItemDataManager.instance;
            gm = GameManager.instance;               
        }

        // 5.1 J => 장비 추가로 인한 리팩토링
        // 인벤토리 아이템 텍스트 출력
        protected void InventoryItemText(EquipItem equipItem)
        {
            string equip = equipItem.IsEquip ? "[E]" : "";

            Console.Write($"{equip}{equipItem.ItemName} ({equipItem.GetEquipItemClassName()})\t| ");
            
            switch (equipItem.ItemRank)
            {
                case EItemRank.COMMON:
                    Console.Write($"일반\t| ");
                    break;
                case EItemRank.RARE:
                    Console.Write($"희귀\t| ");
                    break;
                case EItemRank.EPIC:
                    Console.Write($"서사\t| ");
                    break;
            }            

            if(equipItem.AtkValue != 0)
            {
                Console.Write($"공격력 {equipItem.AtkValue} ");
            }

            if(equipItem.DefValue != 0)
            {
                Console.Write($"방어력 {equipItem.DefValue} ");
            }

            Console.WriteLine($"|\t{equipItem.Desc}");            
        }


        // 플레이어의 행동 텍스트 출력
        protected void MyActionText()
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");            
        }

        protected void SystemMessageText(EMessageType messageType)
        {
            Console.WriteLine();

            switch (messageType)
            {
                case EMessageType.DEFAULT:
                    return;
                case EMessageType.ERROR:
                    Console.WriteLine("잘못된 입력입니다");
                    break;
                case EMessageType.OTHERCLASSITEM:
                    Console.WriteLine("현재 직업에 맞지 않는 아이템입니다.");
                    break;
                case EMessageType.MANALESS:
                    Console.WriteLine("마나가 부족합니다.");
                    break;
                case EMessageType.BUYITEM:
                    Console.WriteLine("아이템을 구매했습니다.");
                    break;
                case EMessageType.SELL:
                    Console.WriteLine("아이템을 판매했습니다.");
                    break;
                case EMessageType.GOLD:
                    Console.WriteLine("Gold가 부족합니다.");
                    break;
                case EMessageType.ALREADYBUYITEM:
                    Console.WriteLine("이미 구매한 장비입니다.");
                    break;
                case EMessageType.SHOPRESET:
                    Console.WriteLine("상점 장비 아이템을 초기화했습니다.");
                    break;
                case EMessageType.SHOPRESETFAIL:
                    Console.WriteLine("모든 장비를 다 구매하셨습니다.");
                    break;
            }
            Thread.Sleep(750);
        }

        public abstract void ScreenOn();
    }
}
