using System;

namespace Napilnik.BotWeaponPlayer
{
    internal class Ammo
    {
        private int _count;

        public Ammo(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            _count = capacity;
        }

        public bool Empty => _count == 0;

        public void EjectOne()
        {
            if (Empty)
                throw new InvalidOperationException();

            _count -= 1;
        }
    }
}