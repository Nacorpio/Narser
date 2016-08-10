namespace Narser.API.Parser.Utilities
{
    /// <summary>
    /// Represents a location within a <see cref="string"/>.
    /// </summary>
    public struct StringLocation
    {
        /// <summary>
        /// Represents an empty <see cref="StringLocation"/>.
        /// </summary>
        public static StringLocation Empty => new StringLocation(1, 1, 1);

        /// <summary>
        /// Initializes an instance of the <see cref="StringLocation"/> structure.
        /// </summary>
        /// <param name="pos">The one-based position.</param>
        /// <param name="col">The column.</param>
        /// <param name="line">The line number.</param>
        public StringLocation(int pos, int col, int line)
        {
            Position = pos;
            Column = col;
            Line = line;
        }

        /// <summary>
        /// Gets the one-based position of the <see cref="StringLocation"/>.
        /// </summary>
        public int Position { get; internal set; }

        /// <summary>
        /// Gets the column of the <see cref="StringLocation"/>.
        /// </summary>
        public int Column { get; internal set; }

        /// <summary>
        /// Gets the line number of the <see cref="StringLocation"/>.
        /// </summary>
        public int Line { get; internal set; }

        /// <summary>
        /// Determines whether the left instance is greater than the right instance.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <returns></returns>
        public static bool operator >(StringLocation left, StringLocation right)
        {
            return left.Position > right.Position;
        }

        /// <summary>
        /// Determines whether the left instance is less than the right instance.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <returns></returns>
        public static bool operator <(StringLocation left, StringLocation right)
        {
            return left.Position < right.Position;
        }

        /// <summary>
        /// Determines whether the left instance is equal to the right instance.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <returns></returns>
        public static bool operator ==(StringLocation left, StringLocation right)
        {
            return left.Position == right.Position;
        }

        /// <summary>
        /// Determines whether the left instance isn't equal to the right instance.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <returns></returns>
        public static bool operator !=(StringLocation left, StringLocation right)
        {
            return !(left == right);
        }

        public bool Equals(StringLocation other)
        {
            return Position == other.Position && Column == other.Column && Line == other.Line;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is StringLocation && Equals((StringLocation)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Position;
                hashCode = (hashCode * 397) ^ Column;
                hashCode = (hashCode * 397) ^ Line;
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"[{Column} @ {Line}]";
        }
    }
}