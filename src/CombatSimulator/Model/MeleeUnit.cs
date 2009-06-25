namespace CombatSimulator.Model
{
    public class ArcherUnit : UnitDecorator
    {
        public ArcherUnit(Unit unit)
            : base(unit)
        {
        }
    }

    public class MeleeUnit : UnitDecorator
    {
        public MeleeUnit(Unit unit)
            : base(unit)
        {
        }
    }
}