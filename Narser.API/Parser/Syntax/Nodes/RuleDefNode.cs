using Narser.API.Parser.Syntax.Declarations;

namespace Narser.API.Parser.Syntax.Nodes
{
    public class RuleDefNode : SyntaxNode
    {
        /// <summary>
        /// Initializes an instance of the <see cref="RuleDefNode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="decl">The declaration.</param>
        /// <param name="token">The initial token.</param>
        internal RuleDefNode(string name, RuleDefDeclaration decl, Token token) : base(token)
        {
            Name = name;
            Declaration = decl;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="RuleDefNode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="decl">The declaration.</param>
        public RuleDefNode(string name, RuleDefDeclaration decl) : this(name, decl, null)
        { }

        /// <summary>
        /// Gets the name of the <see cref="RuleDefNode"/>.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the declaration of the <see cref="RuleDefNode"/>.
        /// </summary>
        public RuleDefDeclaration Declaration { get; internal set; }
    }
}
