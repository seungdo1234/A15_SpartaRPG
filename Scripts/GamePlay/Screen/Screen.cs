
namespace TextRPG
{

    // 각종 스크린 클래스의 부모클래스
    public  abstract class Screen
    {
        protected ItemDataManager dm;
        protected GameManager gm;
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

        public abstract void ScreenOn();
    }
}
