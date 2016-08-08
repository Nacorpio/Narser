namespace Narser.API.Parser
{
    /// <summary>
    /// Represents a type of <see cref="Token"/>.
    /// </summary>
    public enum TokenKind
    {
        Undefined = 0x00,
        Whitespace,
        LineFeed,
        Tab,
        CarriageReturn,
        Dot,
        Comma,
        Colon,
        Semicolon,
        At,
        Exclamation,
        Pound,
        Euro,
        Dollar,
        Ampersand,
        NumberSign,

        DoubleQuote,
        SingleQuote,

        RBrace,
        LBrace,
        RCurlyBrace,
        LCurlyBrace,
        RSquareBrace,
        LSquareBrace,

        Slash,
        Backslash,

        Plus,
        Hyphen,
        Asterisk,
        Percent,
        Equals,
        QuestionMark,
        Caret,
        SectionSign,
        GreaterThan,
        LessThan,
        Tilde,
        CurrencySign,
        Pipe,
        Underscore,
        GraveAccent,
        DiacriticalMark,
        Identifier,
        NumericLiteral,
        EndOfInput,

        StringLiteral,
        CharLiteral,
        CharacterClass,

        True,
        False,

        BitwiseAnd,
        BitwiseOr,
        BitwiseXor,
        GreaterThanOrEqualTo,
        LessThanOrEqualTo,

        KeywordAnd,
        KeywordOr,
        KeywordSyntaxDef,
        KeywordTokenDef,
        Regex
    }
}