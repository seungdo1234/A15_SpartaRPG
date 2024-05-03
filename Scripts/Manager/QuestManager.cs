using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Scripts
{
    public class QuestManager
    {
        private List<Quest> StoryQuest;
        private List<Quest> MonsterQuest;

        public List<(string QuestType, int QuestNumber, int CurrentProgress)> QuestSave;

        public QuestManager()
        {
            string jsonFilePath;
            string jsonText;

            //플레이어의 진행에 따라 퀘스트 정보가 저장됨.
            //QuestType: 배열 인덱스의 의미 전달.
            //QuestNumber: 현재 진행해야 할 퀘스트의 index. (Log의 경우 최신 완료퀘의 index임.)
            //CurrentProgress: 퀘스트의 진행도(사실 몬스터퀘만을 위한 것)
            QuestSave = new List<(string QuestType, int QuestNumber, int CurrentProgress)>();
            jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\SaveData\QuestSave.json");
            jsonText = File.ReadAllText(jsonFilePath);
            QuestSave = JsonConvert.DeserializeObject<List<(string QuestType, int QuestNumber, int CurrentProgress)>>(jsonText);

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
            return StoryQuest[QuestSave[0].QuestNumber];
        }

        public Quest GetCurrentMonsterQuest()
        {
            MonsterQuest[QuestSave[1].QuestNumber].CurrentProgress = QuestSave[1].CurrentProgress;
            return MonsterQuest[QuestSave[1].QuestNumber];
        }

        public List<Quest> GetStoryLog()
        {
            List<Quest> storyLog = new List<Quest>();
            if (QuestSave[2].QuestNumber != -1)
                storyLog.GetRange(QuestSave[2].CurrentProgress, QuestSave[2].QuestNumber);
            return storyLog;
        }

        public List<Quest> GetEnemyLog()
        {
            List<Quest> monsterLog = new List<Quest>();
            if (QuestSave[3].QuestNumber != -1)
                monsterLog.GetRange(QuestSave[3].CurrentProgress, QuestSave[3].QuestNumber);
            return monsterLog;
        }

        public void SetMonsterQuest(Enemy deadEnemy)
        {
            var oldQ = QuestSave[1];
            int currentQ = oldQ.QuestNumber;
            int current = oldQ.CurrentProgress; 

            if (current == -1)
            {
                return;
            }
            else
            {
                if (currentQ - 1 == EnemyDataManager.instance.MonsterDB.IndexOf(deadEnemy))
                {
                    var newQ = (oldQ.QuestType, oldQ.QuestNumber, ++oldQ.CurrentProgress);

                    QuestSave[1] = newQ;
                }else if (currentQ == 0)
                {
                    var newQ = (oldQ.QuestType, oldQ.QuestNumber, ++oldQ.CurrentProgress);

                    QuestSave[1] = newQ;
                }
            }
        }
    }
}
