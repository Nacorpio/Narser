namespace Narser.API.Parser.Syntax.Nodes
{
    /// <summary>
    /// Represents a syntax definition node.
    /// </summary>
    public sealed class SyntaxDefNode : SyntaxNode
    {
        /// <summary>
        /// Initializes an instance of the <see cref="SyntaxDefNode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="token">The initial token.</param>
        internal SyntaxDefNode(string name, Token token) : base(token)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="SyntaxDefNode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public SyntaxDefNode(string name) : this(name, Token.Empty)
        { }

        /// <summary>
        /// Gets the name of the <see cref="SyntaxDefNode"/>.
        /// </summary>
        public string Name { get; internal set; }
    }
}
