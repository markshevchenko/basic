namespace LearningBasic.Parsing.Code
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents integer range with minimal and maximal values.
    /// </summary>
    public struct Range : IEquatable<Range>
    {
        private readonly bool isDefined;
        private readonly int min;
        private readonly int max;

        /// <summary>
        /// The empty range.
        /// </summary>
        public static readonly Range Undefined = new Range();

        /// <summary>
        /// Indicates that the range is empty.
        /// </summary>
        public bool IsDefined { get { return isDefined; } }

        /// <summary>
        /// Gets the minimal value of the range.
        /// </summary>
        /// <exception cref="InvalidOperationException">The range is empty.</exception>
        public int Min
        {
            get
            {
                if (isDefined)
                    return min;

                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Gets the maximal value of the range.
        /// </summary>
        /// <exception cref="InvalidOperationException">The range is empty.</exception>
        public int Max
        {
            get
            {
                if (isDefined)
                    return max;

                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Creates an instance of <see cref="Range"/> with the same bounds.
        /// </summary>
        /// <param name="value"></param>
        public Range(int value)
            : this()
        {
            min = max = value;
            isDefined = true;
        }

        /// <summary>
        /// Creates an instance of <see cref="Range"/> with <paramref name="min"/>
        /// and <paramref name="max"/> bounds.
        /// </summary>
        /// <param name="min">The min bound of the range.</param>
        /// <param name="max">The max bound of the range.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="min"/> is greater than <paramref name="max"/>.
        /// </exception>
        public Range(int min, int max)
            : this()
        {
            if (min > max)
                throw new ArgumentException();

            this.min = min;
            this.max = max;
            this.isDefined = true;
        }

        /// <summary>
        /// Determines whether the <see cref="Range"/> contains a specific value.
        /// </summary>
        /// <remarks>An empty <Ran</remarks>
        /// <param name="value">The value to locate in the <see cref="Range"/>.</param>
        /// <returns><c>true</c> if the <see cref="Range"/> contains a specific value;
        /// otherwise, <c>false</c>.</returns>
        public bool Contains(int value)
        {
            return !IsDefined || (value >= Min && value <= Max);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (!IsDefined)
                return string.Empty;

            if (Min == Max)
                return Min.ToString(CultureInfo.InvariantCulture);

            return string.Format(CultureInfo.InvariantCulture, "{0}-{1}", Min, Max);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is Range)
                return Equals((Range)obj);

            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return min.GetHashCode() ^ max.GetHashCode();
        }

        /// <inheritdoc />
        public bool Equals(Range other)
        {
            return (!isDefined && !other.isDefined)
                || (isDefined == other.isDefined && min == other.min && max == other.max);
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="Range"/> are equal.
        /// </summary>
        /// <param name="a">The first object to compare.</param>
        /// <param name="b">The second object to compare.</param>
        /// <returns>
        /// <c>true</c>, if <paramref name="a"/> and <paramref name="b"/>
        /// represents the same range; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(Range a, Range b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="Range"/> are not equal.
        /// </summary>
        /// <param name="a">The first object to compare.</param>
        /// <param name="b">The second object to compare.</param>
        /// <returns>
        /// <c>true</c>, if <paramref name="a"/> and <paramref name="b"/>
        /// do not represents the same range; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(Range a, Range b)
        {
            return !a.Equals(b);
        }
    }
}
