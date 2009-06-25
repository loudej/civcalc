namespace CombatSimulator.Model.CategoryBonuses
{
    public class VsAxeman : UnitDecorator
    {
        private readonly int _bonus;

        public VsAxeman(Unit decorated, int bonus)
            : base(decorated)
        {
            _bonus = bonus;
        }

        public override int BonusAgainst(Unit opponent, bool attacking)
        {
            return (opponent.Name == "Axeman" ? _bonus : 0) + base.BonusAgainst(opponent, attacking);
        }
    }
}