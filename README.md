# JavaPlantUmlGenerator

A command line program to generate [PlantUML](https://plantuml.com/) class diagrams from Java files.

Written in C#; uses [ANTLR 4.0](https://www.antlr.org/) and its [reference Java grammar](https://github.com/antlr/grammars-v4/tree/master/java/java9) to parse Java files.

## Usage

1. Install [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
1. `git clone ...`
1. `cd JavaPlantUmlGenerator`
1. `dotnet run -- /path/to/*.java > diagram.puml`
