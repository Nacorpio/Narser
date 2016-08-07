using System.Collections.Generic;

namespace Narser.API.Parser.Syntax.Declarations.Components
{
    /// <summary>
    /// Represents a declaration component, which forms a <see cref="DeclarationBase"/>.
    /// </summary>
    public abstract class ComponentBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="ComponentBase"/> class.
        /// </summary>
        protected ComponentBase()
            : this(new Queue<Token>())
        { }

        /// <summary>
        /// Initializes an instance of the <see cref="ComponentBase"/> class.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        protected ComponentBase(Queue<Token> tokens)
        {
            Tokens = tokens;
        }

        /// <summary>
        /// Gets the tokens of the <see cref="ComponentBase"/>.
        /// </summary>
        public Queue<Token> Tokens { get; internal set; }
    }
}