
namespace TextRPG
{
    public class Enemy : Unit
    {
        public Enemy(string Name, int Level, float Atk, float Def,
            int MaxHealth, int AvoidChance, int CriticalChance, float CriticalDamage)
        {
            this.Name = Name;
            this.Level = Level;
            this.Atk = Atk;
            this.Def = Def;
            this.MaxHealth = MaxHealth; 
            Health = MaxHealth;
            this.AvoidChance = AvoidChance;
            this.CriticalChance = CriticalChance;
            this.CriticalDamage = CriticalDamage;
        }


        public override string ToString()
        {
            if (Level > 9)
            {
                return $"Lv.{Level} {Name}\tHP {Health}/{MaxHealth}";
            }
            else
            {
                return $"Lv.{Level}  {Name}\tHP {Health}/{MaxHealth}";
            }
        }
    }
}
