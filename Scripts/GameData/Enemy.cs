
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

        public Enemy(Enemy e)
        {
            Name = e.Name;
            Level = e.Level;
            Atk = e.Atk;
            Def = e.Def;
            Health = e.Health;
            MaxHealth = e.MaxHealth;
            AvoidChance = e.AvoidChance;
            CriticalChance = e.CriticalChance;
            CriticalDamage = e.CriticalDamage;
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
