using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class EnemyDataManager
    {
        public static EnemyDataManager instance = new EnemyDataManager();

        //전체 몬스터 리스트
        private List<Enemy> MonsterDB;
        //스테이지별 몬스터 리스트
        private List<Enemy> SpawnMonsters;
        private Enemy BossMonster;
        

        public void Init()
        {
            MonsterDB = new List<Enemy>();
            SpawnMonsters = new List<Enemy>();

            MonsterDB.Add(new Enemy("사나운 토끼", 1, 5, 0, 20));
            MonsterDB[0].AddMonsterSkill(new EnemySkill("앞니물기", MonsterDB[0].Level, MonsterDB[0].Atk,
                "목을 노려 평소보다 강하다.")); //기본: 공격 강화

            MonsterDB.Add(new Enemy("노을의 늑대개", 2, 10, 3, 30));
            MonsterDB[1].AddMonsterSkill(new EnemySkill("달그림자", MonsterDB[1].Level, MonsterDB[1].Atk,
                "늑대의 암습은 도망칠 수 없다.")); //가능하면 회피 무시

            MonsterDB.Add(new Enemy("강을 건넌 사람", 3, 15, 6, 40));
            MonsterDB[2].AddMonsterSkill(new EnemySkill("생자탐닉", MonsterDB[2].Level, MonsterDB[2].Atk,
                "산자의 정기를 갈취한다.")); //가능하면 자기체력 소량 회복

            MonsterDB.Add(new Enemy("흐느끼는 유령", 4, 20, 9, 50));
            MonsterDB[3].AddMonsterSkill(new EnemySkill("사망예고", MonsterDB[3].Level, MonsterDB[3].Atk,
                "당신이 죽길 기다리는 흐느낌이다.")); //가능하면 

            MonsterDB.Add(new Enemy("늘어나는 그림자", 5, 25, 12, 60));
            MonsterDB[4].AddMonsterSkill(new EnemySkill("시선고정", MonsterDB[4].Level, MonsterDB[4].Atk,
                "바라볼수록 그는 강해진다.")); //가능하면 스킬 빼고 턴마다 공격력 미약하게 증가.

            MonsterDB.Add(new Enemy("노래하는 물고기", 6, 30, 15, 70));
            MonsterDB[5].AddMonsterSkill(new EnemySkill("바다유혹", MonsterDB[5].Level, MonsterDB[5].Atk,
                "당신의 마음을 녹여 생존을 꾀한다.")); //가능하면 다음턴 플레이어 크리티컬 불가.or다음몬스터 턴 회피불가.

            MonsterDB.Add(new Enemy("흰머리 호랑이", 7, 35, 18, 80));
            MonsterDB[6].AddMonsterSkill(new EnemySkill("흉내내기", MonsterDB[6].Level, MonsterDB[6].Atk,
                "당신은 혹시 하는 생각을 멈출수 없었다.")); //가능하면 플레이어 스킬 블록

            MonsterDB.Add(new Enemy("외로운 불귀신", 9, 45, 24, 100));
            MonsterDB[7].AddMonsterSkill(new EnemySkill("불태워라", MonsterDB[7].Level, MonsterDB[7].Atk,
                "당신께 꺼지지 않는 불을 선사한다.")); //가능하면 플레이어에 지속데미지

            MonsterDB.Add(new Enemy("독수리사자", 8, 40, 21, 90));
            MonsterDB[8].AddMonsterSkill(new EnemySkill("짐승의왕", MonsterDB[8].Level, MonsterDB[8].Atk,
                "날짐승과 들짐승의 왕의 패기가 당신을 짓누른다.")); //가능하면 플레이어 스턴
            

            MonsterDB.Add(new Enemy("녹안의 악마", 10, 50, 27, 120));
            MonsterDB[9].AddMonsterSkill(new EnemySkill("시각갈취", MonsterDB[9].Level, MonsterDB[9].Atk,
                "질투는 당신의 눈을 멀게 한다.")); //가능하면 콘솔 화면의 일부 출력 방해??? SetCursorPosition으로 덮?

            BossMonster = (new Enemy("휘몰아치는 강철이", 0, 100, 50, 1000));  //보스lv은 특수처리.
            BossMonster.AddMonsterSkill(new EnemySkill("", 0, 100, "")); //1페 (체력 70%)
            BossMonster.AddMonsterSkill(new EnemySkill("", 0, 100, "")); //2페 (체력 40%)
            BossMonster.AddMonsterSkill(new EnemySkill("", 0, 100, "")); //3페 (체력 10%)
        }

        /*내가 만든 것
        public List<Enemy> GetSpawnMonsters(int CurrentDungeonLevel)
        {
            SpawnMonsters.Clear(); // 이전에 선택된 몬스터들을 클리어
            Random random = new Random();

            // 현재 던전 레벨에 따라 선택 가능한 최대 몬스터 레벨을 계산
            int maxAvailableLevel = CurrentDungeonLevel * 2;

            // 몬스터 수를 랜덤으로 결정 (예: 1~3)
            int numberOfMonsters = random.Next(1, 4);

            for (int i = 0; i < numberOfMonsters; i++)
            {
                // 선택 가능한 몬스터들 중에서 랜덤으로 하나를 선택
                List<Enemy> availableMonsters = MonsterDB.Where(monster => monster.Level <= maxAvailableLevel).ToList();
                if (availableMonsters.Count > 0)
                {
                    Enemy selectedMonster = availableMonsters[random.Next(availableMonsters.Count)];
                    // 선택된 몬스터를 스폰 몬스터 목록에 추가
                    SpawnMonsters.Add(selectedMonster);
                }
            }

            return SpawnMonsters;
        }
        */

        public List<Enemy> GetSpawnMonsters(int CurrentDungeonLevel)
        {
            int totalLevelLimit = CurrentDungeonLevel * 2;
            int maxLevel = 10;
            int[] monsterLevels = randomMonsterEncount(totalLevelLimit, maxLevel);

            foreach (int i in monsterLevels)
            {
                if (i == 0) continue;

                SpawnMonsters.Add(MonsterDB[i - 1]);
            }

            return SpawnMonsters;
        }
        

        private int[] randomMonsterEncount(int totalLevelLimit, int maxLevel)
        {
            List<int[]> levelCombinations = new List<int[]>();
            int[] selectedCombination;
            Random random = new Random();

            maxLevel = Math.Min(totalLevelLimit, maxLevel);

            for (int i = 0; i <= maxLevel; i++)
            {
                for (int j = 0; i + j <= maxLevel; j++)
                {
                    for (int k = 0; i + j + k <= maxLevel; k++)
                    {
                        int l = totalLevelLimit - (i + j + k);
                        if (l >= 0 && l <= maxLevel)
                        {
                            levelCombinations.Add(new int[] { i, j, k, l });
                        }
                    }
                }
            }

            selectedCombination = levelCombinations[random.Next(levelCombinations.Count)];
            return selectedCombination;
        }

        public Enemy GetBoss()
        {
            return BossMonster;
        }
    }
}
