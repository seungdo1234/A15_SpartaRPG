
namespace TextRPG
{
    public class Boss : Enemy
    {        
        public Boss()
        {
            Name = "발록";
            Level = 99;
            Atk = 100;
            Def = 40;
            MaxHealth = 10000;
            Health = MaxHealth;            
            AvoidChance = 10;
            CriticalChance = 20;
            CriticalDamage = 1.6f;
            MaxMana = 100;
            Mana = MaxMana;
            Phase = 1;
            base.Skills = new List<Skill>();
            Skills.Add(new DemonClaw(22));
            Skills.Add(new BloodDrain(23));
            Skills.Add(new CallOfDeath(24));            
        }
        public override string OnDamaged(int damage) // 최소 데미지 1
        {
            int endDamage = (damage - Def) > 0 ? (int)(damage - Def) : 1;
            Health -= endDamage;
            CheckPhaseChange();

            return $"[데미지 {endDamage}] ";
        }
        public override string OnDamagedDenyDef(int damage) // 최소 데미지 1
        {
            Health -= damage;
            CheckPhaseChange();

            return $"[데미지 {damage}] ";
        }
        
        private void CheckPhaseChange()
        {
            if(Health <= 7000 && Phase < 2)
            {
                Phase = 2;                
            }
            if(Health <= 3000 && Phase < 3)
            {
                Phase = 3;                
            }
        }
    }
}
