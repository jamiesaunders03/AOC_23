using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AocHelper.Utilities
{
    public class HashContainer<T> : IReadOnlyCollection<T>
    {
        private readonly T[] _values;
        private int? _hashCode;

        public HashContainer(IEnumerable<T> values)
        {
            _values = values.ToArray();
        }

        public HashContainer(T[] values)
        {
            _values = values;
        }

        /// <summary>
        /// Gets the number of elements in the collection.
        /// </summary>
        /// <returns>The number of elements in the collection.</returns>
        public int Count => _values.Length;

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            _hashCode ??= _values.GetHashCode();
            return (int)_hashCode;
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
        public override bool Equals(object? obj)
        {
            T[] other;
            switch (obj)
            {
                case null:
                    return false;
                case T[] arr:
                    other = arr;
                    break;
                case HashContainer<T> container:
                    other = container._values;
                    break;
                default:
                    return false;
            }

            if (other.Length != _values.Length)
                return false;
            if (other.GetHashCode() != GetHashCode())
                return false;

            return !_values.Where((t, i) => !t.Equals(other[i])).Any();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _values.Cast<T>().GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
