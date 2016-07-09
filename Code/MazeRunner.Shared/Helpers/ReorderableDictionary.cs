using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;

namespace MazeRunner.Shared.Helpers
{
    [Serializable]
    public class ReorderableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISerializable, IDictionary
    {
        private ICollection _OrderedDictionaryAsICollection;
        private ICollection OrderedDictionaryAsICollection
        {
            get
            {
                if (_OrderedDictionaryAsICollection != null) return _OrderedDictionaryAsICollection;

                return _OrderedDictionaryAsICollection = _OrderedDictionary;
            }
        }

        private IDictionary _OrderedDictionaryAsIDictionary;
        private IDictionary OrderedDictionaryAsIDictionary
        {
            get
            {
                if (_OrderedDictionaryAsIDictionary != null) return _OrderedDictionaryAsIDictionary;

                return _OrderedDictionaryAsIDictionary = _OrderedDictionary;
            }
        }

        private readonly IEqualityComparer _Comparer;
        private readonly OrderedDictionary _OrderedDictionary;

        public ReorderableDictionary()
        {
            _OrderedDictionary = new OrderedDictionary();
        }

        public ReorderableDictionary(IEqualityComparer comparer)
        {
            _Comparer = comparer;
            _OrderedDictionary = new OrderedDictionary(comparer);
        }

        public ReorderableDictionary(int capacity)
        {
            _OrderedDictionary = new OrderedDictionary(capacity);
        }

        public ReorderableDictionary(int capacity, IEqualityComparer comparer)
        {
            _Comparer = comparer;
            _OrderedDictionary = new OrderedDictionary(capacity, comparer);
        }

        public ReorderableDictionary(SerializationInfo info, StreamingContext context)
        {
            _Comparer = (IEqualityComparer)info.GetValue("Comparer", typeof(IEqualityComparer));
            _OrderedDictionary = (OrderedDictionary)info.GetValue("OrderedDictionary", typeof(OrderedDictionary));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Comparer", _Comparer);
            info.AddValue("OrderedDictionary", _OrderedDictionary);
        }

        public void CopyTo(Array array, int index)
        {
            OrderedDictionaryAsICollection.CopyTo(array, index);
        }

        public int Count => _OrderedDictionary.Count;

        public object SyncRoot => OrderedDictionaryAsICollection.SyncRoot;

        public bool IsSynchronized => OrderedDictionaryAsICollection.IsSynchronized;

        public bool IsReadOnly => _OrderedDictionary.IsReadOnly;

        public bool IsFixedSize => OrderedDictionaryAsIDictionary.IsSynchronized;

        public int GetIndexOf(TKey key)
        {
            if (!_OrderedDictionary.Contains(key)) return -1;

            var i = 0;
            foreach (TKey k in _OrderedDictionary.Keys)
            {
                if (KeysAreEqual(key, k)) return i;

                i++;
            }

            throw new Exception($"Key '{key}' should have been found but wasn't");
        }

        public void ReOrder(TKey key, int newIndex)
        {
            if (newIndex >= _OrderedDictionary.Count) throw new ArgumentOutOfRangeException(nameof(newIndex));

            var previousIndex = GetIndexOf(key);
            if (previousIndex == -1) throw new ArgumentException("name");
            if (previousIndex == newIndex) return;

            var value = (TValue)_OrderedDictionary[key];
            _OrderedDictionary.Remove(key);
            _OrderedDictionary.Insert(newIndex, key, value);
        }

        private bool KeysAreEqual(TKey key1, TKey key2)
        {
            return (_Comparer != null && _Comparer.Equals(key1, key2)) || (_Comparer == null && key1.Equals(key2));
        }

        public void Insert(int index, TKey key, TValue value)
        {
            _OrderedDictionary.Insert(index, key, value);
        }

        public void RemoveAt(int index)
        {
            _OrderedDictionary.RemoveAt(index);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new ProxyEnumerator(_OrderedDictionary.GetEnumerator());
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
            private IDictionaryEnumerator _Enumerator;

            public ProxyEnumerator(IDictionaryEnumerator enumerator)
            {
                _Enumerator = enumerator;
            }

            public void Dispose()
            {
                _Enumerator = default(IDictionaryEnumerator);
            }

            public bool MoveNext()
            {
                return _Enumerator.MoveNext();
            }

            public void Reset()
            {
                _Enumerator.Reset();
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    var current = (DictionaryEntry)_Enumerator.Current;
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
            _OrderedDictionary.Add(item.Key, item.Value);
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
            _OrderedDictionary.Clear();
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return OrderedDictionaryAsIDictionary.GetEnumerator();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return OrderedDictionaryAsIDictionary.Contains(item);
        }

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
            return _OrderedDictionary.Contains(key);
        }

        public void Add(TKey key, TValue value)
        {
            _OrderedDictionary.Add(key, value);
        }

        public bool Remove(TKey key)
        {
            if (!_OrderedDictionary.Contains(key)) return false;

            _OrderedDictionary.Remove(key);
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (!_OrderedDictionary.Contains(key))
            {
                value = default(TValue);
                return false;
            }

            value = (TValue)_OrderedDictionary[key];
            return true;
        }

        public TValue this[int index]
        {
            get { return (TValue)_OrderedDictionary[index]; }
            set { _OrderedDictionary[index] = value; }
        }

        public TValue this[TKey key]
        {
            get { return (TValue)_OrderedDictionary[key]; }
            set { _OrderedDictionary[key] = value; }
        }

        public ICollection Keys => _OrderedDictionary.Keys;

        public ICollection Values => _OrderedDictionary.Values;

        ICollection<TValue> IDictionary<TKey, TValue>.Values //use with caution  cant find a way to making this be the default Values implementation without inducing a huge performance penalty
            => _OrderedDictionary.Values.Cast<TValue>().ToList();

        ICollection<TKey> IDictionary<TKey, TValue>.Keys //use with caution   cant find a way to making this be the default Keys implementation without inducing a huge performance penalty
            => _OrderedDictionary.Keys.Cast<TKey>().ToList();
    }
}