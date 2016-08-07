using System;
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

            var keywords = tokens.FirstOrDefault(x => x.Name == "KEYWORD");
            var operators = tokens.FirstOrDefault(x => x.Name == "OPERATOR");

            var kwUsing = keywords?["using"];
        }
    }
}
