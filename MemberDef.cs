namespace JavaPlantUmlGenerator
{
    public class MemberDef
    {
        public enum MemberKind
        {
            Field,
            Method,
            EnumConstant
        }

        public MemberKind Kind { get; set; }

        public string Decl { get; set; }

        public string Type { get; set; } = "";
    }
}