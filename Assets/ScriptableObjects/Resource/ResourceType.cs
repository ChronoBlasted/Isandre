using System;

public enum ResourceType
{
    None = 0,

    ______ENTITIES______ = 100,

    Player = 101,

    ______WEAPON______ = 200,

    Pistol = 202,
    AssaultRifle = 203,
    Shotgun = 204,
    Kick = 205,
    Punch = 206,

    ______PROJECTILE______ = 300,

    NormalBullet = 301,

    ______UI______ = 400,

    FloatingText = 401,
}

public enum WeaponType
{
    None = ResourceType.None,

    Pistol = ResourceType.Pistol,
    AssaultRifle = ResourceType.AssaultRifle,
    Shotgun = ResourceType.Shotgun,
    Kick = ResourceType.Kick,
    Punch = ResourceType.Punch,
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