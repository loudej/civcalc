using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using CombatSimulator.Model;

namespace CombatSimulator
{
    [DataContract(Namespace = "")]
    public class Stack
    {
        public Stack()
        {
            Units = new List<Unit>();
        }

        public void Reset()
        {
            foreach (var unit in Units)
                unit.Reset(0);
        }

        public int Count()
        {
            return Units.Count(x => x.Health != 0);
        }
        public bool Any()
        {
            return Units.Any(x => x.Health != 0);
        }

        [DataMember(Order = 0)]
        public IList<Unit> Units { get; set; }

        public Stack Add(params Unit[] units)
        {
            foreach (var unit in units)
                Units.Add(unit);
            return this;
        }

        public void Terrain(Func<Unit, Unit> func)
        {
            for (var index = 0; index != Units.Count; ++index)
            {
                Units[index] = func(Units[index]);
            }
        }

    }
}
