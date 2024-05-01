
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
        [JsonProperty] public List<SkillData> Skills { get; protected set; }

        protected Random random = new Random();
        public virtual void OnDamaged(int damage) // 최소 데미지 1
        {   
            Health -= (damage - Def) > 0 ? (int)(damage - Def) : 1;           
        }

        public string? IsCriticalHit()
        {
            int critRate = random.Next(0, 101); // 치명타 확률
            if (critRate <= CriticalChance)
            {                
                return "Critical!!";
            }
            return null;
        }

        public virtual int GetDamagePerHit() // bool 반환형 = 치명타 여부
        {
            int atkRange = random.Next(0, 21); // 공격력 오차범위
            float damage = Atk * (100 + (atkRange - 10)) * 0.01f; // 오차범위 적용한 데미지 
            
            return Convert.ToInt32(Math.Round(damage));
        }

        public virtual string Attack(Unit target) // bool 반환형 = 치명타 여부
        {   
            int avoidRange = random.Next(0, 101); // 회피 범위
            string result;
            string? critStr = IsCriticalHit();
            float critRate = critStr != null ? CriticalDamage : 1f;                        
            int damage = GetDamagePerHit();

            if (avoidRange <= AvoidChance) // 회피 시 리턴
            {
                return "Miss!!";
            }

            damage = Convert.ToInt32(Math.Round(damage * critRate));
            target.OnDamaged(damage);
            result = $"[데미지 {damage}] " + critRate;

            return result;
        }

        public virtual void CostMana(int cost)
        {
            Mana -= cost;
        }
    }
}
