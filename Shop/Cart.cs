using System;
using System.Collections.Generic;
using System.Text;

namespace Code.napilnik.Shop
{
    public struct Order
    {
        public Order(string paylink)
        {
            Paylink = paylink;
        }

        public string Paylink { get; }
    }
    
    public class Cart
    {
        private readonly IGoodsProvider _goodsProvider;
        private readonly Dictionary<Good, Amount> _orderedGoods;

        public Cart(IGoodsProvider goodsProvider)
        {
            if (goodsProvider == null)
                throw new ArgumentNullException(nameof(goodsProvider));
            
            _goodsProvider = goodsProvider;
            _orderedGoods = new Dictionary<Good, Amount>();
        }

        public void Add(Good good, Amount amount)
        {
            if (good == null)
                throw new ArgumentNullException(nameof(good));
            
            if (_goodsProvider.HasGoods(good, amount) == false)
                throw new InvalidOperationException();
            
            if (_orderedGoods.ContainsKey(good))
            {
                _orderedGoods[good] += amount;
                return;
            }

            _orderedGoods.Add(good, amount);
        }

        public Order Order()
        {
            StringBuilder paylinkBuilder = new StringBuilder();

            foreach (var good in _orderedGoods.Keys)
            {
                paylinkBuilder.Append(good.Name);
                paylinkBuilder.Append(", ");
            }

            return new Order(paylinkBuilder.ToString());
        }
    }
}