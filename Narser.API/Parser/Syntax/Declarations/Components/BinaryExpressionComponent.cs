namespace Narser.API.Parser.Syntax.Declarations.Components
{
    /// <summary>
    /// Represents a binary expression component.
    /// </summary>
    public class BinaryExpressionComponent : ComponentBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="BinaryExpressionComponent"/> class.
        /// </summary>
        /// <param name="left">The left expression.</param>
        /// <param name="type">The operator type.</param>
        /// <param name="right">The right expression.</param>
        public BinaryExpressionComponent(ComponentBase left, OperatorType type, ComponentBase right)
        {
            Left = left;
            Right = right;

            Type = type;
        }

        /// <summary>
        /// Gets the left side of the <see cref="BinaryExpressionComponent"/>.
        /// </summary>
        public ComponentBase Left { get; internal set; }

        /// <summary>
        /// Gets the right side of the <see cref="BinaryExpressionComponent"/>.
        /// </summary>
        public ComponentBase Right { get; internal set; }

        /// <summary>
        /// Gets the type of the <see cref="BinaryExpressionComponent"/>.
        /// </summary>
        public OperatorType Type { get; internal set; }
    }
}