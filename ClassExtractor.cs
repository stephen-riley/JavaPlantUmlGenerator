using System.Collections.Generic;
using System.Linq;
using static JavaPlantUmlGenerator.MemberDef;

namespace JavaPlantUmlGenerator
{
    public class ClassExtractor : Java9ParserBaseVisitor<bool>
    {
        public Dictionary<string, ClassDef> ClassDefs { get; } = new();

        public ClassDef CurrentClass { get; set; }

        public override bool VisitNormalClassDeclaration(Java9Parser.NormalClassDeclarationContext context)
        {
            var cl = new ClassDef
            {
                Name = context.identifier().GetText(),
                Superclass = context.superclass()?.classType().GetText(),
                ImplementedInterfaces = context.superinterfaces()?.interfaceTypeList()?.interfaceType()?.Select(it => it.GetText())?.ToHashSet(),
                Abstract = context.classModifier()?.Any(cm => cm?.ABSTRACT() is not null) ?? false
            };

            ClassDefs[cl.Name] = CurrentClass = cl;

            return base.VisitNormalClassDeclaration(context);
        }

        public override bool VisitNormalInterfaceDeclaration(Java9Parser.NormalInterfaceDeclarationContext context)
        {
            var cl = new ClassDef
            {
                Name = context.identifier().GetText(),
                Type = "interface"
            };

            ClassDefs[cl.Name] = CurrentClass = cl;

            return base.VisitNormalInterfaceDeclaration(context);
        }

        public override bool VisitEnumDeclaration(Java9Parser.EnumDeclarationContext context)
        {
            var e = new ClassDef
            {
                Name = context.identifier().GetText(),
                Type = "enum",
                Members = context.enumBody().enumConstantList().enumConstant().Select(e => new MemberDef
                {
                    Decl = e.GetText(),
                    Kind = MemberKind.EnumConstant
                }).ToHashSet()
            };

            ClassDefs[e.Name] = e;

            return base.VisitEnumDeclaration(context);
        }

        public override bool VisitMethodHeader(Java9Parser.MethodHeaderContext context)
        {
            var typeName = context.result().GetText();

            if (typeName is not null)
            {
                var decl = context.methodDeclarator().GetText();
                CurrentClass.Members.Add(new MemberDef
                {
                    Decl = decl,
                    Type = typeName,
                    Kind = MemberKind.Method
                });
            }

            return base.VisitMethodHeader(context);
        }

        public override bool VisitFieldDeclaration(Java9Parser.FieldDeclarationContext context)
        {
            var typeName = context.unannType().unannReferenceType()?.GetText().Replace("[]", "");

            if (typeName is not null)
            {
                var fieldName = context.variableDeclaratorList().variableDeclarator()[0].GetText();
                CurrentClass.Members.Add(new MemberDef
                {
                    Decl = fieldName,
                    Type = typeName,
                    Kind = MemberKind.Field
                });
            }

            return base.VisitFieldDeclaration(context);
        }
    }
}