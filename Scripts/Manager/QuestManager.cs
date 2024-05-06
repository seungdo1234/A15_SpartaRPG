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
            if (GameManager.instance.Dungeon.CurrentDungeonLevel > QuestSave[0].CurrentProgress)
            {
                var oldQ = QuestSave[0];
                QuestSave[0] = (oldQ.QuestType, oldQ.QuestNumber, oldQ.CurrentProgress);
            }

            StoryQuest[QuestSave[0].QuestNumber].CurrentProgress = QuestSave[0].CurrentProgress;

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
                storyLog = StoryQuest.GetRange(QuestSave[2].CurrentProgress, QuestSave[2].QuestNumber);
            return storyLog;
        }

        public List<Quest> GetEnemyLog()
        {
            List<Quest> monsterLog = new List<Quest>();
            if (QuestSave[3].QuestNumber != -1)
                monsterLog = MonsterQuest.GetRange(QuestSave[3].CurrentProgress, QuestSave[3].QuestNumber);
            return monsterLog;
        }

        public void SetMonsterQuest(Enemy deadEnemy)
        {
            int deadEnemyIndex = EnemyDataManager.instance.MonsterDB.FindIndex(monster => monster.Name == deadEnemy.Name); // 5.6 A 몬스터 추적
            var oldQ = QuestSave[1];
            int currentQ = oldQ.QuestNumber;
            int current = oldQ.CurrentProgress; 

            if (current == -1)
            {
                return;
            }
            else
            {    // 5.6 A 몬스터 추적을 위한 코드 수정
                if (currentQ - 1 == deadEnemyIndex || currentQ == 0)
                {
                    var newQ = (oldQ.QuestType, oldQ.QuestNumber, ++oldQ.CurrentProgress);
                    QuestSave[1] = newQ;
                }
            }   //
        }

        // 5.5 A : 다음 스토리 퀘스트로 이동
        public void AdvanceToNextStoryQuest()
        {
            if (QuestSave[0].QuestNumber < StoryQuest.Count - 1)
            {
                QuestSave[0] = (QuestSave[0].QuestType, QuestSave[0].QuestNumber + 1, 0);
            }
        }
    }
}
