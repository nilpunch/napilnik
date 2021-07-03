using System;

namespace Napilnik.BotWeaponPlayer
{
    internal class Player
    {
        private int _health;

        public Player(int health)
        {
            if (health < 0)
                throw new ArgumentOutOfRangeException(nameof(health));

            _health = health;
        }

        public bool Dead => _health == 0;
        public bool Alive => Dead == false;

        public void TakeDamage(Damage damage)
        {
            if (Dead)
                throw new InvalidOperationException();

            _health -= damage.Value;

            if (_health < 0)
                _health = 0;
        }
    }
}