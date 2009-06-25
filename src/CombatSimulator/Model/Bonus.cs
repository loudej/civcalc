namespace CombatSimulator.Model
{
    public class Bonus : UnitDecorator
    {
        private readonly int _bonus;

        public Bonus(int bonus, Unit decorated) : base(decorated)
        {
            _bonus = bonus;
        }

        public override int StrengthBonus()
        {
            return base.StrengthBonus() + _bonus;
        }
    }
}