
namespace TextRPG
{
    public class WeaknessSniping : Skill
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
            result = target.OnDamaged(damage);
            result += critStr;

            return result;
        }
    }
}
