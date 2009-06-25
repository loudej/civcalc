namespace CombatSimulator.Model.Terrains
{
    public class NoDefenseBonus : UnitDecorator
    {
        public NoDefenseBonus(Unit decorated)
            : base(decorated)
        {
        }
    }
}