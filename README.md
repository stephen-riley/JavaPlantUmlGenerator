# JavaPlantUmlGenerator

A command line program to generate [PlantUML](https://plantuml.com/) class diagrams from Java files.

Written in C#; uses [ANTLR 4](https://www.antlr.org/) and its [reference Java grammar](https://github.com/antlr/grammars-v4/tree/master/java/java9) to parse Java files.

## Usage

1. Install [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
1. `git clone https://github.com/stephen-riley/JavaPlantUmlGenerator.git`
1. `cd JavaPlantUmlGenerator`
1. `dotnet run -- /path/to/*.java > diagram.puml`

## About

This is a very simple project that illustrates how to use a pre-existing grammar and language tools to construct a simple "transpiler"; in this case, from Java source code to [PlantUML class diagrams](https://plantuml.com/class-diagram).

The structure of the program is quite simple. ANTLR is used to generate a lexer, parser, and [Visitor](https://en.wikipedia.org/wiki/Visitor_pattern) [base class](https://saumitra.me/blog/antlr4-visitor-vs-listener-pattern/) from a Java 9 grammar. The [ClassExtractor](./ClassExtractor.cs) class subclasses the Visitor base and overrides a minimal number of methods to intercept certain points of the grammar; namely:

- [normalClassDeclaration](blob/main/Java9Parser.g4#L328)
- [normalInterfaceDeclaration](blob/main/Java9Parser.g4#L629)
- [enumDeclaration](blob/main/Java9Parser.g4#L596)
- [methodHeader](blob/main/Java9Parser.g4#L497)
- [fieldDeclaration](blob/main/Java9Parser.g4#L382)

These overrides record information such as class names and members, which are then emitted in `.puml` syntax by the [Emit()](blob/main/Program.cs#L46) method.
