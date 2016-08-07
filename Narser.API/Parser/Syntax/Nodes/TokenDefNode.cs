using System.Collections.Generic;
using System.Linq;

namespace Narser.API.Parser.Syntax.Nodes
{
    /// <summary>
    /// Represents a token definition node.
    /// </summary>
    public sealed class TokenDefNode : SyntaxNode
    {
        /// <summary>
        /// Initializes an instance of the <see cref="TokenDefNode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rules">The rules.</param>
        /// <param name="token">The initial token.</param>
        internal TokenDefNode(string name, IEnumerable<RuleDefNode> rules, Token token) : base(token)
        {
            Name = name;
            Rules = rules;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="TokenDefNode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rules">The rules.</param>
        public TokenDefNode(string name, IEnumerable<RuleDefNode> rules) : this(name, rules, null)
        { }

        /// <summary>
        /// Gets the name of the <see cref="TokenDefNode"/>.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the inheritance of the <see cref="TokenDefNode"/>.
        /// </summary>
        public string Inheritance { get; internal set; }

        /// <summary>
        /// Gets the rules of the <see cref="TokenDefNode"/>.
        /// </summary>
        public IEnumerable<RuleDefNode> Rules { get; internal set; }

        /// <summary>
        /// Gets a rule definition with a specific name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public RuleDefNode this[string name] => Rules.FirstOrDefault(x => x.Name == name);
    }
}