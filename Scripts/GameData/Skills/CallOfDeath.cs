
namespace TextRPG
{
    public class CallOfDeath : Skill
    {
        public CallOfDeath(int id) : base(id)
        {
        }

        public override string CastSkill(Unit caster, Unit target)
        {
            string result;

            int damage = target.Health - 1;
            
            result = target.OnDamagedDenyDef(damage);
            
            return result;
        }
    }
}
