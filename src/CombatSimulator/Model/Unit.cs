using System;

namespace CombatSimulator.Model
{
    public class BaseUnit : Unit
    {
        private readonly string _name;
        private readonly int _strength;

        public BaseUnit(string name, int strength)
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

    public abstract class Unit
    {
        public abstract Unit Self { get; set; }

        public abstract string Name { get; }
        public abstract int Strength { get; }

        public abstract int StrengthBonus();
        public abstract int BonusAgainst(Unit opponent, bool attacking);

        public abstract bool Used { get; set; }
        public abstract int Health { get; set; }

        public abstract int CollateralDamageStrength(int bonus);
        public abstract void CollateralDamage(int attackStrength, int minimumHealth, bool verbose);

        public abstract int FirstStrikes();

        public virtual void Damaged(int damage)
        {
            Health = Math.Max(Health - damage, 0);
        }

        public virtual void Reset(int health)
        {
            Used = false;
            Health = health == 0 ? 100 : health;
        }

        public virtual bool Is<T>()
        {
            return this is T;
        }

        public virtual T As<T>() where T : class
        {
            return this as T;
        }

        public static Unit Axeman()
        {
            return new VsMelee(new MeleeUnit(new BaseUnit("Axeman", 5)), 50);
        }

        public static Unit Spearman()
        {
            return new VsMounted(new MeleeUnit(new BaseUnit("Spearman", 4)), 100);
        }

        public static Unit Archer()
        {
            return new FirstStrike(new CityDefense(new HillsDefense(new ArcherUnit(new BaseUnit("Archer", 3)), 25), 50));
        }

        public static Unit Swordsman()
        {
            return new VsCity(new MeleeUnit(new BaseUnit("Swordsman", 6)), 10);
        }

        public static Unit Catapult()
        {
            return new CollateralDamage(new BaseUnit("Catapult", 5), 0, 5)
                .NoDefenseBonus();
        }

        public static Unit Chariot()
        {
            return new VsAxeman(new MountedUnit(new BaseUnit("Chariot", 4)), 100)
                .NoDefenseBonus();
        }

        public static Unit HorseArcher()
        {
            return new MountedUnit(new BaseUnit("HorseArcher", 6))
                .NoDefenseBonus();
        }

        public static Unit Crossbowman()
        {
            return new FirstStrike(new VsMelee(new ArcherUnit(new BaseUnit("Crossbowman", 6)), 50));
        }

        public static Unit Knight()
        {
            return new IgnoreFirstStrike(new MountedUnit(new BaseUnit("Knight", 10)))
                .NoDefenseBonus();
        }
        public static Unit Cataphract()
        {
            return new IgnoreFirstStrike(new MountedUnit(new BaseUnit("Cataphract", 12)))
                .NoDefenseBonus();
        }

        public static Unit Cannon()
        {
            return new CollateralDamage(new BaseUnit("Cannon", 12), 0, 6)
                .NoDefenseBonus();
        }

        public static Unit Musketman()
        {
            return new GunpowderUnit(new BaseUnit("Musketman", 9))
                .NoDefenseBonus();
        }

        public static Unit WarElephant()
        {
            return new VsMounted(new MountedUnit(new BaseUnit("WarElephant", 8)), 50)
                .NoDefenseBonus();
        }

        public static Unit Longbowman()
        {
            return new FirstStrike(new CityDefense(new HillsDefense(new ArcherUnit(new BaseUnit("Longbowman", 6)), 25), 25));
        }

        public static Unit Maceman()
        {
            return new VsMelee(new MeleeUnit(new BaseUnit("Maceman", 8)), 50);
        }

        public static Unit Pikeman()
        {
            return new VsMounted(new MeleeUnit(new BaseUnit("Pikeman", 6)), 100);
        }
    }


}