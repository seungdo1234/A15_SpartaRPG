using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Scripts
{
    public class QuestManager
    {
        private List<Quest> StoryQuest;
        private List<Quest> MonsterQuest;
        private List<Quest> MonsterCatalog;
        //private List<Quest> EquipmentCatalog; //장비모으기 업적: 일단 제외.

        public int[,] QuestSaver;


        public QuestManager()
        {
            string jsonFilePath;
            string jsonText;

            QuestSaver = new int[4, 2]; //왼쪽: 퀘스트 타입. 오른쪽: 퀘스트번호, currentProgress
            QuestSaver[0, 0] = 0;   //임시: 이번 퀘스트 넘버.
            QuestSaver[0, 1] = 4;   //임시: 클리어한 최대 스테이지 번호.
            QuestSaver[1, 0] = 0;
            QuestSaver[1, 1] = 5;

            //스토리 퀘스트 리스트
            StoryQuest = new List<Quest>();
            jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\SaveData\StoryQuest.json");
            jsonText = File.ReadAllText(jsonFilePath);
            StoryQuest = JsonConvert.DeserializeObject<List<Quest>>(jsonText);

            //몬스터 퀘스트 리스트
            MonsterQuest = new List<Quest>();
            jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\SaveData\MonsterQuest.json");
            jsonText = File.ReadAllText(jsonFilePath);
            MonsterQuest = JsonConvert.DeserializeObject<List<Quest>>(jsonText);
        }


        public Quest GetCurrentStoryQuest()
        {
            StoryQuest[QuestSaver[0,0]].CurrentProgress = QuestSaver[0, 1];

            return StoryQuest[QuestSaver[0,0]];
        }

        public Quest GetCurrentMonsterQuest()
        {
            MonsterQuest[QuestSaver[1,0]].CurrentProgress = QuestSaver[1,1];

            return MonsterQuest[QuestSaver[1, 0]];
        }
    }
}
