using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class EnemySkill //일반몹: 필살기. //boss: 스킬.
    {
        private string Name { get; }
        private int Damage { get; }
        private string Content { get; } //플레이어와 달리 정확한 정보x, 재미요소o.

        public EnemySkill(string name, int baseDamage, string content)
        {
            Name = name;
            Damage = baseDamage;
            Content = content;
        }

        public void Activate(Unit target)
        {

        }
    }
}
