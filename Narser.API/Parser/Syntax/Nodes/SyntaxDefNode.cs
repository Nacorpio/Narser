using System.Collections.Generic;
using System.Linq;

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
        /// <param name="rules">The rules.</param>
        /// <param name="token">The initial token.</param>
        internal SyntaxDefNode(string name, IEnumerable<RuleDefNode> rules, Token token) : base(token)
        {
            Name = name;
            Rules = rules;

            foreach (var rule in Rules)
                rule.Parent = this;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="SyntaxDefNode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rules">The rules.</param>
        public SyntaxDefNode(string name, IEnumerable<RuleDefNode> rules) : this(name, rules, Token.Empty)
        { }

        /// <summary>
        /// Gets the name of the <see cref="SyntaxDefNode"/>.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the rules of the <see cref="SyntaxDefNode"/>.
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
