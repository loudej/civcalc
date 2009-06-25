namespace CombatSimulator.Model.SpecialRules
{
    public class FirstStrike : UnitDecorator
    {
        public FirstStrike(Unit decorated)
            : base(decorated)
        {
        }

        public override int FirstStrikes()
        {
            return 1 + base.FirstStrikes();
        }
    }
}