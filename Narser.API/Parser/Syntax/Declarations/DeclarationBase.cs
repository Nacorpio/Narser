using System.Collections.Generic;
using System.Collections.ObjectModel;
using Narser.API.Parser.Syntax.Declarations.Components;

namespace Narser.API.Parser.Syntax.Declarations
{
    /// <summary>
    /// Represents the base of any declaration.
    /// </summary>
    public abstract class DeclarationBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DeclarationBase"/> class.
        /// </summary>
        /// <param name="tokens">The underlying tokens.</param>
        internal DeclarationBase(Queue<Token> tokens)
        {
            Tokens = tokens;
        }

        /// <summary>
        /// Gets the tokens of the <see cref="DeclarationBase"/>.
        /// </summary>
        public Queue<Token> Tokens { get; internal set; }

        /// <summary>
        /// Returns the components of the <see cref="DeclarationBase"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ComponentBase> GetComponents()
        {
            var results = new Collection<ComponentBase>();

            while (Tokens.Count > 0)
            {
                
            }

            return results;
        }
    }
}
