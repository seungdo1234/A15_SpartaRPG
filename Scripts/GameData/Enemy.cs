
using TextRPG.Scripts.Manager;

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

        // 5.3 A 매개변수 추가, 해당 매개변수는 EnemyData
        public Enemy(Enemy e, float statMultiplier)
        {
            Name = e.Name;
            Level = e.Level;
            Atk = e.Atk * statMultiplier;
            Def = e.Def * statMultiplier;
            Health = (int)Math.Round(e.Health * statMultiplier);
            MaxHealth = (int)Math.Round(e.MaxHealth * statMultiplier);
            AvoidChance = e.AvoidChance;
            CriticalChance = e.CriticalChance;
            CriticalDamage = e.CriticalDamage * statMultiplier;
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
