using Narser.API.Parser.Utilities;

namespace Narser.API.Parser.Syntax.Declarations.Components
{
    /// <summary>
    /// Represents a component flag.
    /// </summary>
    public enum ComponentFlag
    { }

    /// <summary>
    /// Represents a declaration component, which forms a <see cref="DeclarationBase"/>.
    /// </summary>
    public abstract class ComponentBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="ComponentBase"/> class.
        /// </summary>
        protected ComponentBase()
        { }

        /// <summary>
        /// Gets the first token of the <see cref="ComponentBase"/>.
        /// </summary>
        public Token Token { get; internal set; }

        /// <summary>
        /// Returns the location of the <see cref="ComponentBase"/>.
        /// </summary>
        public StringLocation GetLocation() => Token.Start;
    }
}