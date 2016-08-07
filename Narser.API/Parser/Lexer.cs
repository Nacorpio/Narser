using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Narser.API.Parser.Utilities;
using BaseToken = Narser.API.Parser.Token<Narser.API.Parser.TokenKind>;

namespace Narser.API.Parser
{
    public class Lexer
    {
        private int _pos = 1, _col = 1, _line = 1;
        private readonly StringReader _reader;

        /// <summary>
        /// Initializes an instance of the <see cref="Lexer"/> class.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public Lexer(StringReader reader)
        {
            IdentifierWhitelist = new [] {'-', '_'};
            _reader = reader;
        }

        /// <summary>
        /// Gets the characters that are allowed inside an identifier.
        /// </summary>
        public char[] IdentifierWhitelist { get; internal set; }

        /// <summary>
        /// Reads the next character in the underlying stream.
        /// </summary>
        /// <returns></returns>
        internal int Read()
        {
            _pos++;
            _col++;

            return _reader.Read();
        }

        /// <summary>
        /// Tokenizes the input in the <see cref="Lexer"/>.
        /// </summary>
        /// <returns></returns>
        public Queue<BaseToken> Tokenize()
        {
            var tokens = new Queue<Token<TokenKind>>();
            while (_reader.Peek() != -1)
            {
                while (char.IsWhiteSpace((char) _reader.Peek()))
                    Read();

                if (_reader.Peek() == -1)
                    break;

                var c = (char) _reader.Peek();
                switch (c)
                {
                    case '\n':
                    {
                        Read();

                        _line++;
                        _col = 1;

                        break;
                    }

                    case '\r':
                    {
                        Read();
                        goto case '\n';
                    }

                    case '§':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.SectionSign, GetLocation()));
                        Read();

                        break;
                    }

