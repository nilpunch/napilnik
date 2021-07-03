using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Napilnik.Shop
{
    public class Warehouse : IGoodsProvider, IEnumerable<Warehouse.StoredGood>
    {
        public struct StoredGood
        {
            public StoredGood(Good good, Amount amount)
            {
                if (good == null)
                    throw new ArgumentNullException(nameof(good));
                
                Good = good;
                Amount = amount;
            }

            public Good Good { get; }
            public Amount Amount { get; }
        }

        private readonly Dictionary<Good, Amount> _storedGoods;

        public Warehouse()
        {
            _storedGoods = new Dictionary<Good, Amount>();
        }

        public void Delive(Good good, Amount amount)
        {
            if (good == null)
                throw new ArgumentNullException(nameof(good));
            
            if (_storedGoods.ContainsKey(good))
            {
                _storedGoods[good] += amount;
                return;
            }

            _storedGoods.Add(good, amount);
        }

        public bool HasGoods(Good good, Amount amount)
        {
            if (good == null)
                throw new ArgumentNullException(nameof(good));
            
            if (_storedGoods.TryGetValue(good, out Amount storedAmount))
            {
                return storedAmount.Value >= amount.Value;
            }

            return false;
        }

        public IEnumerator<StoredGood> GetEnumerator()
        {
            foreach (var goodAmountPair in _storedGoods)
            {
                yield return new StoredGood(goodAmountPair.Key, goodAmountPair.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return GetEnumerator();
        }
    }
}