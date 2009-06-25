using CombatSimulator.Model.Terrains;

namespace CombatSimulator.Model.TerrainBonuses
{
    public class VsCity : UnitDecorator
    {
        private readonly int _bonus;

        public VsCity(Unit decorated, int bonus)
            : base(decorated)
        {
            _bonus = bonus;
        }

        public override int BonusAgainst(Unit opponent, bool attacking)
        {
            return (attacking == true && opponent.Is<City>() ? _bonus : 0) + base.BonusAgainst(opponent, attacking);
        }
    }

    public class IgnoreFirstStrike : UnitDecorator
    {
        public IgnoreFirstStrike(Unit decorated)
            : base(decorated)
        {
        }
    }

    public class HillsDefense : UnitDecorator
    {
        private readonly int _bonus;

        public HillsDefense(Unit decorated, int bonus)
            : base(decorated)
        {
            _bonus = bonus;
        }

        public override int BonusAgainst(Unit opponent, bool attacking)
        {
            return (attacking == false && Self.Is<Hills>() ? _bonus : 0) + base.BonusAgainst(opponent, attacking);
        }
    }
}