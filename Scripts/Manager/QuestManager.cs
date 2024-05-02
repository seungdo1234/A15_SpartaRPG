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
        private int[,] QuestSaver;


        public QuestManager()
        {
            string jsonFilePath;
            string jsonText;

            QuestSaver = new int[4, 2]; //왼쪽: 퀘스트 타입. 오른쪽: 퀘스트번호, currentProgress
            QuestSaver[0, 0] = 0;   //임시: 이번 퀘스트 넘버.
            QuestSaver[0, 1] = 0;   //임시: 클리어한 최대 스테이지 번호.

            //스토리 퀘스트 리스트
            StoryQuest = new List<Quest>();
            jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\SaveData\StoryQuest.json");
            jsonText = File.ReadAllText(jsonFilePath);
            StoryQuest = JsonConvert.DeserializeObject<List<Quest>>(jsonText);
        }


        public Quest GetCurrentStoryQuest()
        {
            StoryQuest[QuestSaver[0,0]].CurrentProgress = QuestSaver[0, 1];

            return StoryQuest[QuestSaver[0,0]];
        }
    }
}
