
namespace TextRPG
{
    public class DungeonData // 던전 데이터 정보가 들어있는 클래스
    {
        private string[] dungeonNames;
        private int[] recommendedDefs;
        private int[] dungeonRewards;

        // 각각의 던전 정보들을 읽을 수 있는 함수
        public string GetDungeonName(int idx)
        {
            return dungeonNames[idx];
        }
        public int GetRecommandedDef(int idx)
        {
            return recommendedDefs[idx];
        }
        public int GetDungeonReward(int idx)
        {
            return dungeonRewards[idx];
        }

        public DungeonData() 
        {
            dungeonNames = new string[3] { "쉬운 던전", "일반 던전", "어려운 던전" };
            recommendedDefs = new int[3] { 5, 11, 17 };
            dungeonRewards = new int[3] { 1000, 1700, 2500 };
        }

    }
}
