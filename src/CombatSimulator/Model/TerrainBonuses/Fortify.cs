namespace CombatSimulator.Model
{
    public class Fortify : UnitDecorator
    {
        private readonly int _bonus;

        public Fortify(Unit decorated, int bonus)
            : base(decorated)
        {
            _bonus = bonus;
        }

        public override int BonusAgainst(Unit opponent, bool attacking)
        {
            return (attacking == false ? _bonus : 0) + base.BonusAgainst(opponent, attacking);
        }
    }
}