using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatSimulator.Model.Terrains
{
    public class TerrainBase : UnitDecorator
    {
        private readonly int _bonus;

        public TerrainBase(Unit decorated, int bonus)
            : base(decorated)
        {
            _bonus = bonus;
        }

        public override int BonusAgainst(Unit opponent, bool attacking)
        {
            return (attacking == false && Self.Is<NoDefenseBonus>() == false ? _bonus : 0) + base.BonusAgainst(opponent, attacking);
        }
    }
}