namespace Narser.API.Parser.Syntax.Declarations.Components
{
    /// <summary>
    /// Represents a component which is a reference to another declaration.
    /// </summary>
    public sealed class RefComponent : ComponentBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="RefComponent"/> class.
        /// </summary>
        /// <param name="referenceName">The name of the reference.</param>
        internal RefComponent(string referenceName)
        {
            ReferenceName = referenceName;
        }

        /// <summary>
        /// Gets the reference name of the <see cref="RefComponent"/>.
        /// </summary>
        public string ReferenceName { get; internal set; }
    }
}
