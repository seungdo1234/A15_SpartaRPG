namespace TextRPG
{
    public class PlayerRewards
    {
        public List<EquipItem> rewardEquipItems; // 던전 보상 추가 예정
        public int totalGold;
        public int currentGold;

        public PlayerRewards()
        {
            rewardEquipItems = new List<EquipItem>();
            totalGold = 0;
            currentGold = 0;
        }
    }
}
