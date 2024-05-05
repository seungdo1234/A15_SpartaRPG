using Newtonsoft.Json;
using System.Xml.Linq;
using TextRPG.Scripts.Manager;

namespace TextRPG
{
    public class Skill
    {  
        //public int ID { get; private set; }
        public string Name { get; private set; }
        public int MaxTargetCount { get; private set; }
        public bool IsMultiTarget { get; private set; }
        public int ManaCost { get; private set; } // 마나 소모량
        public string Content { get; private set; } // 스킬 설명
        public EUnitType UnitType { get; private set; }

        protected Random random = new Random();

        [JsonConstructor]
        public Skill(string name, int maxTargetCount, bool isMultiTarget, int manaCost, string content, EUnitType unitType)
        {
            Name = name;
            MaxTargetCount = maxTargetCount;
            IsMultiTarget = isMultiTarget;
            ManaCost = manaCost;
            Content = content;
            UnitType = unitType;
        }

        

        public Skill(int id)
        {
            Skill skill;
            SkillDataManager.GetInstance().SkillDictionary.TryGetValue(id, out skill);

            Name = skill.Name;
            MaxTargetCount = skill.MaxTargetCount;
            IsMultiTarget = skill.IsMultiTarget;
            ManaCost = skill.ManaCost;
            Content = skill.Content;
            UnitType = skill.UnitType;
        }

        public virtual string CastSkill(Unit caster, Unit target)
        {
            string result;
            string? critStr = caster.IsCriticalHit();
            float critRate = critStr != null ? caster.CriticalDamage : 1f;             
            float skillRate = IsMultiTarget ? 1.5f : 2f;            
            int damage = caster.GetDamagePerHit();
                                    
            damage = Convert.ToInt32(Math.Round(damage * skillRate * critRate));
            result = target.OnDamaged(damage);
            result += critStr;

            return result;
        }
    }
}
