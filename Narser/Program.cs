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
                using (lexer = new Lexer(new StringReader(reader.ReadToEnd())))
                    lexer.Tokenize();

            var builder = new SyntaxBuilder(lexer.Tokens);
            var nodes = builder.Build();

            Console.ReadLine();
        }
    }
}