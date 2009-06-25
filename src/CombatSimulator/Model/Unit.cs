using System;
using CombatSimulator.Model.Categories;
using CombatSimulator.Model.CategoryBonuses;
using CombatSimulator.Model.SpecialRules;
using CombatSimulator.Model.TerrainBonuses;

namespace CombatSimulator.Model
{
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
            return new VsMelee(new MeleeUnit(new UnitBase("Axeman", 5)), 50);
        }

        public static Unit Spearman()
        {
            return new VsMounted(new MeleeUnit(new UnitBase("Spearman", 4)), 100);
        }

        public static Unit Archer()
        {
            return new FirstStrike(new CityDefense(new HillsDefense(new ArcherUnit(new UnitBase("Archer", 3)), 25), 50));
        }

        public static Unit Swordsman()
        {
            return new VsCity(new MeleeUnit(new UnitBase("Swordsman", 6)), 10);
        }

        public static Unit Catapult()
        {
            return new CollateralDamage(new UnitBase("Catapult", 5), 0, 5)
                .NoDefenseBonus();
        }

        public static Unit Chariot()
        {
            return new VsAxeman(new MountedUnit(new UnitBase("Chariot", 4)), 100)
                .NoDefenseBonus();
        }

        public static Unit HorseArcher()
        {
            return new MountedUnit(new UnitBase("HorseArcher", 6))
                .NoDefenseBonus();
        }

        public static Unit Crossbowman()
        {
            return new FirstStrike(new VsMelee(new ArcherUnit(new UnitBase("Crossbowman", 6)), 50));
        }

        public static Unit Knight()
        {
            return new IgnoreFirstStrike(new MountedUnit(new UnitBase("Knight", 10)))
                .NoDefenseBonus();
        }
        public static Unit Cataphract()
        {
            return new IgnoreFirstStrike(new MountedUnit(new UnitBase("Cataphract", 12)))
                .NoDefenseBonus();
        }

        public static Unit Cannon()
        {
            return new CollateralDamage(new UnitBase("Cannon", 12), 0, 6)
                .NoDefenseBonus();
        }

        public static Unit Musketman()
        {
            return new GunpowderUnit(new UnitBase("Musketman", 9))
                .NoDefenseBonus();
        }

        public static Unit WarElephant()
        {
            return new VsMounted(new MountedUnit(new UnitBase("WarElephant", 8)), 50)
                .NoDefenseBonus();
        }

        public static Unit Longbowman()
        {
            return new FirstStrike(new CityDefense(new HillsDefense(new ArcherUnit(new UnitBase("Longbowman", 6)), 25), 25));
        }

        public static Unit Maceman()
        {
            return new VsMelee(new MeleeUnit(new UnitBase("Maceman", 8)), 50);
        }

        public static Unit Pikeman()
        {
            return new VsMounted(new MeleeUnit(new UnitBase("Pikeman", 6)), 100);
        }
    }


}