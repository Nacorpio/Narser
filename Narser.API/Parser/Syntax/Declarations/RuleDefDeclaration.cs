using System.Collections.Generic;

namespace Narser.API.Parser.Syntax.Declarations
{
    public class RuleDefDeclaration : DeclarationBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="RuleDefDeclaration"/> class.
        /// </summary>
        /// <param name="tokens">The underlying tokens.</param>
        internal RuleDefDeclaration(Queue<Token> tokens) : base(tokens)
        {
            Parse();
        }

        /// <summary>
        /// Parses the declaration tokens to form a collection of declaration components.
        /// </summary>
        private void Parse()
        {
        }
    }
}