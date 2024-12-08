namespace AocHelper.DataStructures;

public class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TKey: notnull
                                                                        where TValue: new()
{
    public new TValue this[TKey key]
    {
        get
        {
            if (!TryGetValue(key, out TValue val))
            {
                val = new TValue();
                Add(key, val);
            }
            return val;
        }
        set => base[key] = value;
    }
}