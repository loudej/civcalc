using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using CombatSimulator.Model;
using CombatSimulator.Serialization;

namespace CombatSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var scenario = new Scenario();
            using (var reader = XmlReader.Create(args[0]))
            {
                scenario.Load(reader);
            }


            //scenario.Attackers.Add(
            //    Unit.Axeman().Bonus(10),
            //    Unit.Axeman().Bonus(10),
            //    Unit.Axeman(),
            //    Unit.Axeman(),
            //    Unit.HorseArcher().Bonus(10).Shock(25).HitPoints(92),
            //    Unit.Chariot().Bonus(10).Shock(25),
            //    Unit.Archer(),
            //    Unit.Archer());

            //scenario.Defenders.Add(
            //    Unit.Swordsman().Bonus(20).Shock(25),
            //    Unit.Swordsman().Bonus(20).Shock(25),
            //    Unit.Axeman().Bonus(20).Shock(25));

            //scenario.Attackers.Add(
            //    Unit.Axeman(),
            //    Unit.Axeman().Bonus(10),
            //    Unit.Axeman().Bonus(10).Shock(25).HitPoints(20),
            //    Unit.Archer(),
            //    Unit.Archer());

            //scenario.Defenders.Add(
            //    Unit.Swordsman().Bonus(20).Shock(25));

            //scenario.Defenders.Add(
            //    Unit.Swordsman(),
            //    Unit.HorseArcher().Bonus(10).CurrentStrength(1.2),
            //    Unit.Swordsman().Bonus(10).CurrentStrength(3.8),
            //    Unit.Swordsman().Bonus(10).CurrentStrength(3.8),
            //    Unit.Swordsman().CurrentStrength(4.9),
            //    Unit.Swordsman(),
            //    Unit.Catapult().CurrentStrength(1.1).Barrage(20),
            //    Unit.Catapult().Barrage(20),
            //    Unit.Catapult(),
            //    Unit.Axeman().Bonus(10).Shock(25).CurrentStrength(0.3),
            //    Unit.Axeman().Bonus(10).Shock(25).CurrentStrength(3.2),
            //    Unit.Axeman().Bonus(10),
            //    Unit.Axeman().CurrentStrength(3.7),
            //    Unit.Spearman().Bonus(10),
            //    Unit.Axeman().Bonus(10),
            //    Unit.Axeman().Bonus(10),
            //    Unit.Axeman().Bonus(10)
            //    );
            //scenario.Defenders.Terrain(u => new City(u, 0));
            //scenario.Defenders.Terrain(u => new Hills(u));

            //scenario.Attackers.Add(
            //    Unit.Swordsman().Bonus(30),
            //    Unit.Swordsman().Bonus(30),
            //    Unit.Swordsman().Bonus(30),
            //    Unit.Swordsman().Bonus(20).Shock(25),
            //    Unit.Swordsman().Bonus(20).Shock(25),
            //    Unit.Swordsman().Bonus(20).Shock(25),
            //    Unit.Swordsman().Bonus(20),
            //    Unit.Axeman().Bonus(20).Shock(25),
            //    Unit.Axeman().Bonus(10).Shock(25),
            //    Unit.Axeman().Bonus(10),
            //    Unit.Archer(),
            //    Unit.Archer(),
            //    Unit.Archer());

            //scenario.Attackers.Terrain(u => new Forest(u));

            //scenario.Defenders.Terrain(50);

            //    Unit.Swordsman().Bonus(20).Shock(25),
            //    Unit.Axeman().Bonus(20).Shock(25));

            for (var loop = 0; loop != 20; ++loop)
            {
                scenario.Reset();
                var battle = new Battle(scenario.Attackers, scenario.Defenders);
                battle.Engage(true);

                WriteUnits(scenario);

                if (scenario.Attackers.Any() && scenario.Defenders.Any())
                {
                    battle = new Battle(scenario.Defenders, scenario.Attackers);
                    battle.Engage(true);
                    WriteUnits(scenario);
                }
                Console.WriteLine("==========");
            }

            scenario.Reset();
            var topCount = Math.Max(scenario.Attackers.Count(), scenario.Defenders.Count()) + 1;

            var attackers1 = new int[topCount];
            var defenders1 = new int[topCount];
            var attackers2 = new int[topCount];
            var defenders2 = new int[topCount];
            var ratio = new int[5];
            for (var loop = 0; loop != 1000; ++loop)
            {
                scenario.Reset();
                var battle = new Battle(scenario.Attackers, scenario.Defenders);
                battle.Engage(false);

                ++attackers1[scenario.Attackers.Count()];
                ++defenders1[scenario.Defenders.Count()];

                if (!scenario.Defenders.Any())
                {
                    ++ratio[0];
                }
                else if (!scenario.Attackers.Any())
                {
                    ++ratio[4];
                }
                else
                {
                    battle = new Battle(scenario.Defenders, scenario.Attackers);
                    battle.Engage(false);
                    if (!scenario.Defenders.Any())
                    {
                        ++ratio[1];
                    }
                    else if (!scenario.Attackers.Any())
                    {
                        ++ratio[3];
                    }
                    else
                    {
                        ++ratio[2];
                    }
                }

                ++attackers2[scenario.Attackers.Count()];
                ++defenders2[scenario.Defenders.Count()];
            }

            Console.WriteLine("win/adv/push/dis/loss:{0}/{1}/{2}/{3}/{4}",
                ratio[0], ratio[1], ratio[2], ratio[3], ratio[4]);
            for (var index = 0; index != topCount; ++index)
            {
                Console.WriteLine("{0} {1} {2} {3} {4}",
                    attackers2[index].ToString().PadLeft(8),
                    attackers1[index].ToString().PadLeft(8),
                    index.ToString().PadLeft(2),
                    defenders1[index].ToString().PadRight(8),
                    defenders2[index].ToString().PadRight(8));
            }
            
            for (var index = 0; index != topCount; ++index)
            {
                Console.WriteLine("{0} {1} {2} {3} {4}",
                    attackers2.Take(index + 1).Sum().ToString().PadLeft(8),
                    attackers1.Take(index + 1).Sum().ToString().PadLeft(8),
                    index.ToString().PadLeft(2),
                    defenders1.Take(index + 1).Sum().ToString().PadRight(8),
                    defenders2.Take(index + 1).Sum().ToString().PadRight(8));
            }

            Console.WriteLine("Complete");
            Console.ReadLine();
        }

        private static void WriteUnits(Scenario scenario)
        {
            Console.WriteLine("----------");
            var sb = new StringBuilder();
            sb.Append("atk: ");
            foreach (var u in scenario.Attackers.Units.Where(x => x.Health != 0))
            {
                // ReSharper disable PossibleLossOfFraction
                sb.Append(u.Name).Append(" ").Append((u.Strength * u.Health / 10) / 10.0).Append(" ");
                // ReSharper restore PossibleLossOfFraction
            }
            sb.AppendLine();
            sb.Append("def: ");
            foreach (var u in scenario.Defenders.Units.Where(x => x.Health != 0))
            {
                // ReSharper disable PossibleLossOfFraction
                sb.Append(u.Name).Append(" ").Append((u.Strength * u.Health / 10) / 10.0).Append(" ");
                // ReSharper restore PossibleLossOfFraction
            }

            Console.WriteLine(sb);
        }
    }

}
