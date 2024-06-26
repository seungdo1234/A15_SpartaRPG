﻿
namespace TextRPG
{

    public class DungeonManager : DungeonData
    {
        public EDungeonResultType DungeonResultType { get; set; }
        public EDungeonDifficulty dif {  get; set; }
        public RandomReward RandomReward { get; private set; }
        public int BattleExp {  get;  set; } // 전투 경험치 보상
        public int PrevHealth {  get; set; }
        public PlayerRewards PlayerRewards { get; private set; }


        public bool IsBossFightAvailable { get; set; } // 5.5 A 보스전 플래그 추적

        public DungeonManager()
        {
            EnemyDataManager.instance.Init();
            RandomReward = new RandomReward();
        }

        // 던전 보상 만드는 함수
        public Reward GetDungeonReward()
        {
            Reward reward =  RandomReward.GetRandomReward();

            PlayerRewards.rewardEquipItems.Add(reward.rewardEquipItem);
            PlayerRewards.totalGold += reward.gold;
            PlayerRewards.currentGold = reward.gold;

            return reward;
        }

        public void RewardInit()
        {
            PlayerRewards = new PlayerRewards();
        }

        
        public void DungeonLevelUp()
        {
            CurrentDungeonLevel += (int)dif;

            if(CurrentDungeonLevel > MaxDungeonLevel)
            {
                CurrentDungeonLevel = MaxDungeonLevel;
            }
        }
        public void DungeonInit()
        {
            RewardInit();
            CurrentDungeonLevel = 0;
        }

    }
}
