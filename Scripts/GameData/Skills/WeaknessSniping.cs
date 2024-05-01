
namespace TextRPG
{
    public class WeaknessSniping : SkillData
    {
        public WeaknessSniping(int id) : base(id)
        {
        }

        public override string CastSkill(Unit caster, Unit target)
        {
            string result;
            string? critStr = "Critical!!";
            float critRate = caster.CriticalDamage + 0.5f; // 치명타배율 +50%
            float skillRate = 2;
            
            int damage = caster.GetDamagePerHit();

            damage = Convert.ToInt32(Math.Round(damage * skillRate * critRate));
            target.OnDamaged(damage);
            result = $"[데미지 {damage}] " + critRate;

            return result;
        }
    }
}
