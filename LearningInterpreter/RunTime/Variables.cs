namespace LearningInterpreter.RunTime
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Implements the dictionary of custom variables plus predefined variables line <see cref="Random"/>.
    /// </summary>
    public class Variables : IDictionary<string, object>
    {
        private readonly IDictionary<string, object> variables;

        /// <summary>
        /// Gets or sets the <see cref="System.Random"/> object that is using inside <c>RND</c> BASIC function.
        /// </summary>
        public Random Random { get; set; }

        /// <summary>
        /// Gets or sets the last used name of the program.
        /// </summary>
        /// <remarks><c>null</c>, if there was not save and load operations.</remarks>
        public string LastUsedProgramName { get; set; }

        /// <inheritdoc />
        public object this[string key]
        {
            get { return variables[key]; }
            set { variables[key] = value; }
        }

        /// <inheritdoc />
        public int Count
        {
            get { return variables.Count; }
        }

        /// <inheritdoc />
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <inheritdoc />
        public ICollection<string> Keys
        {
            get { return variables.Keys; }
        }

        /// <inheritdoc />
        public ICollection<object> Values
        {
            get { return variables.Values; }
        }

        /// <summary>
        /// Initializes a nes instance of the <see cref="Variables"/> class.
        /// </summary>
        public Variables()
        {
            variables = new Dictionary<string, dynamic>();

            Random = new Random();
        }

        /// <inheritdoc />
        public void Add(KeyValuePair<string, object> item)
        {
            variables.Add(item);
        }

        /// <inheritdoc />
        public void Add(string key, object value)
        {
            variables.Add(key, value);
        }

        /// <inheritdoc />
        public void Clear()
        {
            variables.Clear();
        }

        /// <inheritdoc />
        public bool Contains(KeyValuePair<string, object> item)
        {
            return variables.Contains(item);
        }

        /// <inheritdoc />
        public bool ContainsKey(string key)
        {
            return variables.ContainsKey(key);
        }

        /// <inheritdoc />
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            variables.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return variables.GetEnumerator();
        }

        /// <inheritdoc />
        public bool Remove(KeyValuePair<string, object> item)
        {
            return variables.Remove(item);
        }

        /// <inheritdoc />
        public bool Remove(string key)
        {
            return variables.Remove(key);
        }

        /// <inheritdoc />
        public bool TryGetValue(string key, out object value)
        {
            return variables.TryGetValue(key, out value);
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return variables.GetEnumerator();
        }
    }
}
