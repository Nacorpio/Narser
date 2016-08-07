using System.Collections.Generic;
using System.Linq;

namespace Narser.API.Parser.Syntax.Declarations.Components.Compounds
{
    /// <summary>
    /// Represents a component that contains child components.
    /// </summary>
    public abstract class CompoundComponent : ComponentBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="CompoundComponent"/> class.
        /// </summary>
        /// <param name="children">The child components.</param>
        protected CompoundComponent(params ComponentBase[] children)
        {
            Children = children;
        }

        /// <summary>
        /// Gets the child components of the <see cref="CompoundComponent"/>.
        /// </summary>
        public IEnumerable<ComponentBase> Children { get; internal set; }

        /// <summary>
        /// Gets a child at a specific index in the <see cref="CompoundComponent"/>.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns></returns>
        public ComponentBase this[int index] => Children.ToArray()[index];
    }
}