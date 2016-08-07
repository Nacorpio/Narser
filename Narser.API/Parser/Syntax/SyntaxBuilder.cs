using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Narser.API.Parser.Extensions;
using Narser.API.Parser.Syntax.Declarations;
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
        /// Expects a specific <see cref="TokenKind"/> predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <param name="advance">Whether to advance if successful.</param>
        private void Expect(Predicate<TokenKind> predicate, bool advance = true)
        {
            if (!predicate(Peek().Kind))
            {
                throw new Exception($"Unexpected token '{Peek().Kind}'.");
            }

            if (advance)
                Next();
        }

        /// <summary>
        /// Expects a specific <see cref="TokenKind"/>
        /// </summary>
        /// <param name="kind">The kind to expect.</param>
        /// <param name="advance">Whether to advance if successful.</param>
        private void Expect(TokenKind kind, bool advance = true)
        {
            Expect(tokenKind => tokenKind == kind, advance);
        }

        /// <summary>
        /// Builds a tree of nodes using the tokens in the <see cref="SyntaxBuilder"/>.
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
                        ParseTokenDef(ref node);
                        break;
                    }
                }

                if (node != null)
                {
                    nodes.Enqueue(node);
                    node = null;
                }
            }

            return nodes;
        }

        /// <summary>
        /// Parses a token definition at the current position.
        /// </summary>
        /// <param name="output">The resulting token definition.</param>
        /// <returns></returns>
        public bool ParseTokenDef(ref SyntaxNode output)
        {
            var start = Peek();

            Expect(TokenKind.KeywordTokenDef);
            Expect(TokenKind.Identifier, false);

            var name = (string) Next().Value;
            var isInheriting = Peek().Kind == TokenKind.Identifier;

            string inheritance = null;

            if (isInheriting)
                inheritance = (string) Next().Value;

            Expect(TokenKind.RCurlyBrace);

            var rules = new Collection<RuleDefNode>();

            SyntaxNode rule;
            while (ParseRuleDef(output, out rule))
                rules.Add((RuleDefNode)rule);

            Expect(TokenKind.LCurlyBrace);
            Expect(TokenKind.Semicolon);

            output = new TokenDefNode(name, rules)
            {
                Token = start,
                Inheritance = inheritance
            };

            return true;
        }

        /// <summary>
        /// Parses a syntax definition at the current position.
        /// </summary>
        /// <param name="output">The resulting syntax definition.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        public bool ParseSyntaxDef(ref SyntaxNode output)
        {
            var start = Peek();

            Expect(TokenKind.KeywordSyntaxDef);
            Expect(TokenKind.Identifier, false);

            var name = (string) Next().Value;

            Expect(TokenKind.RCurlyBrace);

            var rules = new Collection<RuleDefNode>();

            SyntaxNode rule;
            while (ParseRuleDef(output, out rule))
                rules.Add((RuleDefNode) rule);

            Expect(TokenKind.LCurlyBrace);
            Expect(TokenKind.Semicolon);

            output = new SyntaxDefNode(name, rules)
            {
                Token = start
            };

            return true;
        }

        /// <summary>
        /// Parses a rule definition at the current position.
        /// </summary>
        /// <param name="parent">The parental node.</param>
        /// <param name="output">The resulting rule definition.</param>
        /// <returns></returns>
        public bool ParseRuleDef(SyntaxNode parent, out SyntaxNode output)
        {
            if (Peek().Kind != TokenKind.Identifier &&
                Peek().Kind != TokenKind.StringLiteral &&
                Peek().Kind != TokenKind.CharLiteral)
            {
                output = null;
                return false;
            }

            var start = Peek();
            var name = (string) Next().Value;

            var tokens = new Queue<Token<TokenKind>>();
            while (Peek().Kind != TokenKind.Semicolon)
                tokens.Enqueue(Next());

            Next();

            RuleDefDeclaration decl;
            tokens.Parse(out decl);

            output = new RuleDefNode(name, decl)
            {
                Parent = parent,
                Token = start
            };

            return true;
        }
    }
}