namespace Narser.API.Parser.Syntax.Declarations.Components.Compounds
{
    /// <summary>
    /// Represents a component with two components and one operator.
    /// </summary>
    public class BinaryComponent : CompoundComponent
    {
        /// <summary>
        /// Initializes an instance of the <see cref="BinaryComponent"/> class.
        /// </summary>
        /// <param name="left">The left expression.</param>
        /// <param name="op">The operator.</param>
        /// <param name="right">The right expression.</param>
        public BinaryComponent(ComponentBase left, OperatorComponent op, ComponentBase right)
            : base(left, op, right)
        {
            Left = left;
            Right = right;
            Operator = op;
        }

        /// <summary>
        /// Gets the left-side component of the <see cref="BinaryComponent"/>.
        /// </summary>
        public ComponentBase Left { get; internal set; }

        /// <summary>
        /// Gets the right-side component of the <see cref="BinaryComponent"/>.
        /// </summary>
        public ComponentBase Right { get; internal set; }

        /// <summary>
        /// Gets the operator component of the <see cref="BinaryComponent"/>.
        /// </summary>
        public OperatorComponent Operator { get; internal set; }
    }
}