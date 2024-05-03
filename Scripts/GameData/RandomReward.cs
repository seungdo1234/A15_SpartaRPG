
namespace TextRPG
{
    public struct Reward
    {
        public EquipItem rewardEquipItem; // 던전 보상 추가 예정
        public ConsumableItem rewardConsumableItem; // 던전 보상 추가 예정
        public int gold;
    }
    public class RandomReward
    {
        private int[] baseGoldReward = new int[3] { 500, 900, 1500 };
        // 난이도 아이템 보상 확률 별 확률
        private int[] easyRewardPercent = new int[3] { 67, 30, 3 };
        private int[] normalRewardPercent = new int[3] { 50, 40, 10 };
        private int[] hardRewardPercent = new int[3] { 30, 50, 20 };
        // 난이도 별 물약 보상 등장 확률 
        private int[] consumableItemRewardPercent = new int[3] { 30, 60, 100 };

        Random random = new Random();

        public Reward GetRandomReward()
        {
            Reward reward;

            reward.gold = GetGoldReward();

            EItemRank itemRank = GameManager.instance.Dungeon.dif switch
            {
                EDungeonDifficulty.EASY => GetRandomItemRank(easyRewardPercent),
                EDungeonDifficulty.NORMAL => GetRandomItemRank(normalRewardPercent),
                EDungeonDifficulty.HARD =>GetRandomItemRank(hardRewardPercent),
            };

            reward.rewardEquipItem = GetEquipItemReward(itemRank);

            if (random.Next(1, 101) <= consumableItemRewardPercent[(int)GameManager.instance.Dungeon.dif - 1])
            {
                reward.rewardConsumableItem = GetConsumableItemReward(itemRank);
            }
            else
            {
                reward.rewardConsumableItem = null;
            }

            return reward;
        }

        private int GetGoldReward()
        {
            int goldReward = baseGoldReward[(int)GameManager.instance.Dungeon.dif - 1];
            int randomGold = random.Next(-100, 101);

            return goldReward + randomGold;
        }
        private EItemRank GetRandomItemRank(int[] percent)
        {
            int per = random.Next(1,101);
            int sum = 0;
            for(int i = 0; i < percent.Length; i++)
            {
                sum += percent[i];
                if (per <= sum)
                {
                    return (EItemRank)i + 1;
                }
            }

            return EItemRank.DEFAULT;
        }
        private EquipItem GetEquipItemReward(EItemRank itemRank)
        {
            EquipItem[] equipItems = ItemDataManager.instance.EquipItemDB.FindAll(obj => obj.ItemRank == itemRank).ToArray();

            return equipItems[random.Next(0,equipItems.Length)].CopyEquipItem();
        }
        private ConsumableItem GetConsumableItemReward(EItemRank itemRank)
        {
            ConsumableItem[] consumableItems = ItemDataManager.instance.ConsumableItemDB.FindAll(obj => obj.ItemRank == itemRank).ToArray();

            return consumableItems[random.Next(0, consumableItems.Length)];
        }
    }
}
