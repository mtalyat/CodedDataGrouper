using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedDataGrouper
{
    [Serializable]
    internal class Category
    {
        public static float GlobalThreshold { get; set; } = 0.0f;

        public string Name { get; set; }

        public float Threshold { get; set; }

        public Category(string name, float threshold)
        {
            Name = name;
            Threshold = threshold;
        }

        public Category(string name) : this(name, GlobalThreshold)
        {

        }

        public Category() : this("", GlobalThreshold)
        {

        }

        public override string ToString()
        {
            return $"{Name} ({Threshold}s)";
        }
    }
}
