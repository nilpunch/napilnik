using NUnit.Framework;

namespace Napilnik.BotWeaponPlayer
{
    // Use Case
    internal static class BotWeaponPlayerTests
    {
        [Test]
        public static void RegularUsage()
        {
            Player player = new Player(20);
            Weapon weapon = new Weapon(new Damage(10), new Ammo(7));
            Bot bot = new Bot(weapon);
            bot.OnSeePlayer(player);
        }
    }
}