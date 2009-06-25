using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatSimulator.Model
{
    public class Terrain : UnitDecorator
    {
        private readonly int _bonus;

        public Terrain(Unit decorated, int bonus)
            : base(decorated)
        {
            _bonus = bonus;
        }

        public override int BonusAgainst(Unit opponent, bool attacking)
        {
            return (attacking == false && Self.Is<NoDefenseBonus>() == false ? _bonus : 0) + base.BonusAgainst(opponent, attacking);
        }
    }

    public class NoDefenseBonus : UnitDecorator
    {
        public NoDefenseBonus(Unit decorated)
            : base(decorated)
        {
        }
    }

    public class City : Terrain
    {
        public City(Unit decorated, int bonus)
            : base(decorated, bonus)
        {
        }
    }

    public class Hills : Terrain
    {
        public Hills(Unit decorated)
            : base(decorated, 25)
        {
        }
    }

    public class Forest : Terrain
    {
        public Forest(Unit decorated)
            : base(decorated, 25)
        {
        }
    }
}
