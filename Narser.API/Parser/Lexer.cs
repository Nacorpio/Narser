using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Narser.API.Parser.Utilities;
using BaseToken = Narser.API.Parser.Token<Narser.API.Parser.TokenKind>;

namespace Narser.API.Parser
{
    public class Lexer : IDisposable
    {
        private readonly char[] _escapeSequences = {'n', 'r', 'a', 'b', 'f', 't', 'v', '\'', '"', '\\', '?'};

        private int _pos = 1, _col = 1, _line = 1;
        private readonly StringReader _reader;

        /// <summary>
        /// Initializes an instance of the <see cref="Lexer"/> class.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public Lexer(StringReader reader)
        {
            IdentifierWhitelist = new[] {'-', '_'};
            _reader = reader;

            Tokens = new Queue<BaseToken>();
        }

        /// <summary>
        /// Gets the resulting tokens of the <see cref="Lexer"/>.
        /// </summary>
        public Queue<BaseToken> Tokens { get; }

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
        /// Prints an error to the console.
        /// </summary>
        /// <param name="output">The output.</param>
        private void Error(string output)
        {
            if (string.IsNullOrWhiteSpace(output))
                throw new ArgumentNullException(nameof(output));

            Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}]\t{GetLocation()}\t{output}");
        }

        /// <summary>
        /// Prints an expectancy error to the console.
        /// </summary>
        /// <param name="unexpected">The unexpected character.</param>
        /// <param name="expected">The expected character.</param>
        private void ExpectError(char unexpected, char expected)
        {
            Error($"Unexpected character '{unexpected}'; expected '{expected}'.");
        }

        /// <summary>
        /// Adds a token to the <see cref="Lexer"/> results.
        /// </summary>
        /// <param name="kind">The token kind to add.</param>
        /// <param name="location">The location of the token.</param>
        /// <param name="read">Whether to read after adding.</param>
        private void Add(TokenKind kind, StringLocation location, bool read = true)
        {
            Tokens.Enqueue(Token.Create(kind, location));

            if (read)
                Read();
        }

        /// <summary>
        /// Adds a token to the <see cref="Lexer"/> results.
        /// </summary>
        /// <param name="kind">The token to add.</param>
        /// <param name="read">Whether to read after the adding.</param>
        private void Add(TokenKind kind, bool read = true)
        {
            Add(kind, GetLocation(), read);
        }
        
        /// <summary>
        /// Tokenizes the input in the <see cref="Lexer"/>.
        /// </summary>
        /// <returns></returns>
        public void Tokenize()
        {
            while (!IsAtEnd())
            {
                while ((char) _reader.Peek() == ' ')
                    Read();

                if (IsAtEnd())
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
                        Add(TokenKind.SectionSign);
                        break;
                    }

                    case '!':
                    {
                        Add(TokenKind.Exclamation);
                        break;
                    }

                    case '"':
                    {
                        Token<TokenKind> literal = null;
                        if (ParseStringLiteral(ref literal))
                        {
                            Tokens.Enqueue(literal);
                            break;
                        }

                        Add(TokenKind.DoubleQuote);
                        break;
                    }

                    case '@':
                    {
                        Add(TokenKind.At);
                        break;
                    }

                    case '#':
                    {
                        Add(TokenKind.NumberSign);
                        break;
                    }

                    case '£':
                    {
                        Add(TokenKind.Pound);
                        break;
                    }

                    case '%':
                    {
                        Add(TokenKind.Percent);
                        break;
                    }

                    case '€':
                    {
                        Add(TokenKind.Euro);
                        break;
                    }

                    case '$':
                    {
                        Add(TokenKind.Dollar);
                        break;
                    }

                    case '¤':
                    {
                        Add(TokenKind.CurrencySign);
                        break;
                    }

                    case '&':
                    {
                        Add(TokenKind.Ampersand);
                        break;
                    }

                    case '/':
                    {
                        Add(TokenKind.Slash);
                        break;
                    }

                    case '{':
                    {
                        Add(TokenKind.RCurlyBrace);
                        break;
                    }

                    case '(':
                    {
                        Add(TokenKind.RBrace);
                        break;
                    }

                    case '[':
                    {
                        Token<TokenKind> literal = null;
                        if (ParseCharacterClass(ref literal))
                        {
                            Tokens.Enqueue(literal);
                            break;
                        }

                        Add(TokenKind.RSquareBrace);
                        break;
                    }

                    case ')':
                    {
                        Add(TokenKind.LBrace);
                        break;
                    }

                    case ']':
                    {
                        Add(TokenKind.LSquareBrace);
                        break;
                    }

                    case '}':
                    {
                        Add(TokenKind.LCurlyBrace);
                        break;
                    }

                    case '=':
                    {
                        Add(TokenKind.Equals);
                        break;
                    }

                    case '?':
                    {
                        Add(TokenKind.QuestionMark);
                        break;
                    }

                    case '+':
                    {
                        Add(TokenKind.Plus);
                        break;
                    }

                    case '\\':
                    {
                        Add(TokenKind.Backslash);
                        break;
                    }

                    case '´':
                    {
                        Add(TokenKind.DiacriticalMark);
                        break;
                    }

                    case '`':
                    {
                        Add(TokenKind.GraveAccent);
                        break;
                    }

                    case '^':
                    {
                        Add(TokenKind.Caret);
                        break;
                    }

                    case '\'':
                    {
                        Token<TokenKind> literal = null;
                        if (ParseCharLiteral(ref literal))
                        {
                            Tokens.Enqueue(literal);
                            break;
                        }

                        Add(TokenKind.SingleQuote);
                        break;
                    }

                    case '*':
                    {
                        Add(TokenKind.Asterisk);
                        break;
                    }

                    case ',':
                    {
                        Add(TokenKind.Comma);
                        break;
                    }

                    case ';':
                    {
                        Add(TokenKind.Semicolon);
                        break;
                    }

                    case '.':
                    {
                        Add(TokenKind.Dot);
                        break;
                    }

                    case '-':
                    {
                        Add(TokenKind.Hyphen);
                        break;
                    }

                    case '_':
                    {
                        Add(TokenKind.Underscore);
                        break;
                    }

                    case '<':
                    {
                        Add(TokenKind.LessThan);
                        break;
                    }

                    case '>':
                    {
                        Add(TokenKind.GreaterThan);
                        break;
                    }

                    case '|':
                    {
                        Add(TokenKind.Pipe);
                        break;
                    }

                    default:
                    {
                        if (char.IsLetter(c))
                        {
                            Token<TokenKind> keyword;

                            ParseKeyword(out keyword);
                            Tokens.Enqueue(keyword);

                            break;
                        }

                        Read();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Parses a string literal from the <see cref="Lexer"/>.
        /// </summary>
        /// <param name="output">The output token.</param>
        /// <returns></returns>
        internal bool ParseStringLiteral(ref BaseToken output)
        {
            if (_reader.Peek() != '"')
                return false;

            Read();

            var start = GetLocation();
            var sb = new StringBuilder();

            while (_reader.Peek() != '"')
                sb.Append((char) Read());

            Read();

            output = new Token<TokenKind>(sb.ToString(), start, TokenKind.StringLiteral)
            {
                Length = sb.Length,
                End = GetLocation()
            };

            return true;
        }

        /// <summary>
        /// Parses a char literal from the <see cref="Lexer"/>.
        /// </summary>
        /// <param name="output">The output token.</param>
        /// <returns></returns>
        private bool ParseCharLiteral(ref BaseToken output)
        {
            // Check if the declaration has a leading single-quote.
            if (_reader.Peek() != '\'')
                return false;

            _reader.Read();

            var sb = new StringBuilder();
            var start = GetLocation();
            var isEscapeSequence = false;

            // Check if the literal has a leading backslash.
            if (_reader.Peek() == '\\')
            {
                isEscapeSequence = true;
                _reader.Read();
            }

            // Read until it hits the last single-quote.
            while (_reader.Peek() != '\'')
                sb.Append((char) Read());

            if (isEscapeSequence)
            {
                if (sb.Length > 2 || !_escapeSequences.Contains(sb[0]))
                {
                    Error("Invalid escape sequence.");
                    return false;
                }
            }
            else
            {
                if (sb.Length > 1)
                {
                    Error("Too many characters in literal.");
                    return false;
                }
            }

            Read();

            output = new Token<TokenKind>(sb.ToString(), start, TokenKind.CharLiteral)
            {
                Length = sb.Length,
                End = GetLocation()
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
            var start = GetLocation();

            while (_reader.Peek() != ']')
                sb.Append((char) Read());

            Read();

            output = new Token<TokenKind>(sb.ToString(), start, TokenKind.CharacterClass)
            {
                Length = sb.Length,
                End = GetLocation()
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
            var start = GetLocation();

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

            output = new Token<TokenKind>(value, start, kind)
            {
                Length = sb.Length,
                End = GetLocation()
            };

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

        /// <summary>
        /// Disposes the <see cref="Lexer"/>.
        /// </summary>
        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}