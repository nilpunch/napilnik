using System;

namespace Napilnik.BotWeaponPlayer
{
    internal class Bot
    {
        private readonly Weapon _weapon;

        public Bot(Weapon weapon)
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
        }

        public void OnSeePlayer(Player player)
        {
            if (_weapon.CanAttack && player.Alive)
                _weapon.Attack(player);
        }
    }
}