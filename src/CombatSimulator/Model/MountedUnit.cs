namespace CombatSimulator.Model
{
    public class MountedUnit : UnitDecorator
    {
        public MountedUnit(Unit unit)
            : base(unit)
        {
        }
    }

    public class GunpowderUnit : UnitDecorator
    {
        public GunpowderUnit(Unit unit)
            : base(unit)
        {
        }
    }
}