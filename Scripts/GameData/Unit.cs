
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
        public void OnDamaged(int health) // 피격
        {
            this.Health -= health;
        }

        virtual public void Attack(Unit unit)
        {
            int per = random.Next(1, 101);

            if (per <= CriticalChance)
            {

            }
        }
    }
}
