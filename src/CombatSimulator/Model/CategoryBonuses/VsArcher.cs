using CombatSimulator.Model.Categories;

namespace CombatSimulator.Model.CategoryBonuses
{
    public class VsArcher : UnitDecorator
    {
        private readonly int _bonus;

        public VsArcher(Unit decorated, int bonus)
            : base(decorated)
        {
            _bonus = bonus;
        }

        public override int BonusAgainst(Unit opponent, bool attacking)
        {
            return (opponent.Is<ArcherUnit>() ? _bonus : 0) + base.BonusAgainst(opponent, attacking);
        }
    }
}