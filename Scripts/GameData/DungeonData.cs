
namespace TextRPG
{
    public class DungeonData // 던전 데이터 정보가 들어있는 클래스
    {
        // 4.30 J / 레벨 및 난이도 별 던전 데이터가 나오게 수정 
        public int MaxDungeonLevel { get; private set; }
        public int CurrentDungeonLevel { get; private set; }
        public List <Item> DungeonRewards { get; private set; }


        // 전에 있던 던전 데이터 클래스 구성 요소
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
            // 4.30 J / 레벨 및 난이도 별 던전 데이터가 나오게 수정 
            MaxDungeonLevel = 10;
            CurrentDungeonLevel = 1;
            DungeonRewards.Add(new Item("수련자 갑옷", ItemTypes.ARMOR, 5, "수련에 도움을 주는 갑옷입니다.", 1000));
            DungeonRewards.Add(new Item("무쇠 갑옷", ItemTypes.ARMOR, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1500));
            DungeonRewards.Add(new Item("스파르타의 갑옷", ItemTypes.ARMOR, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500));
            DungeonRewards.Add(new Item("낡은 검", ItemTypes.WEAPON, 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600));
            DungeonRewards.Add(new Item("청동 도끼", ItemTypes.WEAPON, 5, "쉽게 볼 수 있는 낡은 검 입니다.", 1500));
            DungeonRewards.Add(new Item("스파르타의 창", ItemTypes.WEAPON, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000));


            // 전에 있던 던전 데이터 클래스 구성 요소
            dungeonNames = new string[3] { "쉬운 던전", "일반 던전", "어려운 던전" };
            recommendedDefs = new int[3] { 5, 11, 17 };
            dungeonRewards = new int[3] { 1000, 1700, 2500 };
        }

    }
}
