using System;

namespace Napilnik.Shop
{
    public struct Amount
    {
        public int Value { get; }

        public Amount(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            Value = value;
        }
        
        public static Amount operator+(Amount a, Amount b)
        {
            return new Amount(a.Value + b.Value);
        }
        
        public static implicit operator Amount(int value)
        {
            return new Amount(value);
        }
    }
}