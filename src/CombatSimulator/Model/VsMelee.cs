namespace CombatSimulator.Model
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

    public class VsArcher : UnitDecorator
    {
        private readonly int _bonus;

        public VsArcher(Unit decorated, int bonus)
            : base(decorated)
        {
            _bonus = bonus;
        }

        public override int BonusAgainst(Unit opponent, bool attacking)
        {
            return (opponent.Is<ArcherUnit>() ? _bonus : 0) + base.BonusAgainst(opponent, attacking);
        }
    }
}
