using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CombatSimulator.Model;
using CombatSimulator.Model.TerrainBonuses;

namespace CombatSimulator
{
    public class Combat
    {
        private static Random _random = new Random();

        public Unit Attack { get; private set; }
        public Unit Defend { get; private set; }
        public int AttackStrength { get; private set; }
        public int DefendStrength { get; private set; }
        public int AttackFirstStrikes { get; private set; }
        public int DefendFirstStrikes { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} {1:#0.#} vs {2:#0.#} {3} {4:#0.00}:{5:#0.00}",
                                 Attack.Name, Attack.Strength * Attack.Health / 100.0,
                                 Defend.Name, Defend.Strength * Defend.Health / 100.0,
                                 AttackStrength / 100.0, DefendStrength / 100.0);
        }

        public Combat(Unit attack, Unit defend)
        {
            Attack = attack;
            Defend = defend;

            var attackBonus = attack.StrengthBonus();
            var defendBonusBase = defend.StrengthBonus();

            var attackExtra = attack.BonusAgainst(defend, true);
            var defendExtra = defend.BonusAgainst(attack, false);

            if (!defend.Is<IgnoreFirstStrike>())
                AttackFirstStrikes = attack.FirstStrikes();
            if (!attack.Is<IgnoreFirstStrike>())
                DefendFirstStrikes = defend.FirstStrikes();

            var defendBonus = defendBonusBase + defendExtra - attackExtra;

            AttackStrength = attack.Strength * (100 + attackBonus);
            if (defendBonus >= 0)
                DefendStrength = defend.Strength * (100 + defendBonus);
            else
                DefendStrength = defend.Strength * 10000 / (100 - defendBonus);
        }

        public void Engage(bool verbose)
        {
            if (verbose)
            {
                Console.Write(Attack.Name + " " + (Attack.Health * AttackStrength / 10000.0) + ">" + (Defend.Health * DefendStrength / 10000.0) + " " +
                              Defend.Name);
            }
            int round = 0;
            while (Attack.Health != 0 && Defend.Health != 0)
            {
                ++round;
                Strike(verbose, round);
            }

            if (verbose)
            {
                Console.Write(Defend.Health == 0 ? ">" : "<");
                Console.WriteLine(Defend.Health + Attack.Health);
            }
        }

        public void Strike(bool verbose, int round)
        {
            var roll = _random.Next(AttackStrength + DefendStrength);
            if (roll < AttackStrength)
            {
                if (round <= DefendFirstStrikes)
                {
                    if (verbose)
                        Console.Write("~");
                }
                else
                {
                    if (verbose)
                        Console.Write("+");
                    Defend.Damaged(Damage(AttackStrength, DefendStrength));
                }
            }
            else
            {
                if (round <= AttackFirstStrikes)
                {
                    if (verbose)
                        Console.Write("~");
                }
                else
                {
                    if (verbose)
                        Console.Write("-");
                    Attack.Damaged(Damage(DefendStrength, AttackStrength));
                }
            }
        }

        public int Damage(int hitterStrength, int targetStrength)
        {
            return 20 * (3 * hitterStrength + targetStrength) / (3 * targetStrength + hitterStrength);
        }

        public static bool IsFirstBetterForAttack(Combat first, Combat second)
        {
            //better 
            // is (a1/d1)*ah > (a2/d2)*dh
            // is (a1*d2) > (a2*d1)
            return
                1.0 * first.AttackStrength * second.DefendStrength * first.Attack.Health * second.Defend.Health >
                1.0 * second.AttackStrength * first.DefendStrength * second.Attack.Health * first.Defend.Health;
        }
    }
}
