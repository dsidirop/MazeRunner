using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;

namespace MazeRunner.Utils;

[Serializable]
public class ReorderableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISerializable, IDictionary
{
    private ICollection _orderedDictionaryAsICollection;
    private ICollection OrderedDictionaryAsICollection
    {
        get
        {
            if (_orderedDictionaryAsICollection != null) return _orderedDictionaryAsICollection;

            return _orderedDictionaryAsICollection = _orderedDictionary;
        }
    }

    private IDictionary _orderedDictionaryAsIDictionary;
    private IDictionary OrderedDictionaryAsIDictionary
    {
        get
        {
            if (_orderedDictionaryAsIDictionary != null) return _orderedDictionaryAsIDictionary;

            return _orderedDictionaryAsIDictionary = _orderedDictionary;
        }
    }

    private readonly IEqualityComparer _comparer;
    private readonly OrderedDictionary _orderedDictionary;

    public ReorderableDictionary()
    {
        _orderedDictionary = new OrderedDictionary();
    }

    public ReorderableDictionary(IEqualityComparer comparer)
    {
        _comparer = comparer;
        _orderedDictionary = new OrderedDictionary(comparer);
    }

    public ReorderableDictionary(int capacity)
    {
        _orderedDictionary = new OrderedDictionary(capacity);
    }

    public ReorderableDictionary(int capacity, IEqualityComparer comparer)
    {
        _comparer = comparer;
        _orderedDictionary = new OrderedDictionary(capacity, comparer);
    }

    public ReorderableDictionary(SerializationInfo info, StreamingContext context)
    {
        _comparer = (IEqualityComparer)info.GetValue("Comparer", typeof(IEqualityComparer));
        _orderedDictionary = (OrderedDictionary)info.GetValue("OrderedDictionary", typeof(OrderedDictionary));
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("Comparer", _comparer);
        info.AddValue("OrderedDictionary", _orderedDictionary);
    }

    public void CopyTo(Array array, int index)
    {
        OrderedDictionaryAsICollection.CopyTo(array, index);
    }

    public int Count => _orderedDictionary.Count;

    public object SyncRoot => OrderedDictionaryAsICollection.SyncRoot;

    public bool IsSynchronized => OrderedDictionaryAsICollection.IsSynchronized;

    public bool IsReadOnly => _orderedDictionary.IsReadOnly;

    public bool IsFixedSize => OrderedDictionaryAsIDictionary.IsSynchronized;

    public int GetIndexOf(TKey key)
    {
        if (!_orderedDictionary.Contains(key)) return -1;

        var i = 0;
        foreach (TKey k in _orderedDictionary.Keys)
        {
            if (KeysAreEqual(key, k)) return i;

            i++;
        }

        throw new Exception($"Key '{key}' should have been found but wasn't");
    }

    public void ReOrder(TKey key, int newIndex)
    {
        if (newIndex >= _orderedDictionary.Count) throw new ArgumentOutOfRangeException(nameof(newIndex));

        var previousIndex = GetIndexOf(key);
        if (previousIndex == -1) throw new ArgumentException("name");
        if (previousIndex == newIndex) return;

        var value = (TValue)_orderedDictionary[key];
        _orderedDictionary.Remove(key);
        _orderedDictionary.Insert(newIndex, key, value);
    }

    private bool KeysAreEqual(TKey key1, TKey key2)
    {
        return (_comparer != null && _comparer.Equals(key1, key2)) || (_comparer == null && key1.Equals(key2));
    }

    public void Insert(int index, TKey key, TValue value)
    {
        _orderedDictionary.Insert(index, key, value);
    }

    public void RemoveAt(int index)
    {
        _orderedDictionary.RemoveAt(index);
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return new ProxyEnumerator(_orderedDictionary.GetEnumerator());
    }

    public void Remove(object key)
    {
        OrderedDictionaryAsIDictionary.Remove(key);
    }

