using System;

public enum ResourceType
{
    None = 0,

    ______ENTITIES______ = 100,

    Player = 101,

    ______WEAPON______ = 200,

    Sword = 201,

    ______PROJECTILE______ = 300,

    Bullet = 301,

    ______UI______ = 400,

    FloatingText = 401,
}

public enum WeaponType
{
    None = ResourceType.None,

    Sword = ResourceType.Sword,
}

public enum ProjectileType
{
    None = ResourceType.None,

    Bullet = ResourceType.Bullet,
}

public enum EntityType
{
    Player = ResourceType.Player,
}