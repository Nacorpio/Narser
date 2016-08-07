namespace Narser.API.Parser.Syntax.Nodes
{
    public class RuleDefNode : SyntaxNode
    {
        /// <summary>
        /// Initializes an instance of the <see cref="RuleDefNode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="token">The initial token.</param>
        internal RuleDefNode(string name, Token token) : base(token)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="RuleDefNode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public RuleDefNode(string name) : this(name, null)
        { }

        /// <summary>
        /// Gets the name of the <see cref="RuleDefNode"/>.
        /// </summary>
        public string Name { get; internal set; }
    }
}
