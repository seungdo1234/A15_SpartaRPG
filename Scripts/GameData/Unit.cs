
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
        [JsonProperty] public int Phase { get; protected set; } // 05.03 W 해금되는 스킬의 갯수
        [JsonProperty] public ECrowdControlType CrowdControlFlag { get; protected set; } // 05.05 W CC상태 
        [JsonProperty] public List<Skill> Skills { get; protected set; }
        [JsonProperty] public List<DeBuff> DeBuffs { get; protected set; }

        public delegate void DebuffAction(Unit target); // 05.05 W 디버프 메소드를 저장할 델리게이트 & 이벤트핸들러
        public event DebuffAction? DebuffActiveHandler;

        protected Random random = new Random();
        public virtual string OnDamaged(int damage) // 최소 데미지 1
        {
            int endDamage = Convert.ToInt32(Math.Round(damage - Def));
            endDamage = endDamage > 0 ? endDamage : 1;
                
            Health -= endDamage;
            return $"[데미지 {endDamage}] ";
        }
        public virtual string OnDamagedDenyDef(int damage) // 최소 데미지 1
        {   
            Health -= damage;
            return $"[데미지 {damage}] ";
        }

        public string? IsCriticalHit()
        {
            int critRate = random.Next(0, 101); // 치명타 확률
            if (critRate <= CriticalChance)
            {                
                return "Critical!! ";
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

            if (CheckCrowdControl(ECrowdControlType.BLIND) || avoidRange <= AvoidChance) // 회피 시 리턴 05.05 W 실명 추가
            {
                return "Miss!!";
            }

            damage = Convert.ToInt32(Math.Round(damage * critRate));
            result = target.OnDamaged(damage);
            result += critStr;

            return result;
        }

        public void OnDebuffActive() // 05.05 W 이벤트핼들러 실행 함수
        {
            //Console.WriteLine($"[{Name}의 디버프]");
            DebuffActiveHandler?.Invoke(this);
        }

        public virtual void CostMana(int cost)
        {
            Mana -= cost;
        }

        // 체력 및 마나 회복 함수
        public virtual void RecoveryHealth(int health)
        {
            if (Health + health > MaxHealth)
            {
                Health = MaxHealth;
            }
            else
            {
                Health += health;
            }
        }

        public virtual void RecoveryMana(int mana)
        {
            if (Mana + mana > MaxMana)
            {
                Mana = MaxMana;
            }
            else
            {
                Mana += mana;
            }
        }

        public bool CheckCrowdControl(ECrowdControlType type)
        {
            if ((CrowdControlFlag & type) > 0) return true; // 05.05 W bit flag 해당타입 활성화 확인
            return false;
        }

        public void SetCrowdControl(ECrowdControlType type)
        {
            CrowdControlFlag |= type; // 05.05 W bit flag 해당타입 추가
        }

        public void DispelCrowdControl(ECrowdControlType type)
        {
            CrowdControlFlag &= ~type; // 05.05 W bit flag 해당타입 제거
        }
    }
}
