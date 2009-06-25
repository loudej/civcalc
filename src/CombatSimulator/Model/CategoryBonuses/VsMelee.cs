using CombatSimulator.Model.Categories;

namespace CombatSimulator.Model.CategoryBonuses
{
    public class VsMelee : UnitDecorator
    {
        private readonly int _bonus;

        public VsMelee(Unit decorated, int bonus)
            : base(decorated)
        {
            _bonus = bonus;
        }

        public override int BonusAgainst(Unit opponent, bool attacking)
        {
            return (opponent.Is<MeleeUnit>() ? _bonus : 0) + base.BonusAgainst(opponent, attacking);
        }
    }
}