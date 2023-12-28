using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace AocHelper.DataStructures
{
    public class BoolArray
    {
        private const int BITS_IN_BYTE = 8;

        private readonly uint _size;
        private readonly byte[] _values;

        public BoolArray(uint size)
        {
            int arrSize = (int)Math.Ceiling((float)size / BITS_IN_BYTE);
            _size = size;
            _values = new byte[arrSize];
        }

        public BoolArray(BoolArray b)
        {
            _size = b._size;
            _values = b._values;
        }

        public bool this[int index]
        {
            get => GetValue(index);
            set => SetValue(index, value);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new();
            for (int i = 0; i < _size; i++)
                sb.Append(this[i] ? '1' : '0');

            return sb.ToString();
        }

        /// <summary>
        /// Gets the value at the given index
        /// </summary>
        /// <param name="index">The index of the parameter</param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public bool GetValue(int index)
        {
            if (index >= _size || index < 0)
                throw new IndexOutOfRangeException();

            int bucket = index / BITS_IN_BYTE;
            int bucketIndex = index % BITS_IN_BYTE;

            return (_values[bucket] & 1u << bucketIndex) != 0;
        }

        /// <summary>
        /// Sets the value at the given index
        /// </summary>
        /// <param name="index">The index to set</param>
        /// <param name="value">The boolean param</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void SetValue(int index, bool value)
        {
            if (index >= _size || index < 0)
                throw new IndexOutOfRangeException();

            int bucket = index / BITS_IN_BYTE;
            int bucketIndex = index % BITS_IN_BYTE;

            if (value)
            {
                _values[bucket] |= (byte)(1u << bucketIndex);
            }
            else
            {
                _values[bucket] &= (byte)~(1u << bucketIndex);
            }
        }
    }
}
