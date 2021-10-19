using Antlr4.Runtime;
using System.IO;

namespace JavaPlantUmlGenerator
{
    public class ErrorListener<S> : ConsoleErrorListener<S>
    {
        public bool had_error;

        public override void SyntaxError(TextWriter output, IRecognizer recognizer, S offendingSymbol, int line,
            int col, string msg, RecognitionException e)
        {
            had_error = true;
            base.SyntaxError(output, recognizer, offendingSymbol, line, col, msg, e);
        }
    }
}