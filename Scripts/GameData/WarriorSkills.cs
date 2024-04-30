
using System.Xml.XPath;
using static System.Net.Mime.MediaTypeNames;

namespace TextRPG
{
    public class WarriorSkills : Skills
    {
        public WarriorSkills() : base()
        {
            AddSkill(new SkillData("강베기", 1, false, 10, "적 하나에게 무기로 강력한 일격을 날립니다."));
            AddSkill(new SkillData("가로베기", 3, true, 20, "무기를 크게 휘둘러 최대 3명의 적을 공격합니다."));
            AddSkill(new SkillData("위기모면", 1, false, 30, "적 하나에게 잃은 체력 비례 피해를 2회 가격합니다."));
        }

        public override List<(int damage, bool isCrit)>? GetSkillDamages(int id, Unit unit, int enemyCount)
        {
            SkillData? skill = SkillList.Find(x => x.ID == id);
            if (skill == null) return null; // 스킬ID에 해당하는 스킬이 없으면 null 반환

            switch (skill.Name)
            {
                case "강베기":
                    return StrongStrike(skill.ID, unit);
                case "가로베기":
                    return HorizontalStrike(skill.ID, unit, enemyCount);                    
                case "위기모면":
                    return CrisisEvasion(skill.ID, unit);                    
            }

            return null;
        }

        private List<(int damage, bool isCrit)> StrongStrike(int id, Unit player)
        {
            SkillData skill = SkillList.Find(x => x.ID == id);
            List<(int damage, bool isCrit)> result = new List<(int damage, bool isCrit)>();

            player.CostMana(skill.ManaCost); // 마나소모
            (int damage, bool isCrit) hit = player.Attack(); // 1회 타격정보
            hit.damage = hit.damage * 2; // 스킬 데미지 계산

            result.Add(hit);

            return result;
        }

        private List<(int damage, bool isCrit)> HorizontalStrike(int id, Unit player, int enemyCount)
        {
            SkillData skill = SkillList.Find(x => x.ID == id);
            List<(int damage, bool isCrit)> result = new List<(int damage, bool isCrit)>();
            int count = skill.MaxTargetCount > enemyCount ? enemyCount : skill.MaxTargetCount;

            player.CostMana(skill.ManaCost); 
            for (int i = 0; i < count; i++)
            {
                (int damage, bool isCrit) hit = player.Attack(); // 1회 타격정보
                hit.damage = Convert.ToInt32(Math.Round(hit.damage * 1.5f)); 
                result.Add(hit);
            }                        

            return result;
        }

        private List<(int damage, bool isCrit)> CrisisEvasion(int id, Unit player)
        {
            SkillData skill = SkillList.Find(x => x.ID == id);
            List<(int damage, bool isCrit)> result = new List<(int damage, bool isCrit)>();

            player.CostMana(skill.ManaCost); 
            for (int i = 0; i < 2; i++)
            {
                (int damage, bool isCrit) hit = player.Attack(); 
                float skillRate = (player.MaxHealth - player.Health + 150) * 0.01f; // 스킬 계수 계산
                hit.damage = Convert.ToInt32(Math.Round(hit.damage * skillRate));  // 스킬 데미지 계산
                result.Add(hit);
            }                       

            return result;
        }
    }
}
