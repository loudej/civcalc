namespace CombatSimulator.Model
{
    public class VsCity : UnitDecorator
    {
        private readonly int _bonus;

        public VsCity(Unit decorated, int bonus)
            : base(decorated)
        {
            _bonus = bonus;
        }

        public override int BonusAgainst(Unit opponent, bool attacking)
        {
            return (attacking == true && opponent.Is<City>() ? _bonus : 0) + base.BonusAgainst(opponent, attacking);
        }
    }

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

    public class CityDefense : UnitDecorator
    {
        private readonly int _bonus;

        public CityDefense(Unit decorated, int bonus)
            : base(decorated)
        {
            _bonus = bonus;
        }

        public override int BonusAgainst(Unit opponent, bool attacking)
        {
            return (attacking == false && Self.Is<City>() ? _bonus : 0) + base.BonusAgainst(opponent, attacking);
        }
    }

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

    public class IgnoreFirstStrike : UnitDecorator
    {
        public IgnoreFirstStrike(Unit decorated)
            : base(decorated)
        {
        }
    }


    public class HillsDefense : UnitDecorator
    {
        private readonly int _bonus;

        public HillsDefense(Unit decorated, int bonus)
            : base(decorated)
        {
            _bonus = bonus;
        }

        public override int BonusAgainst(Unit opponent, bool attacking)
        {
            return (attacking == false && Self.Is<Hills>() ? _bonus : 0) + base.BonusAgainst(opponent, attacking);
        }
    }

    
}
