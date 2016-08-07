using Narser.API.Parser.Utilities;

namespace Narser.API.Parser
{
    /// <summary>
    /// Represents a token with a generic kind- and value.
    /// </summary>
    /// <typeparam name="TKind">The kind type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    public class Token<TKind, TValue> : Token<TKind> where TKind : struct
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Token{TKind,TValue}"/> class.
        /// </summary>
        /// <param name="value">The token value.</param>
        /// <param name="location">The location.</param>
        /// <param name="kind">The token kind.</param>
        public Token(TValue value, StringLocation location, TKind kind)
            : base(value, location, kind)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the value of the <see cref="Token{TKind, TValue}"/>.
        /// </summary>
        public new TValue Value { get; internal set; }
    }

    /// <summary>
    /// Represents a token with a generic kind.
    /// </summary>
    /// <typeparam name="TKind">The kind type.</typeparam>
    public class Token<TKind> : Token where TKind : struct
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Token{TKind}"/> class.
        /// </summary>
        /// <param name="value">The token value.</param>
        /// <param name="location">The location.</param>
        /// <param name="kind">The token kind-</param>
        public Token(object value, StringLocation location, TKind kind)
            : base(value, location)
        {
            Kind = kind;
        }

        /// <summary>
        /// Gets the kind of the <see cref="Token{TKind}"/>.
        /// </summary>
        public TKind Kind { get; internal set; }
    }

    /// <summary>
    /// Represents a one or more symbols with a specific meaning.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Represents an empty <see cref="Token"/>.
        /// </summary>
        public static Token Empty => new Token();

        /// <summary>
        /// Initializes an instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="value">The token value.</param>
        /// <param name="location">The location.</param>
        public Token(object value, StringLocation location)
        {
            Value = value;
            Location = location;
            Length = 1;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="Token"/> class.
        /// </summary>
        public Token() : this(null, StringLocation.Empty)
        { }
        
        /// <summary>
        /// Gets the value of the <see cref="Token"/>.
        /// </summary>
        public object Value { get; internal set; }

        /// <summary>
        /// Gets the length of the <see cref="Token"/>.
        /// </summary>
        public int Length { get; internal set; }

        /// <summary>
        /// Gets the location of the <see cref="Token"/>.
        /// </summary>
        public StringLocation Location { get; internal set; }

        /// <summary>
        /// Determines whether the left instance is equal to the right instance.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <returns></returns>
        public static bool operator ==(Token left, Token right)
        {
            return left?.Value == right?.Value && left?.Location == right?.Location;
        }

        /// <summary>
        /// Determines whether the left instance isn't equal to the right instance.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <returns></returns>
        public static bool operator !=(Token left, Token right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the left instance is greater than the right instance.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <returns></returns>
        public static bool operator >(Token left, Token right)
        {
            return left.Location > right.Location;
        }

        /// <summary>
        /// Determines whether the left instance is less than the right instance.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <returns></returns>
        public static bool operator <(Token left, Token right)
        {
            return left.Location < right.Location;
        }

        protected bool Equals(Token other)
        {
            return Equals(Value, other.Value) && Length == other.Length && Location.Equals(other.Location);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Token)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Length;
                hashCode = (hashCode * 397) ^ Location.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Creates a token with a specific value and kind.
        /// </summary>
        /// <typeparam name="TKind">The kind type.</typeparam>
        /// <param name="value">The token value.</param>
        /// <param name="location">The location.</param>
        /// <param name="kind">The token kind.</param>
        /// <returns></returns>
        public static Token<TKind> Create<TKind>(object value, StringLocation location, TKind kind) where TKind : struct
        {
            return new Token<TKind>(value, location, kind);
        }

        /// <summary>
        /// Creates a token with a specific kind.
        /// </summary>
        /// <typeparam name="TKind">The kind type.</typeparam>
        /// <param name="kind">The token kind.</param>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        public static Token<TKind> Create<TKind>(TKind kind, StringLocation location) where TKind : struct
        {
            return Create(null, location, kind);
        }
    }
}