using System;

namespace Napilnik.Shop
{
    public class Shop : IGoodsProvider
    {
        private readonly Warehouse _warehouse;

        
        public Shop(Warehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException(nameof(warehouse));
            
            _warehouse = warehouse;
        }

        public Cart Cart()
        {
            return new Cart(this);
        }

        public bool HasGoods(Good good, Amount amount)
        {
            return _warehouse.HasGoods(good, amount);
        }
    }
}