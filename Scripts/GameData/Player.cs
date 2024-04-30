using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection.Emit;

namespace TextRPG
{
    public class Player:Unit
    {
    
        // 플레이어 경험치
        private int[] levelExp = new int[10] { 5, 7, 10, 12, 15, 20, 25, 30, 40, 50 }; // 레벨 별 경험치 통

        public PlayerClass ePlayerClass { get;  set; }
        public int Gold { get; set; }
        [JsonProperty] public int Exp { get; private set; }
        public Item EquipAtkItem { get; set; }
        public Item EquipDefItem { get; set; }
        public Skills PlayerSkills { get; private set; }
        
        public Player(string name)
        {
            Name = name;
            Level = 1;
            Atk = 10;
            Def = 5;
            MaxHealth = 100;
            Health = MaxHealth;
            Gold = 10000;
            AvoidChance = 10;
            CriticalChance = 16;
            CriticalDamage = 1.6f;
            MaxMana = 100;
            Mana = MaxMana;
        }

        public void ChangePlayerClass(PlayerClass ePlayerClass)
        {
            // 플레이어의 직업에 따라 추가스탯 배정
            this.ePlayerClass = ePlayerClass;

            switch (ePlayerClass)
            {
                case PlayerClass.WARRIOR:
                    Health += 50;
                    Def += 5;
                    PlayerSkills = new WarriorSkills();
                    break;
                case PlayerClass.ARCHER:
                    CriticalChance += 9;
                    CriticalDamage += 0.9f;
                    break;
                case PlayerClass.THIEF:
                    AvoidChance += 10;
                    break;
                case PlayerClass.MAGICIAN:
                    MaxMana += 50;
                    Mana += 50;
                    break;
            }
        }

        public string GetPlayerClass(PlayerClass ePlayerClass) // 플레이어의 직업 별 이름 반환 
        {
            string playerClass = ePlayerClass switch
            {
                PlayerClass.WARRIOR => "전사",
                PlayerClass.ARCHER => "궁수",
                PlayerClass.THIEF => "도적",
                PlayerClass.MAGICIAN => "마법사",
                _ => "직업이 존재하지 않습니다." // default
            };

            return playerClass;
        }
        public float GetAtkValue() // 전체 공격력 반환
        {
            if (EquipAtkItem == null)
            {
                return Atk;
            }
            else
            {
                return Atk + EquipAtkItem.Value;
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
                return Def + EquipDefItem.Value;
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

        public override bool IsDamaged(int damage) // 피격시 true 회피시 false
        {
            int per = random.Next(0, 101);

            if (per <= AvoidChance) // 회피 성공 시 
            {
                return false;
            }

            Health -= (damage - GetDefValue()) > 0 ? (int)(damage - GetDefValue()) : 0;

            return true;            
        }

        public override (int damage, bool isCrit) Attack()
        {
            int critRate = random.Next(0, 101); // 치명타 확률
            int atkRange = random.Next(0, 21); // 공격력 오차범위
            float damage = GetAtkValue() * (100 + (atkRange - 10)) * 0.01f; // 오차범위 적용한 데미지

            if (critRate <= CriticalChance)
            {
                return (Convert.ToInt32(Math.Round((damage * CriticalDamage))), true);
            }

            return (Convert.ToInt32(Math.Round(damage)), false);
        }
    }
}
