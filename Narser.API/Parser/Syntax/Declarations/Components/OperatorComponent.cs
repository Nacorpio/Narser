namespace Narser.API.Parser.Syntax.Declarations.Components
{
    /// <summary>
    /// Represents a type of <see cref="OperatorComponent"/>.
    /// </summary>
    public enum OperatorType
    {
        BitwiseAnd,
        BitwiseOr
    }

    /// <summary>
    /// Represents an operator component.
    /// </summary>
    public class OperatorComponent : ComponentBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="OperatorComponent"/> class.
        /// </summary>
        /// <param name="op">An operator token kind.</param>
        internal OperatorComponent(OperatorType op)
        {
            Operator = op;
        }

        /// <summary>
        /// Gets the operator kind of the <see cref="OperatorComponent"/>.
        /// </summary>
        public OperatorType Operator { get; internal set; }
    }
}
