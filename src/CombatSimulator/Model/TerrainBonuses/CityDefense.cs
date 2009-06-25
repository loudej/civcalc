using CombatSimulator.Model.Terrains;

namespace CombatSimulator.Model.TerrainBonuses
{
    public class CityDefense : UnitDecorator
    {
        private readonly int _bonus;

        public CityDefense(Unit decorated, int bonus)
            : base(decorated)
        {
            _bonus = bonus;
        }

        public override int BonusAgainst(Unit opponent, bool attacking)
        {
            return (attacking == false && Self.Is<City>() ? _bonus : 0) + base.BonusAgainst(opponent, attacking);
        }
    }
}