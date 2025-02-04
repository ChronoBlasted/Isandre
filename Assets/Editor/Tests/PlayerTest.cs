using NUnit.Framework;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [Test]
    public void PlayerTakeDamage()
    {
    }

    [Test]
    public void PlayerMoveForward()
    {
        PlayerManager.Instance.playerMovement.Move(Vector2.up);
        PlayerManager.Instance.playerMovement.Move(Vector2.right);
        PlayerManager.Instance.playerMovement.Move(Vector2.left);
        PlayerManager.Instance.playerMovement.Move(Vector2.down);
    }

    [Test]
    public void PlayerDash()
    {
    }

    [Test]
    public void PlayerRun()
    {
    }
}
