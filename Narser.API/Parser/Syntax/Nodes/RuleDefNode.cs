using System.Collections.Generic;

namespace Narser.API.Parser.Syntax.Nodes
{
    public class RuleDefNode : SyntaxNode
    {
        private readonly IEnumerable<Token> _declaration;

        /// <summary>
        /// Initializes an instance of the <see cref="RuleDefNode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="decl">The declaration.</param>
        /// <param name="token">The initial token.</param>
        internal RuleDefNode(string name, IEnumerable<Token> decl, Token token) : base(token)
        {
            Name = name;
            _declaration = decl;
        }

        /// <summary>
        /// Gets the name of the <see cref="RuleDefNode"/>.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the declaration of the <see cref="RuleDefNode"/>.
        /// </summary>
        public IEnumerable<Token> GetDeclaration()
        {
            return _declaration;
        }
    }
}
