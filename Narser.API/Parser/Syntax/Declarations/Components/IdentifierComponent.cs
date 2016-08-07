namespace Narser.API.Parser.Syntax.Declarations.Components
{
    public class IdentifierComponent : ComponentBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="IdentifierComponent"/> class.
        /// </summary>
        /// <param name="name">The identifier name.</param>
        internal IdentifierComponent(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name of the <see cref="IdentifierComponent"/>.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets whether the <see cref="IdentifierComponent"/> is a reference.
        /// </summary>
        public bool IsReference { get; internal set; }
    }
}
