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

        public void Init()
        {
            MonsterDB = new List<Enemy>();
            SpawnMonsters = new List<Enemy>();

            MonsterDB.Add(new Enemy("중세 토끼", 1, 5, 0, 20, 
                1, 1, 1));
            MonsterDB.Add(new Enemy("노을의 늑대개", 2, 10, 3, 30,
                1, 1, 1));
            MonsterDB.Add(new Enemy("빨간 망토", 3, 15, 6, 40,
                1, 1, 1));
            MonsterDB.Add(new Enemy("흐느끼는 유령", 4, 20, 9, 50,
                1, 1, 1));
            MonsterDB.Add(new Enemy("작은 어둑시니", 5, 25, 12, 60,
                1, 1, 1));
            MonsterDB.Add(new Enemy("", 6, 30, 15, 70,
                1, 1, 1));
            MonsterDB.Add(new Enemy("ㅇ", 7, 35, 18, 80,
                1, 1, 1));
            MonsterDB.Add(new Enemy("", 8, 40, 21, 90,
                1, 1, 1));
            MonsterDB.Add(new Enemy("흰머리 호랑이", 9, 45, 24, 100,
                1, 1, 1));
            MonsterDB.Add(new Enemy("a", 10, 50, 27, 120,
                1, 1, 1));

            MonsterDB.Add(new Enemy("휘몰아치는 강철이", 20, 100, 50, 500,
                1, 1, 1));
        }
    }
}
