﻿using Newtonsoft.Json;
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
        public List<Enemy> MonsterDB { get; private set; }
        //스테이지별 몬스터 리스트
        private List<Enemy> SpawnMonsters;
        private Enemy BossMonster;
        

        public void Init()
        {
            MonsterDB = new List<Enemy>();
            SpawnMonsters = new List<Enemy>();

            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\SaveData\Enemy.json");
            string jsonText = File.ReadAllText(jsonFilePath);

            MonsterDB = JsonConvert.DeserializeObject<List<Enemy>>(jsonText);
        }

        public List<Enemy> GetSpawnMonsters(int CurrentDungeonLevel, EDungeonDifficulty dif)
        {
            float statMultiplier = GetStatMultiplier(dif);
            int totalLevelLimit = CurrentDungeonLevel * 2;
            int maxLevel = 10;
            int[] monsterLevels = randomMonsterEncount(totalLevelLimit, maxLevel);
             
            SpawnMonsters.Clear();
            // 5.4 J => 던전 보상 (경험치) 추가
            GameManager.instance.Dungeon.BattleExp = 0;

            foreach (int i in monsterLevels)
            {
                if (i == 0) continue;
                Enemy newEnemy = new Enemy(MonsterDB[i - 1], statMultiplier);
                SpawnMonsters.Add(newEnemy);
                GameManager.instance.Dungeon.BattleExp += newEnemy.Level;
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
            BossMonster = new Boss();
            return BossMonster;
        }

        private float GetStatMultiplier(EDungeonDifficulty dif)
        {
            switch (dif)
            {
                case EDungeonDifficulty.EASY:
                    return 0.8f;
                case EDungeonDifficulty.NORMAL:
                    return 1.0f;
                case EDungeonDifficulty.HARD:
                    return 1.5f;
                default:
                    return 1.0f; // 기본 값으로 보통 난이도 설정
            }
        }
    }
}
