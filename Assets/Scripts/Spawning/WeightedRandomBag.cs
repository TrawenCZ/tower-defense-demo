using System;
using System.Collections.Generic;

namespace Utils
{
    public class WeightedRandomBag<T>
    {
        public struct Entry
        {
            public float AccumulatedWeight;
            public float Weight;
            public T Item;
        }

        public int Count
        {
            get { return _entries.Count; }
        }

        private readonly List<Entry> _entries = new List<Entry>();
        private float _accumulatedWeight;

        public void Clear()
        {
            _entries.Clear();
            _accumulatedWeight = 0;
        }

        public void AddEntry(T item, float weight)
        {
            _accumulatedWeight += weight;
            _entries.Add(new Entry
            {
                Item = item,
                AccumulatedWeight = _accumulatedWeight,
                Weight = weight
            });
        }

        public void RemoveEntry(Entry entry)
        {
            _entries.Remove(entry);
            _accumulatedWeight = 0;
            for (var i = 0; i < _entries.Count; i++)
            {
                var entryItem = _entries[i];
                _accumulatedWeight += entryItem.Weight;
                entryItem.AccumulatedWeight = _accumulatedWeight;
                _entries[i] = entryItem;
            }
        }
        
        public T GetRandom()
        {
            return GetRandomEntry().Item;
        }

        public Entry GetRandomEntry()
        {
            var r = UnityEngine.Random.value * _accumulatedWeight;
            foreach (var entry in _entries)
            {
                if (entry.AccumulatedWeight >= r)
                {
                    return entry;
                }
            }
            return default; //should only happen when there are no entries
        }
        
        public T GetRandomAndRemove()
        {
            var r = UnityEngine.Random.value * _accumulatedWeight;
            foreach (var entry in _entries)
            {
                if (entry.AccumulatedWeight >= r)
                {
                    var a = entry.Item;
                    RemoveEntry(entry);
                    return a;
                }
            }
            return default; //should only happen when there are no entries
        }
    }
}