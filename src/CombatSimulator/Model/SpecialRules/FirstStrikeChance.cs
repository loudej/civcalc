namespace CombatSimulator.Model.SpecialRules
{
    public class FirstStrikeChance : UnitDecorator
    {
        public FirstStrikeChance(Unit decorated)
            : base(decorated)
        {
        }
        
        static System.Random _random = new System.Random();

        public override int FirstStrikes()
        {
            return _random.Next(2) + base.FirstStrikes();
        }
    }
}