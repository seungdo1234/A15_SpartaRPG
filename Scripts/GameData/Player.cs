using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace TextRPG
{
    public class Player:Unit
    {
        // 플레이어 경험치
        private int[] levelExp = new int[10] { 5, 7, 10, 12, 15, 20, 25, 30, 40, 50 }; // 레벨 별 경험치 통

        public EUnitType ePlayerClass { get;  set; }
        public int Gold { get; set; }
        [JsonProperty] public int Exp { get; private set; }
        public float EquipAtkItem { get; set; }
        public float EquipDefItem { get; set; }    
        public EEquipItemType EquipItemFlag { get; set; }


        public Player(string name)
        {
            Name = name;
            Level = 1;
            Atk = 1000;
            Def = 500;
            MaxHealth = 10000;
            Health = MaxHealth;
            Gold = 10000;
            AvoidChance = 10;
            CriticalChance = 16;
            CriticalDamage = 1.6f;
            MaxMana = 10000;
            Mana = MaxMana;
            base.Skills = new List<SkillData>();
        }



        public void ChangePlayerClass(EUnitType ePlayerClass)
        {
            // 플레이어의 직업에 따라 추가스탯 배정
            this.ePlayerClass = ePlayerClass;

            switch (ePlayerClass)
            {
                case EUnitType.WARRIOR:
                    MaxHealth += 50; /// 수정이 필요함
                    Health += 50;
                    Def += 5;
                    Skills.Add(new SkillData(0));
                    Skills.Add(new SkillData(1));
                    Skills.Add(new CrisisEvasion(2));                    
                    break;
                case EUnitType.ARCHER:
                    CriticalChance += 9;
                    CriticalDamage += 0.9f;
                    Skills.Add(new SkillData(3));
                    Skills.Add(new SkillData(4));
                    Skills.Add(new WeaknessSniping(5));
                    break;
                case EUnitType.THIEF:
                    AvoidChance += 10;
                    Skills.Add(new SkillData(6));
                    Skills.Add(new SkillData(7));
                    Skills.Add(new Assassination(8));
                    break;
                case EUnitType.MAGICIAN:
                    MaxMana += 50;
                    Mana += 50;
                    Skills.Add(new SkillData(9));
                    Skills.Add(new SkillData(10));
                    Skills.Add(new ChainLighting(11));
                    break;
            }
        }

        public string GetPlayerClass(EUnitType ePlayerClass) // 플레이어의 직업 별 이름 반환 
        {
            string playerClass = ePlayerClass switch
            {
                EUnitType.WARRIOR => "전사",
                EUnitType.ARCHER => "궁수",
                EUnitType.THIEF => "도적",
                EUnitType.MAGICIAN => "마법사",
                _ => "직업이 존재하지 않습니다." // default
            };

            return playerClass;
        }

        public void SwitchingEquipItem(EquipItem equipItem)
        {
            if (equipItem.IsEquip)
            {
                EquipAtkItem += equipItem.AtkValue;
                EquipDefItem += equipItem.DefValue;
            }
            else
            {
                EquipAtkItem -= equipItem.AtkValue;
                EquipDefItem -= equipItem.DefValue;
            }
            
        }

        public float GetAtkValue() // 전체 공격력 반환
        {
            if (EquipAtkItem == null)
            {
                return Atk;
            }
            else
            {
                return Atk + EquipAtkItem;
            }
        }
        public float GetDefValue() // 전체 방어력 반환
        {
            if (EquipDefItem == null)
            {
                return Def;
            }
            else
            {
                return Def + EquipDefItem;
            }
        }

        public void RecoveryHealth(int health)
        {
            if (this.Health + health > MaxHealth)
            {
                this.Health = MaxHealth;
            }
            else
            {
                this.Health += health;
            }
        }
        public void ExpUp(int exp) // 경험치 상승
        {
            // 4.30 J => 경험치 상승 수정
            Exp += exp;

            if(Exp >= levelExp[Level - 1])
            {
                Exp-= levelExp[Level-1];
                Level++;
                LevelUp();
            }
        }

        private void LevelUp() // 레벨업 
        {
            Atk += 0.5f;
            Def += 1;
        }

        public override string OnDamaged(int damage) // 회피시 0
        {
            int endDamage = (damage - GetDefValue()) > 0 ? (int)(damage - GetDefValue()) : 1;
            Health -= endDamage;
            return $"[데미지 {endDamage}] ";
        }

        public override int GetDamagePerHit() // bool 반환형 = 치명타 여부
        {
            int atkRange = random.Next(0, 21); // 공격력 오차범위
            float damage = GetAtkValue() * (100 + (atkRange - 10)) * 0.01f; // 오차범위 적용한 데미지 

            return Convert.ToInt32(Math.Round(damage));
        }

        public override string Attack(Unit target)
        {
            int avoidRange = random.Next(0, 101); // 회피 범위
            string result;
            string? critStr = IsCriticalHit();
            float critRate = critStr != null ? CriticalDamage : 1f;
            int damage = GetDamagePerHit();

            if (avoidRange <= AvoidChance) // 회피 시 리턴
            {
                return "Miss!! ";
            }

            damage = Convert.ToInt32(Math.Round(damage * critRate));
            result = target.OnDamaged(damage);
            result += critStr;

            return result;
        }
    }
}
