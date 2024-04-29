
namespace TextRPG
{

    // 각종 스크린 클래스의 부모클래스
    public class Screen
    {
        protected ItemDataManager dm;
        protected GameManager gm;
        public Screen()
        {
            dm = ItemDataManager.instance;
            gm = GameManager.instance;
        }

        // 인벤토리 아이템 텍스트 출력
        protected void InventoryItemText(Item item)
        {
            string equip = item.IsEquip ? "[E]" : "";
            string itemType = item.Itemtype == ItemTypes.WEAPON ? "공격력" : "방어력";
            Console.WriteLine($"{equip}{item.ItemName}\t| {itemType} {item.Value} |\t{item.Desc}");
        }


        // 플레이어의 행동 텍스트 출력
        protected void MyActionText()
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
        }
    }
}
