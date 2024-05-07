
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

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{Caster}에 의해 당신은 일반공격을 맞출 수 없다. ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[남은 턴: {Duration}]");
            Console.ResetColor();

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
