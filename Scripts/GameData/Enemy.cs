
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
            base.Skills = new List<Skill>();
            base.DeBuffs = new List<DeBuff>();

            switch (e.Level)
            {
                case 1:
                    Skills.Add(new Bite(12));
                    break;
                case 2:
                    Skills.Add(new Chomp(13));
                    break;
                case 3:
                    Skills.Add(new LifeDrain(14));
                    break;
                case 4:
                    Skills.Add(new DeathSentence(15));
                    break;
                case 5:
                    Skills.Add(new BlackOut(16));
                    break;
                case 6:
                    Skills.Add(new SongOfSiren(17));
                    break;
                case 7:
                    Skills.Add(new Roar(18));
                    break;
                case 8:
                    Skills.Add(new Inferno(19));
                    break;
                case 9:
                    Skills.Add(new FuryOfBeast(20));
                    break;
                case 10:
                    Skills.Add(new Venom(21));
                    break;
            }
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