                    case '!':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.Exclamation, GetLocation()));
                        Read();

                        break;
                    }

                    case '"':
                    {
                        Token<TokenKind> literal = null;
                        if (ParseStringLiteral(ref literal))
                        {
                            tokens.Enqueue(literal);
                            break;
                        }

                        tokens.Enqueue(Token.Create(TokenKind.DoubleQuote, GetLocation()));
                        Read();

                        break;
                    }

                    case '@':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.At, GetLocation()));
                        Read();

                        break;
                    }

                    case '#':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.NumberSign, GetLocation()));
                        Read();

                        break;
                    }

                    case '£':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.Pound, GetLocation()));
                        Read();

                        break;
                    }

                    case '%':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.Percent, GetLocation()));
                        Read();

                        break;
                    }

                    case '€':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.Euro, GetLocation()));
                        Read();

                        break;
                    }

                    case '$':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.Dollar, GetLocation()));
                        Read();

                        break;
                    }

                    case '¤':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.CurrencySign, GetLocation()));
                        Read();

                        break;
                    }

                    case '&':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.Ampersand, GetLocation()));
                        Read();

                        break;
                    }

                    case '/':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.Slash, GetLocation()));
                        Read();

                        break;
                    }

                    case '{':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.RCurlyBrace, GetLocation()));
                        Read();

                        break;
                    }

                    case '(':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.RBrace, GetLocation()));
                        Read();

                        break;
                    }

                    case '[':
                    {
                        Token<TokenKind> literal = null;
                        if (ParseCharacterClass(ref literal))
                        {
                            tokens.Enqueue(literal);
                            break;
                        }

                        tokens.Enqueue(Token.Create(TokenKind.RSquareBrace, GetLocation()));
                        Read();

                        break;
                    }

                    case ')':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.LBrace, GetLocation()));
                        Read();

                        break;
                    }

                    case ']':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.LSquareBrace, GetLocation()));
                        Read();

                        break;
                    }

                    case '}':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.LCurlyBrace, GetLocation()));
                        Read();

                        break;
                    }

                    case '=':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.Equals, GetLocation()));
                        Read();

                        break;
                    }

                    case '?':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.QuestionMark, GetLocation()));
                        Read();

                        break;
                    }

                    case '+':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.Plus, GetLocation()));
                        Read();

                        break;
                    }

                    case '\\':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.Backslash, GetLocation()));
                        Read();

                        break;
                    }

                    case '´':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.DiacriticalMark, GetLocation()));
                        Read();

                        break;
                    }

                    case '`':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.GraveAccent, GetLocation()));
                        Read();

                        break;
                    }

                    case '^':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.Caret, GetLocation()));
                        Read();

                        break;
                    }

                    case '\'':
                    {
                        Token<TokenKind> literal = null;
                        if (ParseCharLiteral(ref literal))
                        {
                            tokens.Enqueue(literal);
                            break;
                        }

                        tokens.Enqueue(Token.Create(TokenKind.SingleQuote, GetLocation()));
                        Read();

                        break;
                    }

                    case '*':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.Asterisk, GetLocation()));
                        Read();

                        break;
                    }

                    case ',':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.Comma, GetLocation()));
                        Read();

                        break;
                    }

                    case ';':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.Semicolon, GetLocation()));
                        Read();

                        break;
                    }

                    case '.':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.Dot, GetLocation()));
                        Read();

                        break;
                    }

                    case '-':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.Hyphen, GetLocation()));
                        Read();

                        break;
                    }

                    case '_':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.Underscore, GetLocation()));
                        Read();

                        break;
                    }

                    case '<':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.LessThan, GetLocation()));
                        Read();

                        break;
                    }

                    case '>':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.GreaterThan, GetLocation()));
                        Read();

                        break;
                    }

                    case '|':
                    {
                        tokens.Enqueue(Token.Create(TokenKind.Pipe, GetLocation()));
                        Read();

                        break;
                    }

                    default:
                    {
                        if (char.IsLetter(c))
                        {
                            Token<TokenKind> keyword;

                            ParseKeyword(out keyword);
                            tokens.Enqueue(keyword);

                            break;
                        }

                        Read();
                        break;
                    }
                }
            }

            return tokens;
        }

        /// <summary>
        /// Parses a string literal from the <see cref="Lexer"/>.
        /// </summary>
        /// <param name="output">The output token.</param>
        /// <returns></returns>
        internal bool ParseStringLiteral(ref BaseToken output)
        {
            if (_reader.Peek() != '\"')
                return false;

            _reader.Read();

            var sb = new StringBuilder();

            while (_reader.Peek() != '"')
                sb.Append((char) Read());

            Read();

            output = new Token<TokenKind>(sb.ToString(), GetLocation(), TokenKind.StringLiteral)
            {
                Length = sb.Length
            };

            return true;
        }

        /// <summary>
        /// Parses a char literal from the <see cref="Lexer"/>.
        /// </summary>
        /// <param name="output">The output token.</param>
        /// <returns></returns>
        internal bool ParseCharLiteral(ref BaseToken output)
        {
            if (_reader.Peek() != '\'')
                return false;

            _reader.Read();

            var sb = new StringBuilder();

            while (_reader.Peek() != '\'')
                sb.Append((char) Read());

            Read();

            output = new Token<TokenKind>(sb.ToString(), GetLocation(), TokenKind.CharLiteral)
            {
                Length = sb.Length    
            };

            return true;
        }

        /// <summary>
        /// Parses a character class from the <see cref="Lexer"/>.
        /// </summary>
        /// <param name="output">The output token.</param>
        /// <returns></returns>
        internal bool ParseCharacterClass(ref BaseToken output)
        {
            if (_reader.Peek() != '[')
                return false;

            _reader.Read();

            var sb = new StringBuilder();

            while (_reader.Peek() != ']')
                sb.Append((char) Read());

            Read();

            output = new Token<TokenKind>(sb.ToString(), GetLocation(), TokenKind.CharacterClass)
            {
                Length = sb.Length
            };

            return true;
        }

        /// <summary>
        /// Parses a keyword from the <see cref="Lexer"/>.
        /// </summary>
        /// <param name="output">The output token.</param>
        /// <returns></returns>
        internal bool ParseKeyword(out BaseToken output)
        {
            var sb = new StringBuilder();

            while (char.IsLetter((char) _reader.Peek()) || IdentifierWhitelist.Contains((char) _reader.Peek()))
                sb.Append((char) Read());

            TokenKind kind;
            string value = null;

            switch (sb.ToString().ToLower())
            {
                case "and":
                    kind = TokenKind.KeywordAnd;
                    break;

                case "or":
                    kind = TokenKind.KeywordOr;
                    break;

                case "syntaxdef":
                    kind = TokenKind.KeywordSyntaxDef;
                    break;

                case "tokendef":
                    kind = TokenKind.KeywordTokenDef;
                    break;

                default:
                    kind = TokenKind.Identifier;
                    value = sb.ToString();
                    break;
            }

            if (sb.Length == 0)
                kind = TokenKind.Undefined;

            output = new Token<TokenKind>(value, GetLocation(), kind);
            return true;
        }

        /// <summary>
        /// Gets the current location of the <see cref="Lexer"/>.
        /// </summary>
        /// <returns></returns>
        public StringLocation GetLocation()
        {
            return new StringLocation(_pos, _col, _line);
        }

        /// <summary>
        /// Determines whether the <see cref="Lexer"/> is at its end.
        /// </summary>
        /// <returns></returns>
        public bool IsAtEnd()
        {
            return _reader.Peek() == -1;
        }
    }
}