
namespace TextRPG
{
    public class ConsumableItem : Item
    {
        public EConsumableType ConsumableType { get;private set; }
        public int Value { get; private set; }

        public ConsumableItem(string itemName, EItemRank itemRank, EConsumableType consumableType,int value ,string desc, int gold )
        {
            ItemName = itemName;
            ItemRank = itemRank;
            ConsumableType = consumableType;
            Value = value;
            Desc = desc;
            Gold = gold;
        }
    }
}
