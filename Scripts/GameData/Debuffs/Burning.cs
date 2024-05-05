
namespace TextRPG
{
    public class Burning : DeBuff
    {
        public Burning(string name, int duration, string caster) : base(name, duration, caster)
        {
        }

        public override void ActiveDebuff(Unit target)
        {
            Duration--;
            Console.WriteLine($"{Caster}의 {Name}에 의한 화상 {target.OnDamagedDenyDef(4)} [남은 턴: {Duration}]");
            Thread.Sleep(1500);
            if (Duration == 0)
            {
                target.DebuffActiveHandler -= ActiveDebuff;
                target.DeBuffs.Remove(this);
            }
        }
    }
}
