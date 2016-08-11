using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Narser.API.Parser.Syntax.Declarations;
using Narser.API.Parser.Syntax.Declarations.Components;
using Narser.API.Parser.Syntax.Declarations.Components.Compounds;
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
            if (queue == null)
                throw new ArgumentNullException(nameof(queue));

            if (queue.Count == 0)
                throw new ArgumentException(nameof(queue));

            if (queue.Peek().Kind == TokenKind.RBrace)
                queue.Dequeue();

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

                case TokenKind.Identifier:
                    {
                        IdentifierComponent identifierComponent;
                        if (!Parse(queue, out identifierComponent))
                        {
                            output = null;
                            return false;
                        }

                        output = identifierComponent;
                        break;
                    }

                case TokenKind.Pipe:
                    {
                        output = new OperatorComponent(OperatorType.BitwiseOr);
                        queue.Dequeue();
                        break;
                    }

                case TokenKind.Ampersand:
                    {
                        output = new OperatorComponent(OperatorType.BitwiseAnd);
                        queue.Dequeue();
                        break;
                    }
            }

            if (queue.Peek().Kind == TokenKind.LBrace)
                queue.Dequeue();

            return true;
        }

        /// <summary>
        /// Parses a binary expression component using the specific token queue.
        /// </summary>
        /// <param name="queue">A queue of tokens.</param>
        /// <param name="component">The resulting binary component.</param>
        /// <returns></returns>
        private static bool Parse(TQueue queue, out BinaryComponent component)
        {
            if (queue == null)
                throw new ArgumentNullException(nameof(queue));

            if (queue.Count == 0)
                throw new ArgumentException(nameof(queue));

            ComponentBase left = null;
            if (!Parse(queue, ref left))
            {
                component = null;
                return false;
            }

            ComponentBase @operator = null;
            if (!Parse(queue, ref @operator))
            {
                component = null;
                return false;
            }

            ComponentBase right = null;
            if (!Parse(queue, ref right))
            {
                component = null;
                return false;
            }

            component = null;
            return true;
        }

        /// <summary>
        /// Parses a binary expression component using the specific component queue.
        /// </summary>
        /// <param name="queue">A queue of components.</param>
        /// <param name="output">The resulting binary component.</param>
        /// <returns></returns>
        public static bool Parse(this Queue<ComponentBase> queue, out BinaryComponent output)
        {
            if (queue == null)
                throw new ArgumentNullException(nameof(queue));

            if (queue.Count == 0)
                throw new ArgumentException(nameof(queue));

            if (queue.Count < 3)
            {
                output = null;
                return true;
            }

            var start = queue.Peek().Token;
            var left = queue.Dequeue();

            if (!(queue.Peek() is OperatorComponent))
            {
                output = null;
                return false;
            }

            var op = (OperatorComponent) queue.Dequeue();
            var right = queue.Dequeue();

            output = new BinaryComponent(left, op, right)
            {
                Token = start
            };

            return true;
        }

        /// <summary>
        /// Parses a declaration using the specific token queue.
        /// </summary>
        /// <param name="queue">A queue of tokens.</param>
        /// <param name="output">The resulting declaration.</param>
        /// <returns></returns>
        public static bool Parse(this TQueue queue, out RuleDefDeclaration output)
        {
            if (queue == null)
                throw new ArgumentNullException(nameof(queue));

            if (queue.Count == 0)
                throw new ArgumentException(nameof(queue));

            var components = new Collection<ComponentBase>();
            ComponentBase component = null;

            while (queue.Count > 0 && queue.Parse(ref component))
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
            if (queue == null)
                throw new ArgumentNullException(nameof(queue));

            if (queue.Count == 0)
                throw new ArgumentNullException(nameof(queue));

            var start = queue.Peek();
            if (queue.Peek().Kind != TokenKind.Identifier)
            {
                output = null;
                return false;
            }

            output = new IdentifierComponent((string) queue.Dequeue().Value)
            {
                Token = start
            };

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
            if (queue == null)
                throw new ArgumentNullException(nameof(queue));

            if (queue.Count == 0)
                throw new ArgumentException(nameof(queue));

            if (queue.Peek().Kind != TokenKind.CharacterClass)
            {
                output = null;
                return false;
            }

            var start = queue.Peek();

            output = new CharacterClassComponent(((string) queue.Dequeue().Value).ToCharArray())
            {
                Token = start
            };

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
            if (queue == null)
                throw new ArgumentNullException(nameof(queue));

            if (queue.Count == 0)
                throw new ArgumentException(nameof(queue));

            if (queue.Peek().Kind != TokenKind.StringLiteral)
            {
                output = null;
                return false;
            }

            var start = queue.Peek();

            output = new StringLiteralComponent((string) queue.Dequeue().Value)
            {
                Token = start
            };

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
            if (queue == null)
                throw new ArgumentNullException(nameof(queue));

            if (queue.Count == 0)
                throw new ArgumentException(nameof(queue));

            if (queue.Peek().Kind != TokenKind.CharLiteral)
            {
                output = null;
                return false;
            }

            var start = queue.Peek();

            output = new CharLiteralComponent((string) queue.Dequeue().Value)
            {
                Token = start
            };

            return true;
        }
    }
}