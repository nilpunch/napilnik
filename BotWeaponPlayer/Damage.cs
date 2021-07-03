using System;

namespace Napilnik.BotWeaponPlayer
{
    internal struct Damage
    {
        public int Value { get; }

        public Damage(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            Value = value;
        }
    }
}