using System.Collections.Generic;
using System.Linq;
using Narser.API.Parser.Extensions;
using Narser.API.Parser.Syntax.Declarations.Components;
using Narser.API.Parser.Syntax.Declarations.Components.Compounds;

namespace Narser.API.Parser.Syntax.Declarations
{
    /// <summary>
    /// Represents the base of any declaration.
    /// </summary>
    public abstract class DeclarationBase
    {
        private readonly IEnumerable<ComponentBase> _components;

        /// <summary>
        /// Initializes an instance of the <see cref="DeclarationBase"/> class.
        /// </summary>
        /// <param name="components">The child components.</param>
        internal DeclarationBase(IEnumerable<ComponentBase> components)
        {
            _components = components;
        }

        /// <summary>
        /// Returns the components of the <see cref="RuleDefDeclaration"/>.
        /// </summary>
        public IEnumerable<ComponentBase> GetComponents()
        {
            return _components;
        }

        /// <summary>
        /// Determines whether the <see cref="DeclarationBase"/> is a binary component.
        /// </summary>
        /// <returns></returns>
        public bool IsBinaryComponent()
        {
            var array = _components.ToArray();
            return array.Length == 3 && array[1] is OperatorComponent;
        }

        /// <summary>
        /// Returns the <see cref="RuleDefDeclaration"/> as a binary component.
        /// </summary>
        /// <returns></returns>
        public BinaryComponent AsBinaryComponent()
        {
            BinaryComponent expression;

            return !new Queue<ComponentBase>(GetComponents()).Parse(out expression) ?
                null : expression;
        }
    }
}
