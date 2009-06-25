using CombatSimulator.Model.Categories;

namespace CombatSimulator.Model.CategoryBonuses
{
    public class VsMounted : UnitDecorator
    {
        private readonly int _bonus;

        public VsMounted(Unit decorated, int bonus)
            : base(decorated)
        {
            _bonus = bonus;
        }

        public override int BonusAgainst(Unit opponent, bool attacking)
        {
            return (opponent.Is<MountedUnit>() ? _bonus : 0) + base.BonusAgainst(opponent, attacking);
        }
    }
}