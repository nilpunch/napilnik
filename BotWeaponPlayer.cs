using System;

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

internal class Weapon
{
    public enum AttackResult
    {
        OutOfAmmo,
        Ok,
    }
    
    private readonly Damage _damage;
    
    private readonly Ammo _ammo;

    public Weapon(Damage damage, Ammo ammo)
    {
        if (ammo == null)
            throw new ArgumentNullException(nameof(ammo));
        
        _damage = damage;
        _ammo = ammo;
    }

    public AttackResult TryAttack(Player player)
    {
        if (player == null)
            throw new ArgumentNullException(nameof(player));
        
        if (player.Dead)
            throw new InvalidOperationException();

        if (_ammo.Empty)
            return AttackResult.OutOfAmmo;
        
        _ammo.EjectOne();
        player.TakeDamage(_damage);
        return AttackResult.Ok;
    }
}

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

    public void TakeDamage(Damage damage)
    {
        if (Dead)
            throw new InvalidOperationException();

        _health -= damage.Value;

        if (_health < 0)
            _health = 0;
    }
}

internal class Bot
{
    private readonly Weapon _weapon;

    public Bot(Weapon weapon)
    {
        if (weapon == null)
            throw new ArgumentNullException(nameof(weapon));
        
        _weapon = weapon;
    }

    public void OnSeePlayer(Player player)
    {
        _weapon.TryAttack(player);
    }
}

// Use Case
internal class Program
{
    public static void Main()
    {
        Player player = new Player(20);
        Weapon weapon = new Weapon(new Damage(10), new Ammo(7));
        Bot bot = new Bot(weapon);
        bot.OnSeePlayer(player);
    }
}