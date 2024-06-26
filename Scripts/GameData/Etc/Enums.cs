
namespace TextRPG
{
    public enum EUnitType { DEFAULT, WARRIOR, ARCHER, THIEF, MAGICIAN, ENEMY, BOSS }

    [Flags]
    public enum EEquipItemType
    {
        DEFAULT = 0, 
        WEAPON = 1 << 1,
        OFFHAND = 1 << 2, 
        BODYARMOR = 1 << 3, 
        HELMET = 1 << 4, 
        BOOTS = 1 << 5
    }

    public enum EDungeonDifficulty { DEFAULT, EASY, NORMAL, HARD }

    public enum EDungeonResultType { DEFAULT, VICTORY, RETIRE }

    public enum EItemRank { DEFAULT, COMMON, RARE, EPIC }

    public enum EConsumableType { DEFAULT, HEALTH, MANA}

    public enum EMessageType { DEFAULT, ERROR, OTHERCLASSITEM , MANALESS, BUYITEM, SELL, GOLD , ALREADYBUYITEM , SHOPRESET , SHOPRESETFAIL, FULLCONDITION  }

    public enum ECrowdControlType
    {
        DEFAULT = 0, 
        STUN = 1 << 1, 
        BLIND = 1 << 2, 
        SILENCE = 1 << 3
    }
}
