
namespace TextRPG
{
    public class ConsumableItem : Item
    {
        public EConsumableType ConsumableType { get; private set; }
        public int Value { get; private set; }

        public ConsumableItem(string itemName, EItemRank itemRank, EConsumableType consumableType, int value, string desc, int gold)
        {
            ItemName = itemName;
            ItemRank = itemRank;
            ConsumableType = consumableType;
            Value = value;
            Desc = desc;
            Gold = gold;
        }

        public void UseItem() // 포션 사용
        {
            switch (ConsumableType)
            {
                case EConsumableType.HEALTH:
                    GameManager.instance.Player.RecoveryHealth(Value);
                    break;
                case EConsumableType.MANA:
                    GameManager.instance.Player.RecoveryMana(Value);
                    break;
            }
            GameManager.instance.Player.RemoveConsumableItem(ItemName);
        }
    }
}
