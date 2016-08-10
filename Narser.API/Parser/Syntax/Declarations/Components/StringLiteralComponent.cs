using System.Text.RegularExpressions;

namespace Narser.API.Parser.Syntax.Declarations.Components
{
    /// <summary>
    /// Represents a string literal component.
    /// </summary>
    public sealed class StringLiteralComponent : ComponentBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="StringLiteralComponent"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        internal StringLiteralComponent(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the value of the <see cref="StringLiteralComponent"/>.
        /// </summary>
        public string Value { get; internal set; }

        /// <summary>
        /// Creates a regex instance with the specific flags from the value of the <see cref="StringLiteralComponent"/>.
        /// </summary>
        /// <param name="flags">The regex-option flags.</param>
        /// <returns></returns>
        public Regex CreateRegex(RegexOptions flags)
        {
            return new Regex(Value, flags);
        }
    }
}