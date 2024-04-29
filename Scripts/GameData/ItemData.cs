
namespace TextRPG
{ 
    public class Item // 아이템 정보가 담긴 클래스
    {
        private bool isSell = false; // 아이템이 팔렸는 지
        private bool isEquip = false; // 장착 중인지
        private string itemName; //
        private ItemTypes itemtype; // 무기인지, 방어구인지
        private float value; // 아이템의 능력치
        private string desc;
        private int gold;


        public bool IsSell { get => isSell; set { isSell = value; } }
        public bool IsEquip { get => isEquip; set { isEquip = value; } }
        public string ItemName { get => itemName; set { itemName = value; } }
        public ItemTypes Itemtype { get => itemtype; set { itemtype = value; } }
        public float Value { get => value; set { this.value = value; } }
        public string Desc { get => desc; set { desc = value; } }
        public int Gold { get => gold; set { gold = value; } }


        // 아이템 초기화
        public Item(string itemName, ItemTypes itemType, float value ,string desc, int gold)
        {
            this.itemName = itemName;
            this.itemtype = itemType;
            this.value = value;
            this.desc = desc;
            this.gold = gold;
        }
    }
}