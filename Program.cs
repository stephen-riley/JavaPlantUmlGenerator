using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JavaPlantUmlGenerator
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var extractor = new ClassExtractor();

            foreach (var file in args)
            {
                extractor.Extract(file);
            }

            extractor.Emit();
        }

        static void Extract(this ClassExtractor extractor, string file)
        {
            var str = new AntlrInputStream(File.ReadAllText(file));
            var lexer = new Java9Lexer(str);
            var tokens = new CommonTokenStream(lexer);
            var parser = new Java9Parser(tokens);
            var listener_lexer = new ErrorListener<int>();
            var listener_parser = new ErrorListener<IToken>();
            lexer.AddErrorListener(listener_lexer);
            parser.AddErrorListener(listener_parser);
            var tree = parser.compilationUnit();
            // Console.WriteLine(tree.ToStringTree(parser));
            if (listener_lexer.had_error || listener_parser.had_error)
            {
                Console.WriteLine("error in parse.");
            }
            else
            {
                // Console.WriteLine("parse completed.");
                extractor.VisitCompilationUnit(tree);
            }
        }

        static void Emit(this ClassExtractor extractor)
        {
            Console.WriteLine("@startuml");

            // render classes, interfaces, and enums
            foreach (var cl in extractor.ClassDefs.Values)
            {
                if (cl.Name == "Main")
                {
                    continue;
                }

                Console.WriteLine($"{(cl.Abstract ? "abstract " : "")}{cl.Type} {cl.Name} {{");

                foreach (var m in cl.Members.OrderBy(m => m.Decl))
                {
                    var sigil = m.Type == "" ? " " : "+";
                    Console.WriteLine($"{sigil} {m.Type} {m.Decl}");
                }
                Console.WriteLine("}\n");
            }

            foreach (var cl in extractor.ClassDefs.Values)
            {
                if (cl.Superclass is not null)
                {
                    Console.WriteLine($"{cl.Superclass} <|-- {cl.Name}");
                }

                if (cl.ImplementedInterfaces is not null)
                {
                    foreach (var iface in cl.ImplementedInterfaces)
                    {
                        Console.WriteLine($"{iface} <|.. {cl.Name}");
                    }
                }

                if (cl.Members is not null)
                {
                    var done = new HashSet<string>();

                    foreach (var m in cl.Members.Where(m => extractor.ClassDefs.ContainsKey(m.Type)))
                    {
                        var connector = extractor.ClassDefs[m.Type].Type is "class" or "interface" ? "*--" : "..";
                        var line = $"{cl.Name} {connector} {m.Type}";
                        if (!done.Contains(line))
                        {
                            Console.WriteLine(line);
                            done.Add(line);
                        }
                    }
                }
            }

            Console.WriteLine("@enduml");
        }
    }
}
