
using Newtonsoft.Json;

namespace TextRPG
{ 
    public class Item // 아이템 정보가 담긴 클래스
    {

        public ItemRanks ItemRank { get; private set; }
        public string ItemName { get; private set; }
        public ItemTypes Itemtype { get; private set; }
        public float Value { get; private set; }
        public string Desc { get; private set; }
        public int Gold { get; private set; }
        public bool IsEquip { get; set; }
        public bool IsSell { get; set; }


        // 아이템 초기화
        public Item(string itemName, ItemTypes itemType, float value ,string desc, int gold)
        {
            ItemName = itemName;
            Itemtype = itemType;
            Value = value;
            Desc = desc;
            Gold = gold;
        }
    }
}