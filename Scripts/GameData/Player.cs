using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TextRPG
{
    public class Player
    {
    
        // 플레이어 경험치
        private int[] levelExp = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; // 레벨 별 경험치 통

        public PlayerClass PlayerClass { get;  set; }
        [JsonProperty] public string Name { get; private set; }
        [JsonProperty] public int Level { get; private set; }
        [JsonProperty] public float Atk { get; private set; }
        [JsonProperty]  public float Def { get; private set; }
        [JsonProperty]  public int Health { get; private set; }
        [JsonProperty] public int MaxHealth { get; private set; }
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
            MaxHealth = 100;
            Health = MaxHealth;
            Gold = 10000;
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

        public void OnDamaged(int health) // 피격
        {
            this.Health -= health;
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
    }
}
