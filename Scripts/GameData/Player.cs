using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TextRPG
{
    public enum PlayerClass { defalut, Warrior, Archer, Thief, Magician }
    public class Player
    {
        // 캐릭터 정보
        private PlayerClass playerClass;
        private string name;
        private int level;
        private float atk;
        private float def;
        private int health;
        private int maxHealth;
        private int gold;

        // 플레이어 경험치
        private int exp; // 현재 경험치
        private int[] levelExp = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; // 레벨 별 경험치 통

        // 플레이어가 장착한 장비
        private Item equipAtkItem;
        private Item equipDefItem;

        public PlayerClass PlayerClass { get => playerClass; set { playerClass = value; } }
        public string Name { get => name; set { name = value; } }
        public int Level { get => level; set { level = value; } }

        public float Atk{ get => atk; set { atk = value; }}
        public float Def { get => def; set { def = value; } }

        public int Health { get => health; set { health = value; } }
        public int MaxHealth { get => maxHealth; set { maxHealth = value; } }
        public int Gold { get => gold; set { gold = value; } }
        public int Exp { get => exp; set { exp = value; } }
        public Item EquipAtkItem { get => equipAtkItem; set { equipAtkItem = value; } }
        public Item EquipDefItem { get => equipDefItem; set { equipDefItem = value; } }

        public Player(string name)
        {
            this.name = name;
            level = 1;
            atk = 10;
            def = 5;
            maxHealth = 100;
            health = maxHealth;
            gold = 10000;
        }

        public string GetPlayerClass(PlayerClass _playerClass) // 플레이어의 직업 별 이름 반환 
        {
            string playerClass = _playerClass switch
            {
                PlayerClass.Warrior => "전사",
                PlayerClass.Archer => "궁수",
                PlayerClass.Thief => "도적",
                PlayerClass.Magician => "마법사",
                _ => "직업이 존재하지 않습니다." // default
            };

            return playerClass;
        }
        public float GetAtkValue() // 전체 공격력 반환
        {
            if (equipAtkItem == null)
            {
                return atk;
            }
            else
            {
                return atk + equipAtkItem.Value;
            }
        }
        public float GetDefValue() // 전체 방어력 반환
        {
            if (equipDefItem == null)
            {
                return def;
            }
            else
            {
                return def + equipDefItem.Value;
            }
        }

        public void OnDamaged(int health) // 피격
        {
            this.health -= health;
        }

        public void RecoveryHealth(int health)
        {
            if (this.health + health > maxHealth)
            {
                this.health = maxHealth;
            }
            else
            {
                this.health += health;
            }
        }
        public void ExpUp() // 경험치 상승
        {
            if (++exp == levelExp[level - 1])
            {
                LevelUp();
                level++;
                exp = 0;
            }
        }

        private void LevelUp() // 레벨업 
        {
            atk += 0.5f;
            def += 1;
        }
    }
}
