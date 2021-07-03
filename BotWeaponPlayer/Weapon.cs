using System;

namespace Napilnik.BotWeaponPlayer
{
    internal class Weapon
    {
        private readonly Damage _damage;

        private readonly Ammo _ammo;

        public Weapon(Damage damage, Ammo ammo)
        {
            if (ammo == null)
                throw new ArgumentNullException(nameof(ammo));

            _damage = damage;
            _ammo = ammo;
        }

        public bool CanAttack => _ammo.Empty;
        private bool CantAttack => CanAttack == false;

        public void Attack(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            if (player.Dead)
                throw new InvalidOperationException();

            if (CantAttack)
                throw new InvalidOperationException();

            _ammo.EjectOne();
            player.TakeDamage(_damage);
        }
    }
}