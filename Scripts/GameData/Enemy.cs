
namespace TextRPG
{
    public class Enemy : Unit
    {
        private List<EnemySkill> SkillList {  get; set; } = new List<EnemySkill>();

        public Enemy()
        {
            Health = MaxHealth;
            AvoidChance = 10;
            CriticalChance = 16;
            CriticalDamage = 1.6f;
        }

        public Enemy(string Name, int Level, float Atk, float Def, int MaxHealth)
        {
            this.Name = Name;
            this.Level = Level;
            this.Atk = Atk;
            this.Def = Def;
            this.MaxHealth = MaxHealth; 
            Health = MaxHealth;
            AvoidChance = 10;
            CriticalChance = 16;
            CriticalDamage = 1.6f;
        }

        public override string ToString()
        {
            if (Level == 0)
            {
                return $"Lv.Max {Name}\tHP {Health}/{MaxHealth}";
            }
            else if (Level > 9)
            {
                return $"Lv.{Level} {Name}\tHP {Health}/{MaxHealth}";
            }
            else 
            {
                return $"Lv.{Level}  {Name}\tHP {Health}/{MaxHealth}";
            }
        }

        public void AddMonsterSkill(EnemySkill enemySkill)
        {
            SkillList.Add(enemySkill);
        }
    }
}
