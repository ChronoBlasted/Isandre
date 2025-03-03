using System.Collections;
using UnityEngine;

public class MultiShotDecorator : IAttackBehaviour
{
    private IAttackBehaviour decoratedBehaviour;
    public int numberOfShots = 3;
    public float angleSpread = 15f;

    public MultiShotDecorator(IAttackBehaviour decoratedBehaviour, int numberOfBullet, float spread)
    {
        this.decoratedBehaviour = decoratedBehaviour;     
        this.numberOfShots = numberOfBullet;
        this.angleSpread = spread;
    }

    public void Attack(Weapon weapon)
    {
        if (weapon.firePoint == null)
        {
            Debug.LogWarning("FirePoint non assigné");
            return;
        }

        Transform firePoint = weapon.firePoint;
        Quaternion originalRotation = firePoint.rotation;
        float halfSpread = angleSpread * 0.5f;

        for (int i = 0; i < numberOfShots; i++)
        {
            // Calcul de l'angle pour répartir les tirs
            float angle = Mathf.Lerp(-halfSpread, halfSpread, (numberOfShots == 1 ? 0.5f : i / (float)(numberOfShots - 1)));
            firePoint.rotation = originalRotation * Quaternion.Euler(0, angle, 0);
            decoratedBehaviour.Attack(weapon);
        }
        firePoint.rotation = originalRotation;
    }
}

public class BurstFireDecorator : IAttackBehaviour
{
    private IAttackBehaviour decoratedBehaviour;
    public int burstCount;           // Nombre de tirs dans la rafale
    public float timeBetweenShots;   // Temps d'attente entre chaque tir dans la rafale

    // Le constructeur prend le comportement de base, le nombre de tirs et le délai entre eux
    public BurstFireDecorator(IAttackBehaviour decoratedBehaviour, int burstCount, float timeBetweenShots)
    {
        this.decoratedBehaviour = decoratedBehaviour;
        this.burstCount = burstCount;
        this.timeBetweenShots = timeBetweenShots;
    }

    public void Attack(Weapon weapon)
    {
        // On vérifie que l'arme est un MonoBehaviour pour démarrer la coroutine
        MonoBehaviour mb = weapon as MonoBehaviour;
        if (mb != null)
        {
            mb.StartCoroutine(BurstCoroutine(weapon));
        }
        else
        {
            // Sinon, on effectue une boucle sans délai
            for (int i = 0; i < burstCount; i++)
            {
                decoratedBehaviour.Attack(weapon);
            }
        }
    }

    private IEnumerator BurstCoroutine(Weapon weapon)
    {
        for (int i = 0; i < burstCount; i++)
        {
            decoratedBehaviour.Attack(weapon);
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}
