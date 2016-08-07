using System.Collections.Generic;
using Narser.API.Parser.Syntax.Declarations.Components;

namespace Narser.API.Parser.Syntax.Declarations
{
    public class RuleDefDeclaration : DeclarationBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="RuleDefDeclaration"/> class.
        /// </summary>
        /// <param name="components">The components.</param>
        internal RuleDefDeclaration(IEnumerable<ComponentBase> components)
        {
            Components = components;
        }

        /// <summary>
        /// Gets the components of the <see cref="RuleDefDeclaration"/>.
        /// </summary>
        public IEnumerable<ComponentBase> Components { get; internal set; }
    }
}