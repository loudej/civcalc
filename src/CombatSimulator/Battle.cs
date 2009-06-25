using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CombatSimulator.Model;

namespace CombatSimulator
{
    public class Battle
    {
        public Stack Attackers { get; set; }
        public Stack Defenders { get; set; }

        public Battle(Stack attackers, Stack defenders)
        {
            Attackers = attackers;
            Defenders = defenders;
        }

        public void Engage(bool verbose)
        {
            foreach (var unit in Attackers.Units)
                unit.Used = false;

            while (Attackers.Units.Any(CanAttack) && Defenders.Units.Any(CanDefend))
            {
                Strike(verbose);
            }
        }

        private static bool CanDefend(Unit unit)
        {
            return unit.Health != 0;
        }

        private static bool CanAttack(Unit unit)
        {
            return unit.Health != 0 && !unit.Used;
        }

        static Random _random = new Random();

        private void Strike(bool verbose)
        {
            Combat selectedCombatSpecial = null;
            Combat selectedCombat = null;
            foreach (var attack in Attackers.Units.Where(x => x.Health != 0 && !x.Used))
            {
                Combat selectedByDefense = null;
                foreach (var defend in Defenders.Units.Where(x => x.Health != 0))
                {
                    var potentialCombat = new Combat(attack, defend);

                    if (selectedByDefense == null || Combat.IsFirstBetterForAttack(selectedByDefense, potentialCombat))
                    {
                        selectedByDefense = potentialCombat;
                    }
                }
                if (selectedCombat == null || Combat.IsFirstBetterForAttack(selectedByDefense, selectedCombat))
                {
                    selectedCombat = selectedByDefense;
                }
                if (attack.Is<CollateralDamage>())
                {
                    if (selectedCombatSpecial == null ||
                        Combat.IsFirstBetterForAttack(selectedByDefense, selectedCombatSpecial))
                    {
                        selectedCombatSpecial = selectedByDefense;
                    }
                }
            }
            if (selectedCombatSpecial != null)
                selectedCombat = selectedCombatSpecial;

            selectedCombat.Engage(verbose);
            selectedCombat.Attack.Used = true;

            if (selectedCombat.Attack.Is<CollateralDamage>())
            {
                var collateralDamageStrength = selectedCombat.Attack.CollateralDamageStrength(0);

                var targets = Defenders.Units.Where(x => x.Health > 25).Where(x => x != selectedCombat.Defend).ToArray();
                var order = new int[targets.Length];
                for(var index = 0; index != order.Length; ++index)
                    order[index] = _random.Next(0, 9000);
                Array.Sort(order, targets);

                var hits = selectedCombat.Attack.As<CollateralDamage>().Hits;

                if (verbose)
                    Console.WriteLine("{0} units take collateral damage", targets.Take(hits).Count());

                foreach (var collateral in targets.Take(hits))
                {
                    collateral.CollateralDamage(collateralDamageStrength, 25, verbose);
                    //var combat = new Combat(selectedCombat.Attack, collateral);
                    //collateral.Damaged(combat.Damage(combat.AttackStrength, combat.DefendStrength));
                }
            }
        }
    }
}
