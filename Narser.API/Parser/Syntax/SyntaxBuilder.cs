using System;
using System.Collections.Generic;
using Narser.API.Parser.Syntax.Nodes;

namespace Narser.API.Parser.Syntax
{
    public sealed class SyntaxBuilder
    {
        /// <summary>
        /// Initializes an instance of the <see cref="SyntaxBuilder"/> class.
        /// </summary>
        /// <param name="tokens">The stack of tokens.</param>
        public SyntaxBuilder(Queue<Token<TokenKind>> tokens)
        {
            if (tokens == null)
                throw new ArgumentNullException(nameof(tokens));

            Tokens = tokens;
        }

        /// <summary>
        /// Gets the tokens of the <see cref="SyntaxBuilder"/>.
        /// </summary>
        public Queue<Token<TokenKind>> Tokens { get; internal set; }

        /// <summary>
        /// Peeks the next token in the underlying stack.
        /// </summary>
        /// <returns></returns>
        public Token<TokenKind> Peek()
        {
            return Tokens.Peek();
        }

        /// <summary>
        /// Pops the next token in the underlying stack.
        /// </summary>
        /// <returns></returns>
        public Token<TokenKind> Next()
        {
            return Tokens.Dequeue();
        }

        /// <summary>
        /// Builds of nodes using the tokens of the <see cref="SyntaxBuilder"/>.
        /// </summary>
        /// <returns></returns>
        public Queue<SyntaxNode> Build()
        {
            var nodes = new Queue<SyntaxNode>();
            SyntaxNode node = null;

            while (Tokens.Count > 0)
            {
                var current = Peek();
                switch (current.Kind)
                {
                    case TokenKind.KeywordSyntaxDef:
                    {
                        ParseSyntaxDef(ref node);
                        break;
                    }

                    case TokenKind.KeywordTokenDef:
                    {
                        Next();
                        break;
                    }
                }

                if (node != null)
                    nodes.Enqueue(node);
            }

            return nodes;
        }

        /// <summary>
        /// Parses a syntax definition at the current position.
        /// </summary>
        /// <param name="output">The resulting syntax definition.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        public bool ParseSyntaxDef(ref SyntaxNode output)
        {
            if (Peek().Kind != TokenKind.KeywordSyntaxDef)
                return false;

            Next();

            if (Peek().Kind != TokenKind.Identifier)
                return false;

            var name = (string) Next().Value;
            output = new SyntaxDefNode(name);

            return true;
        }
    }
}