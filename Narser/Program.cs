using System.IO;
using System.Linq;
using Narser.API.Parser;
using Narser.API.Parser.Syntax;
using Narser.API.Parser.Syntax.Nodes;

namespace Narser
{
    public static class Program
    {
        static void Main(string[] args)
        {
            const string path = "example.par";

            Lexer lexer;
            using (var reader = new StreamReader(File.OpenRead(path)))
                lexer = new Lexer(new StringReader(reader.ReadToEnd()));

            var builder = new SyntaxBuilder(lexer.Tokenize());
            var nodes = builder.Build();

            var tokens = nodes.Where(x => x is TokenDefNode).Cast<TokenDefNode>()
                .ToArray();

            var syntax = (SyntaxDefNode) nodes.FirstOrDefault(x => x is SyntaxDefNode);

            var rlAlphaOrDigit = syntax?["alpha-or-digit"];

            var binaryExp = rlAlphaOrDigit?.GetDeclaration()
                .AsBinaryExpression();
        }
    }
}
