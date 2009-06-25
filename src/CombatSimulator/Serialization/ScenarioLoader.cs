using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using CombatSimulator.Model;
using CombatSimulator.Model.Terrains;

namespace CombatSimulator.Serialization
{
    public class ScenarioLoader
    {
        private readonly Dictionary<string, Func<Unit>> _unitFactory =
            new Dictionary<string, Func<Unit>>
                {
                    {"Axeman", Unit.Axeman},
                    {"Archer", Unit.Archer},
                    {"Catapult", Unit.Catapult},
                    {"Chariot", Unit.Chariot},
                    {"HorseArcher", Unit.HorseArcher},
                    {"Swordsman", Unit.Swordsman},
                    {"Spearman", Unit.Spearman},
                    {"Knight", Unit.Knight},
                    {"WarElephant", Unit.WarElephant},
                    {"Longbowman", Unit.Longbowman},
                    {"Maceman", Unit.Maceman},
                    {"Pikeman", Unit.Pikeman},
                    {"Crossbowman", Unit.Crossbowman},

                    {"Cataphract", Unit.Cataphract},
                    {"Cannon", Unit.Cannon},
                    {"Musketman", Unit.Musketman},
                };

        private readonly Dictionary<string, Func<Unit, int, Unit>> _unitDecorator =
            new Dictionary<string, Func<Unit, int, Unit>>
                {
                    {"Bonus", UnitExtensions.Bonus},
                    {"Barrage", UnitExtensions.Barrage},
                    {"HitPoints", UnitExtensions.HitPoints},
                    {"Shock", UnitExtensions.Shock},
                    {"Cover", UnitExtensions.Cover},
                    {"Garrison", UnitExtensions.Garrison},
                    {"Fortify", UnitExtensions.Fortify},
                    {"Raider", UnitExtensions.Raider},
                    {"FirstStrike", UnitExtensions.FirstStrikes},
                    {"HillsDefense", UnitExtensions.HillsDefense},
                };

        private readonly Dictionary<string, Action<Stack, string>> _stackDecorator =
            new Dictionary<string, Action<Stack, string>>
                {
                    {"City", (s,x)=> s.Terrain(u=>new City(u,Int16.Parse(x)))},
                    {"Hills", (s,x)=> s.Terrain(u=>new Hills(u))},
                    {"Forest", (s,x)=> s.Terrain(u=>new Forest(u))},
                };


        public void Load(XmlReader reader, Scenario scenario)
        {
            reader.ReadStartElement("Scenario");
            LoadScenario(reader, scenario);
            reader.ReadEndElement();
        }

        private void LoadScenario(XmlReader reader, Scenario scenario)
        {
            while (!reader.EOF && reader.NodeType != XmlNodeType.EndElement)
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Stack")
                {
                    var stack = new Stack();
                    scenario.Stacks[reader.GetAttribute("name")] = stack;

                    var stackAttributes = new Dictionary<string, string>();
                    if (reader.MoveToFirstAttribute())
                    {
                        for (; ; )
                        {
                            if (reader.Name != "name")
                                stackAttributes.Add(reader.Name, reader.Value);
                            if (!reader.MoveToNextAttribute())
                                break;
                        }
                        reader.MoveToElement();
                    }

                    reader.ReadStartElement("Stack");
                    LoadStack(reader, stack);
                    reader.ReadEndElement();

                    foreach (var attr in stackAttributes)
                        _stackDecorator[attr.Key](stack, attr.Value);
                }
                else
                {
                    reader.Read();
                }
            }
        }

        private void LoadStack(XmlReader reader, Stack stack)
        {
            if (reader.IsEmptyElement)
            {
                reader.Read();
                return;
            }

            while (!reader.EOF && reader.NodeType != XmlNodeType.EndElement)
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        var unit = _unitFactory[reader.Name]();
                        if (reader.MoveToFirstAttribute())
                        {
                            for (; ; )
                            {
                                int value;
                                if (Int32.TryParse(reader.Value, out value))
                                {
                                    unit = _unitDecorator[reader.Name](unit, value);
                                }
                                if (!reader.MoveToNextAttribute())
                                    break;
                            }
                            reader.MoveToElement();
                        }

                        stack.Add(unit);
                        break;
                }

                reader.Read();
            }
        }
    }
}
