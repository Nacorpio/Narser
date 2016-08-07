using System.Collections.Generic;
using Narser.API.Parser.Extensions;
using Narser.API.Parser.Syntax.Declarations.Components;

namespace Narser.API.Parser.Syntax.Declarations
{
    /// <summary>
    /// Represents the declaration of a <see cref="Nodes.RuleDefNode"/>.
    /// </summary>
    public class RuleDefDeclaration : DeclarationBase
    {
        private readonly IEnumerable<ComponentBase> _components;

        /// <summary>
        /// Initializes an instance of the <see cref="RuleDefDeclaration"/> class.
        /// </summary>
        /// <param name="components">The components.</param>
        internal RuleDefDeclaration(IEnumerable<ComponentBase> components)
        {
            _components = components;
        }

        /// <summary>
        /// Returns the <see cref="RuleDefDeclaration"/> as a binary expression.
        /// </summary>
        /// <returns></returns>
        public BinaryExpressionComponent AsBinaryExpression()
        {
            BinaryExpressionComponent expression;

            return !new Queue<ComponentBase>(GetComponents()).Parse(out expression) ?
                null : expression;
        }

        /// <summary>
        /// Returns the components of the <see cref="RuleDefDeclaration"/>.
        /// </summary>
        public IEnumerable<ComponentBase> GetComponents()
        {
            return _components;
        }
    }
}