using Narser.API.Parser.Utilities;

namespace Narser.API.Parser.Syntax.Nodes
{
    /// <summary>
    /// Represents a node that forms a syntax tree.
    /// </summary>
    public abstract class SyntaxNode
    {
        /// <summary>
        /// Initializes an instance of the <see cref="SyntaxNode"/> class.
        /// </summary>
        protected SyntaxNode() : this(Token.Empty)
        { }

        /// <summary>
        /// Initializes an instance of the <see cref="SyntaxNode"/> class.
        /// </summary>
        /// <param name="token">The initial token.</param>
        protected SyntaxNode(Token token) : this(token, null)
        { }

        /// <summary>
        /// Initializes an instance of the <see cref="SyntaxNode"/> class.
        /// </summary>
        /// <param name="token">The initial token.</param>
        /// <param name="parent">The parental node.</param>
        protected SyntaxNode(Token token, SyntaxNode parent)
        {
            Token = token;
            Parent = parent;
        }

        /// <summary>
        /// Gets the initial token of which the <see cref="SyntaxNode"/> begins at.
        /// </summary>
        public Token Token { get; internal set; }

        /// <summary>
        /// Gets the parental node of the <see cref="SyntaxNode"/>.
        /// </summary>
        public SyntaxNode Parent { get; internal set; }

        /// <summary>
        /// Gets the start location of the <see cref="SyntaxNode"/>.
        /// </summary>
        public StringLocation Start => Token.Start;
    }
}
