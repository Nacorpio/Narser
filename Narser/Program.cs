using System;
using System.Diagnostics;
using System.IO;
using Narser.API.Parser;
using Narser.API.Parser.Syntax;

namespace Narser
{
    public static class Program
    {
        static void Main(string[] args)
        {
            const string path = "example.par";

            var lexer = new Lexer(new StringReader(new StreamReader(File.OpenRead(path)).ReadToEnd()));
            var builder = new SyntaxBuilder(lexer.Tokenize());
            var nodes = builder.Build();
        }
    }
}
