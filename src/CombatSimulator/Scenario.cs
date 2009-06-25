using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
using CombatSimulator.Serialization;

namespace CombatSimulator
{
    public class Scenario
    {
        public Scenario()
        {
            Stacks = new Dictionary<string, Stack>();
            Attackers = new Stack();
            Defenders = new Stack();
        }

        public Dictionary<string, Stack> Stacks { get; set; }

        public Stack Attackers
        {
            get { return Stacks["Attackers"]; }
            set { Stacks["Attackers"] = value; }
        }

        public Stack Defenders
        {
            get { return Stacks["Defenders"]; }
            set { Stacks["Defenders"] = value; }
        }

        public void Reset()
        {
            foreach (var army in Stacks.Values)
                army.Reset();
        }

        public void Load(XmlReader reader)
        {
            var loader = new ScenarioLoader();
            loader.Load(reader, this);
        }
    }
}