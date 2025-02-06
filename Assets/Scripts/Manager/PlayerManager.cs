using BaseTemplate.Behaviours;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public PlayerMovement playerMovement;
    public PlayerAnimation playerAnimation;
    public PlayerWeapon playerWeapon;
    public Alive PlayerLife;
}
