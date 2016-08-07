namespace Narser.API.Parser.Syntax.Declarations.Components
{
    /// <summary>
    /// Represents a binary expression component.
    /// </summary>
    public class BinaryExpressionComponent : CompoundComponent
    {
        /// <summary>
        /// Initializes an instance of the <see cref="BinaryExpressionComponent"/> class.
        /// </summary>
        /// <param name="left">The left expression.</param>
        /// <param name="op">The operator.</param>
        /// <param name="right">The right expression.</param>
        public BinaryExpressionComponent(ComponentBase left, OperatorComponent op, ComponentBase right)
            : base(left, op, right)
        {
            Left = left;
            Right = right;
            Operator = op;
        }

        /// <summary>
        /// Gets the left-side component of the <see cref="BinaryExpressionComponent"/>.
        /// </summary>
        public ComponentBase Left { get; internal set; }

        /// <summary>
        /// Gets the right-side component of the <see cref="BinaryExpressionComponent"/>.
        /// </summary>
        public ComponentBase Right { get; internal set; }

        /// <summary>
        /// Gets the operator component of the <see cref="BinaryExpressionComponent"/>.
        /// </summary>
        public OperatorComponent Operator { get; internal set; }
    }
}