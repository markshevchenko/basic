namespace LearningBasic
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the node of the Abstract Syntax Tree.
    /// </summary>
    /// <typeparam name="TTag">The type of node's tag.</typeparam>
    public class AstNode<TTag>
        where TTag : struct
    {
        /// <summary>
        /// The tag of the node.
        /// </summary>
        public TTag Tag { get; private set; }

        /// <summary>
        /// The text of the node.
        /// </summary>
        /// <remarks>
        /// Optional property, can be <c>null</c>.
        /// </remarks>
        public string Text { get; private set; }

        /// <summary>
        /// The list of child nodes.
        /// </summary>
        public IReadOnlyList<AstNode<TTag>> Children { get; private set; }

        public AstNode(TTag tag, string text, params AstNode<TTag>[] children)
        {
            Tag = tag;
            Text = text;
            Children = children;
        }

        public AstNode(TTag tag, string text, IEnumerable<AstNode<TTag>> children)
        {
            Tag = tag;
            Text = text;
            Children = (children ?? new AstNode<TTag>[0]).ToList();
        }

        public AstNode(TTag tag, params AstNode<TTag>[] children)
            : this(tag, null, children)
        { }

        public AstNode(TTag tag, IEnumerable<AstNode<TTag>> children)
            : this(tag, null, children)
        { }

        public AstNode(TTag tag)
            : this(tag, (string)null)
        { }
    }
}