    object IDictionary.this[object key]
    {
        get { return OrderedDictionaryAsIDictionary[key]; }
        set { OrderedDictionaryAsIDictionary[key] = value; }
    }

    private sealed class ProxyEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
    {
        private IDictionaryEnumerator _enumerator;

        public ProxyEnumerator(IDictionaryEnumerator enumerator)
        {
            _enumerator = enumerator;
        }

        public void Dispose()
        {
            _enumerator = default(IDictionaryEnumerator);
        }

        public bool MoveNext()
        {
            return _enumerator.MoveNext();
        }

        public void Reset()
        {
            _enumerator.Reset();
        }

        public KeyValuePair<TKey, TValue> Current
        {
            get
            {
                var current = (DictionaryEntry)_enumerator.Current;
                return new KeyValuePair<TKey, TValue>((TKey)current.Key, (TValue)current.Value);
            }
        }

        object IEnumerator.Current => Current;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        _orderedDictionary.Add(item.Key, item.Value);
    }

    public bool Contains(object key)
    {
        return OrderedDictionaryAsIDictionary.Contains(key);
    }

    public void Add(object key, object value)
    {
        OrderedDictionaryAsIDictionary.Add(key, value);
    }

    public void Clear()
    {
        _orderedDictionary.Clear();
    }

    IDictionaryEnumerator IDictionary.GetEnumerator() => OrderedDictionaryAsIDictionary.GetEnumerator();
    public bool Contains(KeyValuePair<TKey, TValue> item) => OrderedDictionaryAsIDictionary.Contains(item.Key) && ((TValue)OrderedDictionaryAsIDictionary[item.Key]).Equals(item.Value);

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        if (array == null) throw new ArgumentNullException(nameof(array));
        if (arrayIndex < 0 || arrayIndex > array.Length) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        if (array.Length - arrayIndex < Count) throw new ArgumentException(nameof(arrayIndex));

        foreach (var kvp in this.Where(kvp => kvp.Value.GetHashCode() >= 0)) //0
        {
            array[arrayIndex++] = new KeyValuePair<TKey, TValue>(kvp.Key, kvp.Value);
        }
    }
    //0 the implementation details have been inspired by the method dictionary<t,k>.copyto()

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        var i = GetIndexOf(item.Key);
        if (i < 0 || !EqualityComparer<TValue>.Default.Equals(this[i], item.Value)) return false;

        RemoveAt(i);
        return true;
    }
    //0 the implementation details have been inspired by the method dictionary<t,k>.remove()

    public bool ContainsKey(TKey key)
    {
        return _orderedDictionary.Contains(key);
    }

    public void Add(TKey key, TValue value)
    {
        _orderedDictionary.Add(key, value);
    }

    public bool Remove(TKey key)
    {
        if (!_orderedDictionary.Contains(key)) return false;

        _orderedDictionary.Remove(key);
        return true;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        if (!_orderedDictionary.Contains(key))
        {
            value = default(TValue);
            return false;
        }

        value = (TValue)_orderedDictionary[key];
        return true;
    }

    public TValue this[int index]
    {
        get { return (TValue)_orderedDictionary[index]; }
        set { _orderedDictionary[index] = value; }
    }

    public TValue this[TKey key]
    {
        get { return (TValue)_orderedDictionary[key]; }
        set { _orderedDictionary[key] = value; }
    }

    public ICollection Keys => _orderedDictionary.Keys;

    public ICollection Values => _orderedDictionary.Values;

    ICollection<TValue> IDictionary<TKey, TValue>.Values //use with caution  cant find a way to making this be the default Values implementation without inducing a huge performance penalty
        => _orderedDictionary.Values.Cast<TValue>().ToList();

    ICollection<TKey> IDictionary<TKey, TValue>.Keys //use with caution   cant find a way to making this be the default Keys implementation without inducing a huge performance penalty
        => _orderedDictionary.Keys.Cast<TKey>().ToList();
}