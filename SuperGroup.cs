using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodedDataGrouper
{
    internal class SuperGroup
    {
        public string Name { get; set; }

        public List<string> Groups { get; set; }

        public SuperGroup(string name = "Super Group")
        {
            Name = name;
            Groups = new List<string>();
        }

        public bool Contains(string name)
        {
            foreach(string n in Groups)
            {
                if(name == n)
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
