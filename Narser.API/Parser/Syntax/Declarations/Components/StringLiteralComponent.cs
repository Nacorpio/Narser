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
    }
}