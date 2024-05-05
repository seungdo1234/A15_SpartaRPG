
namespace TextRPG
{
    public class Blind : DeBuff
    {
        public Blind(string name, int duration, string caster) : base(name, duration, caster)
        {
        }

        public override void ActiveDebuff(Unit target)
        {
            Duration--;
            Console.WriteLine($"{Caster}의 {Name}에 의한 실명 [남은 턴: {Duration}]");
            Thread.Sleep(1500);
            if (Duration == 0)
            {
                target.DebuffActiveHandler -= ActiveDebuff;
                target.DeBuffs.Remove(this);
                target.DispelCrowdControl(ECrowdControlType.BLIND);
            }
        }
    }
}
