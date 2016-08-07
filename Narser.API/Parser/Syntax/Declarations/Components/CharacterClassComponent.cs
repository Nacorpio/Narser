namespace Narser.API.Parser.Syntax.Declarations.Components
{
    /// <summary>
    /// Represents a character class component.
    /// </summary>
    public sealed class CharacterClassComponent : ComponentBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="CharacterClassComponent"/> class.
        /// </summary>
        /// <param name="values">The values.</param>
        internal CharacterClassComponent(char[] values)
        {
            Values = values;
        }

        /// <summary>
        /// Gets the values of the <see cref="CharacterClassComponent"/>.
        /// </summary>
        public char[] Values { get; internal set; }
    }
}