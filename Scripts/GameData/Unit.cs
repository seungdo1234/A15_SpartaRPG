
using Newtonsoft.Json;
using System;
using static System.Net.Mime.MediaTypeNames;

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
        [JsonProperty] public int Mana { get; protected set; }
        [JsonProperty] public int MaxMana { get; protected set; }

        protected Random random = new Random();
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

        public virtual (int damage, bool isCrit) Attack() // bool 반환형 = 치명타 여부
        {
            int critRate = random.Next(0, 101); // 치명타 확률
            int atkRange = random.Next(0, 21); // 공격력 오차범위
            float damage = Atk * (100 + (atkRange - 10)) * 0.01f; // 오차범위 적용한 데미지

            if (critRate <= CriticalChance)
            {
                return (Convert.ToInt32(Math.Round((damage * CriticalDamage))), true);
            }

            return (Convert.ToInt32(Math.Round(damage)), false);
        }

        public virtual void CostMana(int cost)
        {
            Mana -= cost;
        }
    }
}
