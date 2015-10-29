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

        /// <summary>
        /// Creates an instance of <see cref="AstNode{TTag}"/>.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="text">The text assiciated with the node.</param>
        /// <param name="children">The children nodes.</param>
        public AstNode(TTag tag, string text, params AstNode<TTag>[] children)
        {
            Tag = tag;
            Text = text;
            Children = children;
        }

        /// <summary>
        /// Creates an instance of <see cref="AstNode{TTag}"/>.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="text">The text assiciated with the node.</param>
        /// <param name="children">The children nodes.</param>
        public AstNode(TTag tag, string text, IEnumerable<AstNode<TTag>> children)
        {
            Tag = tag;
            Text = text;
            Children = (children ?? new AstNode<TTag>[0]).ToList();
        }

        /// <summary>
        /// Creates an instance of <see cref="AstNode{TTag}"/>.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="children">The children nodes.</param>
        public AstNode(TTag tag, params AstNode<TTag>[] children)
            : this(tag, null, children)
        { }

        /// <summary>
        /// Creates an instance of <see cref="AstNode{TTag}"/>.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="children">The children nodes.</param>
        public AstNode(TTag tag, IEnumerable<AstNode<TTag>> children)
            : this(tag, null, children)
        { }

        /// <summary>
        /// Creates an instance of <see cref="AstNode{TTag}"/>.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public AstNode(TTag tag)
            : this(tag, (string)null)
        { }
    }
}
