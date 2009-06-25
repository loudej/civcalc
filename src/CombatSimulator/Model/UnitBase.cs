using System;

namespace CombatSimulator.Model
{
    public class UnitBase : Unit
    {
        private readonly string _name;
        private readonly int _strength;

        public UnitBase(string name, int strength)
        {
            Self = this;
            _name = name;
            _strength = strength;
        }

        public override string Name { get { return _name; } }
        public override int Strength { get { return _strength; } }
        public override int StrengthBonus() { return 0; }
        public override int BonusAgainst(Unit opponent, bool attacking) { return 0; }
        public override bool Used { get; set; }
        public override int Health { get; set; }

        public override int FirstStrikes()
        {
            return 0;
        }

        public override int CollateralDamageStrength(int bonus)
        {
            return _strength * (100 + bonus);
        }

        public override void CollateralDamage(int attackStrength, int minimumHealth, bool verbose)
        {
            var damage = 10 * (3 * attackStrength + Strength * 100) / (3 * Strength * 100 + attackStrength);
            if (verbose)
            {
                Console.Write("{0} ", damage);
            }
            if (Health > minimumHealth)
                Health = Math.Max(Health - damage, minimumHealth);
        }

        public override string ToString()
        {
            return _name;
        }

        public override Unit Self { get; set; }
    }
}