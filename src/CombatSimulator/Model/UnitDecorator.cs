namespace CombatSimulator.Model
{
    public class UnitDecorator : Unit
    {
        protected readonly Unit _decorated;

        public UnitDecorator(Unit decorated)
        {
            _decorated = decorated;
            Self = this;
        }

        public override string Name { get { return _decorated.Name; } }
        public override int Strength { get { return _decorated.Strength; } }
        public override int StrengthBonus() { return _decorated.StrengthBonus(); }
        public override int BonusAgainst(Unit opponent, bool attacking) { return _decorated.BonusAgainst(opponent, attacking); }

        public override int Health
        {
            get { return _decorated.Health; }
            set { _decorated.Health = value; }
        }

        public override bool Used
        {
            get { return _decorated.Used; }
            set { _decorated.Used = value; }
        }

        public override Unit Self
        {
            get { return _decorated.Self; }
            set { _decorated.Self = value; }
        }

        public override int CollateralDamageStrength(int bonus)
        {
            return _decorated.CollateralDamageStrength(bonus);
        }

        public override void CollateralDamage(int attackStrength, int minimumHealth, bool verbose)
        {
            _decorated.CollateralDamage(attackStrength, minimumHealth, verbose);
        }

        public override void Damaged(int damage) { _decorated.Damaged(damage); }
        public override void Reset(int health) { _decorated.Reset(health); }

        public override bool Is<T>()
        {
            return base.Is<T>() || _decorated.Is<T>();
        }
        public override T As<T>() 
        {
            return base.As<T>() ?? _decorated.As<T>();
        }
        public override string ToString()
        {
            return _decorated.ToString();
        }

        public override int FirstStrikes()
        {
            return _decorated.FirstStrikes();
        }
    }
}