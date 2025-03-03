using UnityEngine;

public class BasicProjectileBehaviour : IProjectileBehaviour
{
    private Projectile projectile;

    public BasicProjectileBehaviour(Projectile projectile)
    {
        this.projectile = projectile;
    }

    /// <summary>
    /// Permet d’accéder au projectile associé.
    /// </summary>
    public Projectile Projectile { get { return projectile; } }

    public void OnHit()
    {
        // Comportement de base : jouer le son et libérer le projectile.
        AudioManager.Instance.PlaySound(projectile.projectileData.hitAudioName);
        projectile.Release();
    }
}