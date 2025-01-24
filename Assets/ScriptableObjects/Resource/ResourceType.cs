using System;

public enum ResourceType
{
    None = 0,

    ______ENTITIES______ = 100,

    Player = 101,

    ______WEAPON______ = 200,

    Sword = 201,
    Pistol = 202,

    ______PROJECTILE______ = 300,

    NormalBullet = 301,

    ______UI______ = 400,

    FloatingText = 401,
}

public enum WeaponType
{
    None = ResourceType.None,

    Sword = ResourceType.Sword,
    Pistol = ResourceType.Pistol,
}

public enum ProjectileType
{
    None = ResourceType.None,

    NormalBullet = ResourceType.NormalBullet,
}

public enum EntityType
{
    Player = ResourceType.Player,
}