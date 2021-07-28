using System;
using System.Collections.Generic;

namespace Napilnik.Shop
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
}