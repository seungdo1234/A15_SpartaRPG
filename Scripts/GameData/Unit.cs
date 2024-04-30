
using Newtonsoft.Json;
using System;

namespace TextRPG
{
    public class Unit
    {

        [JsonProperty] public string Name { get; protected set; }
        [JsonProperty] public int Level { get; protected set; }
        [JsonProperty] public float Atk { get; protected set; }
        [JsonProperty] public float Def { get; protected set; }
        [JsonProperty] public int Health { get; protected set; }
        [JsonProperty] public int MaxHealth { get; protected set; }
        [JsonProperty] public int AvoidChance { get; protected set; }
        [JsonProperty] public int CriticalChance { get; protected set; }
        [JsonProperty] public float CriticalDamage { get; protected set; }


        private Random random = new Random();
        public virtual bool IsDamaged(int damage) // 피격
        {
            int per = random.Next(0, 101);

            if(per <= AvoidChance)
            {
                return false;
            }

            Health -= (damage - Def) > 0 ? (int)(damage - Def) : 0;

            return true;
        }

        public virtual int Attack()
        {
            int per = random.Next(0, 101);

            if (per <= CriticalChance)
            {
                return (int)(Atk * per * 0.01f);
            }

            return (int)Atk;
        }
    }
}
