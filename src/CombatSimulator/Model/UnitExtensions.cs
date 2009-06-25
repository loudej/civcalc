using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CombatSimulator.Model.CategoryBonuses;
using CombatSimulator.Model.SpecialRules;
using CombatSimulator.Model.TerrainBonuses;
using CombatSimulator.Model.Terrains;

namespace CombatSimulator.Model
{
    public static class UnitExtensions
    {
        public static Unit Shock(this Unit unit, int bonus)
        {
            return new VsMelee(unit, bonus);
        }

        public static Unit Cover(this Unit unit, int bonus)
        {
            return new VsArcher(unit, bonus);
        }

        public static Unit Barrage(this Unit unit, int bonus)
        {
            return new CollateralDamage(unit, bonus, 0);
        }

        public static Unit Garrison(this Unit unit, int bonus)
        {
            return new CityDefense(unit, bonus);
        }

        public static Unit Raider(this Unit unit, int bonus)
        {
            return new VsCity(unit, bonus);
        }

        public static Unit Fortify(this Unit unit, int bonus)
        {
            return new Fortify(unit, bonus);
        }

        public static Unit Bonus(this Unit unit, int bonus)
        {
            return new Bonus(bonus, unit);
        }

        public static Unit HillsDefense(this Unit unit, int bonus)
        {
            return new HillsDefense(unit, bonus);
        }

        public static Unit FirstStrikes(this Unit unit, int factor)
        {
            while (factor > 1)
            {
                unit = unit.FirstStrike();
                factor = factor - 2;
            }
            if (factor != 0)
            {
                unit = unit.FirstStrikeChance();
            }
            return unit;
        }

        public static Unit FirstStrike(this Unit unit)
        {
            return new FirstStrike(unit);
        }

        public static Unit FirstStrikeChance(this Unit unit)
        {
            return new FirstStrikeChance(unit);
        }

        public static Unit CurrentStrength(this Unit unit, double strength)
        {
            return new OriginalHealth(unit, (int)(100 * strength / unit.Strength));
        }

        public static Unit HitPoints(this Unit unit, int hitPoints)
        {
            return new OriginalHealth(unit, hitPoints);
        }

        public static Unit NoDefenseBonus(this Unit unit)
        {
            return new NoDefenseBonus(unit);
        }
    }

    public class OriginalHealth : UnitDecorator
    {
        private readonly int _originalHealth;

        public OriginalHealth(Unit decorated, int originalHealth)
            : base(decorated)
        {
            _originalHealth = originalHealth;
        }

        public override void Reset(int health)
        {
            base.Reset(_originalHealth);
        }
    }
}
