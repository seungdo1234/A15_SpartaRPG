using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Xml.Linq;

namespace TextRPG
{
    public class Player:Unit
    {
    
        // 플레이어 경험치
        private int[] levelExp = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; // 레벨 별 경험치 통

        public PlayerClass ePlayerClass { get;  set; }
        public int Gold { get; set; }
        [JsonProperty] public int Exp { get; private set; }
        public Item EquipAtkItem { get; set; }
        public Item EquipDefItem { get; set; }
        
        public Player(string name)
        {
            Name = name;
            Level = 1;
            Atk = 10;
            Def = 5;
            MaxHealth = 10000;
            Health = MaxHealth;
            Gold = 10000;
            AvoidChance = 10;
            CriticalChance = 16;
            CriticalDamage = 1.6f;
            MaxMana = 100;
            Mana = MaxMana;
        }

        public void ChangePlayerClass(PlayerClass playerClass)
        {
            this.ePlayerClass = playerClass;

            switch (playerClass)
            {
                case PlayerClass.WARRIOR:
                    Health += 50;
                    Def += 5;
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

        public string GetPlayerClass(PlayerClass _playerClass) // 플레이어의 직업 별 이름 반환 
        {
            string playerClass = _playerClass switch
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
        public void ExpUp() // 경험치 상승
        {
            if (++Exp == levelExp[Level - 1])
            {
                LevelUp();
                Level++;
                Exp = 0;
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

        public override int Attack()
        {
            int per = random.Next(0, 101);

            if (per <= CriticalChance)
            {
                return (int)(GetAtkValue() * CriticalDamage);
            }

            return (int)GetAtkValue();
        }
    }
}
