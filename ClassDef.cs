using System.Collections.Generic;

namespace JavaPlantUmlGenerator
{
    public class ClassDef
    {
        public string Name { get; set; }

        public string Superclass { get; set; }

        public bool Abstract { get; set; }

        public string Type { get; set; } = "class";

        public HashSet<string> ImplementedInterfaces { get; set; } = new();

        public HashSet<MemberDef> Members { get; set; } = new();
    }
}