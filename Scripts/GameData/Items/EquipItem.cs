
namespace TextRPG
{
    public class EquipItem : Item
    {
        public EUnitType UnitType { get; private set; }
        public EEquipItemType Itemtype { get; private set; }
        public float AtkValue { get; private set; }
        public float DefValue { get; private set; }
        public bool IsEquip { get; set; }
        public bool IsSell { get; set; }


        public EquipItem(EUnitType unitType,EItemRank itemRank, string itemName, EEquipItemType itemtype, float atkValue, float defValue, string desc, int gold)
        {
            UnitType = unitType;
            ItemRank = itemRank;
            ItemName = itemName;
            Itemtype = itemtype;
            AtkValue = atkValue;
            DefValue = defValue;
            Desc = desc;
            Gold = gold;
            IsEquip = false;
            IsSell = false;
        }

        public string GetEquipItemClassName() // 플레이어의 직업 별 이름 반환 
        {
            string playerClass = UnitType switch
            {
                EUnitType.WARRIOR => "전사 전용",
                EUnitType.ARCHER => "궁수 전용",
                EUnitType.THIEF => "도적 전용",
                EUnitType.MAGICIAN => "마법사 전용",
                _ => "공용 착용 가능" // default
            };

            return playerClass;
        }
       
        // 던전 보상 장비 아이템 깊은 복사
        public EquipItem CopyEquipItem()
        {
            EquipItem equipItem = new EquipItem(UnitType, ItemRank, ItemName, EquipmenttType, AtkValue,  DefValue, Desc, Gold);

            return equipItem;
        }

    }
}
