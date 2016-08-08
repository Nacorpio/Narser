using System.Collections.Generic;
using Narser.API.Parser.Syntax.Declarations.Components;

namespace Narser.API.Parser.Syntax.Declarations
{
    /// <summary>
    /// Represents the declaration of a <see cref="Nodes.RuleDefNode"/>.
    /// </summary>
    public class RuleDefDeclaration : DeclarationBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="RuleDefDeclaration"/> class.
        /// </summary>
        /// <param name="components">The components.</param>
        internal RuleDefDeclaration(IEnumerable<ComponentBase> components) : base(components)
        { }
    }
}