using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatSimulator.Model.SpecialRules
{
    public class CollateralDamage : UnitDecorator
    {
        private readonly int _bonus;
        private readonly int _hits;

        public CollateralDamage(Unit decorated, int bonus, int hits)
            : base(decorated)
        {
            _bonus = bonus;
            _hits = hits;
        }

        public int Hits
        {
            get { return _hits != 0 ? _hits : _decorated.As<CollateralDamage>().Hits; }
        }

        public override int CollateralDamageStrength(int bonus)
        {
            return base.CollateralDamageStrength(bonus + _bonus);
        }
    }
}
