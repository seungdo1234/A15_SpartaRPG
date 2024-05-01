
using TextRPG.Scripts.Manager;

namespace TextRPG
{
    public class Enemy : Unit
    {
        private List<EnemySkill> SkillList {  get; set; } = new List<EnemySkill>(); // 원래는 private 사용을 위해서는 public 필요

        public Enemy()
        {
            Health = MaxHealth;
            AvoidChance = 10;
            CriticalChance = 16;
            CriticalDamage = 1.6f;
            // AssignSkills(); // 5:55 5월 1일

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
           // SkillList = new List<EnemySkill>(e.SkillList); // 5:55 5월 1일

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

        /*
        // 적 스킬 할당을 위해 KTH가 생성, 5월 1일 오후5시 55분
        private void AssignSkills()
        {
            // SkillDataManager에서 스킬을 가져옴
            var skillManager = SkillDataManager.GetInstance();

            // 예시로 첫 번째 스킬을 추가
            if (skillManager.SkillDictionary.TryGetValue(0, out SkillData skillData))
            {
                var skill = new EnemySkill(skillData.Name, Level, skillData.GetDamage(), skillData.Content);
                AddMonsterSkill(skill);
            }
        }
        */

        public void AddMonsterSkill(EnemySkill enemySkill)
        {
            SkillList.Add(enemySkill);
        }
        
    }
}
