
namespace TextRPG
{
    public class Assassination : Skill
    {
        public Assassination(int id) : base(id)
        {
        }

        public override string CastSkill(Unit caster, Unit target)
        {
            string result;
            string? critStr = caster.IsCriticalHit();
            float critRate = critStr != null ? caster.CriticalDamage : 1f;
            float skillRate = ((float)target.Health / target.MaxHealth) > 0.2f ? 1.2f : 0;
            int damage = caster.GetDamagePerHit();

            if (skillRate == 0)
            {
                damage = target.Health;
                result = target.OnDamagedDenyDef(damage);
                result += critStr;

                return result;
            }

            damage = Convert.ToInt32(Math.Round(damage * skillRate * critRate));
            result = target.OnDamaged(damage);
            result += critStr;

            return result;
        }
    }
}

