using System.Collections.Generic;
using Narser.API.Parser.Syntax.Declarations.Components;

namespace Narser.API.Parser.Extensions
{
    /// <summary>
    /// Represents a collection of parsing extensions.
    /// </summary>
    public static class ParseExtensions
    {
        /// <summary>
        /// Parses a string literal component using the specific tokens queue.
        /// </summary>
        /// <param name="tokens">A queue of tokens</param>
        /// <param name="output">The resulting string literal.</param>
        /// <returns></returns>
        public static bool Parse(this Queue<Token<TokenKind>> tokens, ref StringLiteralComponent output)
        {
            if (tokens.Peek().Kind != TokenKind.StringLiteral)
                return false;

            output = new StringLiteralComponent((string)tokens.Dequeue().Value);
            return true;
        }
    }
}
