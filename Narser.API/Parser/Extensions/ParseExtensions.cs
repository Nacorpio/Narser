using Narser.API.Parser.Syntax.Declarations.Components;
using TQueue = System.Collections.Generic.Queue<Narser.API.Parser.Token<Narser.API.Parser.TokenKind>>;

namespace Narser.API.Parser.Extensions
{
    /// <summary>
    /// Represents a collection of parsing extensions.
    /// </summary>
    public static class ParseExtensions
    {
        /// <summary>
        /// Parses a component using the specific token queue.
        /// </summary>
        /// <param name="queue">A queue of tokens.</param>
        /// <param name="output">The resulting component.</param>
        /// <returns></returns>
        public static bool Parse(this TQueue queue, ref ComponentBase output)
        {
            switch (queue.Peek().Kind)
            {
                case TokenKind.StringLiteral:
                {
                    StringLiteralComponent component;
                    return Parse(queue, out component);
                }

                case TokenKind.CharLiteral:
                {
                    CharLiteralComponent component;
                    return Parse(queue, out component);
                }

                case TokenKind.CharacterClass:
                {
                    CharacterClassComponent component;
                    return Parse(queue, out component);
                }

                case TokenKind.Identifier:
                {
                    RefComponent component;
                    return Parse(queue, out component);
                }
            }

            return false;
        }

        /// <summary>
        /// Parses a reference component using the specific token queue.
        /// </summary>
        /// <param name="queue">A queue of tokens.</param>
        /// <param name="output">The resulting reference component.</param>
        /// <returns></returns>
        public static bool Parse(this TQueue queue, out RefComponent output)
        {
            if (queue.Peek().Kind != TokenKind.Identifier)
            {
                output = null;
                return false;
            }

            output = new RefComponent((string) queue.Dequeue().Value);
            return true;
        }

        /// <summary>
        /// Parses a character class component using the specific token queue.
        /// </summary>
        /// <param name="queue">A queue of tokens.</param>
        /// <param name="output">The resulting character class component.</param>
        /// <returns></returns>
        public static bool Parse(this TQueue queue, out CharacterClassComponent output)
        {
            if (queue.Peek().Kind != TokenKind.CharacterClass)
            {
                output = null;
                return false;
            }

            output = new CharacterClassComponent(((string) queue.Dequeue().Value).ToCharArray());
            return true;
        }

        /// <summary>
        /// Parses a string literal component using the specific token queue.
        /// </summary>
        /// <param name="queue">A queue of tokens</param>
        /// <param name="output">The resulting string literal component.</param>
        /// <returns></returns>
        public static bool Parse(this TQueue queue, out StringLiteralComponent output)
        {
            if (queue.Peek().Kind != TokenKind.StringLiteral)
            {
                output = null;
                return false;
            }

            output = new StringLiteralComponent((string) queue.Dequeue().Value);
            return true;
        }

        /// <summary>
        /// Parses a character literal component using the specific token queue.
        /// </summary>
        /// <param name="queue">A queue of tokens.</param>
        /// <param name="output">The resulting character literal component.</param>
        /// <returns></returns>
        public static bool Parse(this TQueue queue, out CharLiteralComponent output)
        {
            if (queue.Peek().Kind != TokenKind.CharLiteral)
            {
                output = null;
                return false;
            }

            output = new CharLiteralComponent(char.Parse((string) queue.Dequeue().Value));
            return true;
        }
    }
}