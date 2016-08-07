using System.Collections.ObjectModel;
using Narser.API.Parser.Syntax.Declarations;
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
        public static bool Parse(this TQueue queue, out ComponentBase output)
        {
            switch (queue.Peek().Kind)
            {
                case TokenKind.StringLiteral:
                {
                    StringLiteralComponent component;
                    if (!Parse(queue, out component))
                    {
                        output = null;
                        return false;
                    }

                    output = component;
                    return true;
                }

                case TokenKind.CharLiteral:
                {
                    CharLiteralComponent component;
                    if (!Parse(queue, out component))
                    {
                        output = null;
                        return false;
                    }

                    output = component;
                    return true;
                }

                case TokenKind.CharacterClass:
                {
                    CharacterClassComponent component;
                    if (!Parse(queue, out component))
                    {
                        output = null;
                        return false;
                    }

                    output = component;
                    return true;
                }

                case TokenKind.At:
                {
                    RefComponent component;
                    if (!Parse(queue, out component))
                    {
                        output = null;
                        return false;
                    }

                    output = component;
                    return true;
                }

                case TokenKind.Identifier:
                {
                    IdentifierComponent component;
                    if (!Parse(queue, out component))
                    {
                        output = null;
                        return false;
                    }

                    output = component;
                    return true;
                }
            }

            output = null;
            return false;
        }

        /// <summary>
        /// Parses a declaration using the specific token queue.
        /// </summary>
        /// <param name="queue">A queue of tokens.</param>
        /// <param name="output">The resulting declaration.</param>
        /// <returns></returns>
        public static bool Parse(this TQueue queue, out RuleDefDeclaration output)
        {
            var components = new Collection<ComponentBase>();
            ComponentBase component;

            while (queue.Count > 0 && queue.Parse(out component))
                components.Add(component);

            output = new RuleDefDeclaration(components);
            return true;
        }

        /// <summary>
        /// Parses an identifier component using the specific token queue.
        /// </summary>
        /// <param name="queue">A queue of tokens.</param>
        /// <param name="output">The resulting identifier component.</param>
        /// <returns></returns>
        public static bool Parse(this TQueue queue, out IdentifierComponent output)
        {
            if (queue.Peek().Kind != TokenKind.Identifier)
            {
                output = null;
                return false;
            }

            output = new IdentifierComponent((string) queue.Dequeue().Value);
            return true;
        }

        /// <summary>
        /// Parses a reference component using the specific token queue.
        /// </summary>
        /// <param name="queue">A queue of tokens.</param>
        /// <param name="output">The resulting reference component.</param>
        /// <returns></returns>
        public static bool Parse(this TQueue queue, out RefComponent output)
        {
            if (queue.Peek().Kind != TokenKind.At)
            {
                output = null;
                return false;
            }

            // Move past the reference symbol.
            queue.Dequeue();

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

            output = new CharLiteralComponent((string) queue.Dequeue().Value);
            return true;
        }
    }
}