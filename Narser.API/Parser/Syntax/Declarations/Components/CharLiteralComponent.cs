namespace Narser.API.Parser.Syntax.Declarations.Components
{
    /// <summary>
    /// Represents a character literal component.
    /// </summary>
    public sealed class CharLiteralComponent : ComponentBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="CharLiteralComponent"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        internal CharLiteralComponent(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the value of the <see cref="CharLiteralComponent"/>.
        /// </summary>
        public string Value { get; internal set; }
    }
}