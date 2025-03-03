using UnityEngine;

public class ExplosiveProjectileDecorator : ProjectileDecorator
{
    public float explosionRadius;
    public float explosionDamage;
    public float explosionForce;    // Force de recul appliquée aux ennemis
    public GameObject explosionVFX; // Prefab du VFX à instancier lors de l'explosion

    // Le constructeur doit prendre en premier le comportement de base, puis les paramètres.
    public ExplosiveProjectileDecorator(IProjectileBehaviour decoratedBehaviour, float explosionRadius, float explosionDamage, float explosionForce)
        : base(decoratedBehaviour)
    {
        this.explosionRadius = explosionRadius;
        this.explosionDamage = explosionDamage;
        this.explosionForce = explosionForce;        
    }

    public override void OnHit()
    {
        // Essayer de récupérer le projectile depuis le comportement de base.
        BasicProjectileBehaviour baseBehaviour = decoratedBehaviour as BasicProjectileBehaviour;
        if (baseBehaviour != null)
        {
            Projectile proj = baseBehaviour.Projectile;
            // Recherche des cibles dans le rayon d'explosion.
            Collider[] hitColliders = Physics.OverlapSphere(proj.transform.position, explosionRadius);

            GameObject vfx = PoolManager.Instance[ResourceType.ExploseImpact].Get();
            ParticleSystem particleSystem = vfx.GetComponent<ParticleSystem>();
            vfx.transform.position = proj.transform.position;
            particleSystem.Play();

            foreach (var hitCollider in hitColliders)
            {
                if(hitCollider.gameObject.layer != LayerMask.NameToLayer("Enemy"))
                {
                    continue;
                }

                Debug.Log("Explosion");              
                

                // Appliquer des dégâts si l'objet possède un composant 'Alive'
                Alive alive = hitCollider.GetComponent<Alive>();
                if (alive != null)
                {
                    alive.ChangeLife((int)-explosionDamage);
                }

                // Appliquer une force de recul si l'objet possède un Rigidbody.
                Rigidbody rb = hitCollider.attachedRigidbody;
                if (rb != null)
                {
                    // Calculer la direction du recul depuis le centre de l'explosion.
                    Vector3 direction = (hitCollider.transform.position - proj.transform.position).normalized;
                    rb.AddForce(direction * explosionForce, ForceMode.Impulse);
                }
            }
        }

        // Enfin, appel du comportement de base (libération du projectile, etc.)
        base.OnHit();
    }
}

public class ChainProjectileDecorator : ProjectileDecorator
{
    public float chainRadius;
    public int nbOfChain;

    // Le constructeur prend le comportement de base et le rayon de recherche pour la cible suivante.
    public ChainProjectileDecorator(IProjectileBehaviour decoratedBehaviour, float chainRadius, int nbOfChain)
        : base(decoratedBehaviour)
    {
        this.chainRadius = chainRadius;
        this.nbOfChain = nbOfChain;
    }

    public override void OnHit()
    {
        BasicProjectileBehaviour baseBehaviour = decoratedBehaviour as BasicProjectileBehaviour;

        if (baseBehaviour != null)
        {
            Projectile proj = baseBehaviour.Projectile;
            Collider[] hitColliders = Physics.OverlapSphere(proj.transform.position, chainRadius);
            Transform target = null;
            float minDistance = Mathf.Infinity;
            foreach (Collider hit in hitColliders)
            {
                if (hit.gameObject.layer != LayerMask.NameToLayer("Enemy"))
                {
                    continue;
                }
                if (proj.lastHitEnemy != null && hit.gameObject == proj.lastHitEnemy)
                {
                    continue;
                }
                else
                {
                    if (nbOfChain <= 0)
                    {
                        base.OnHit();
                    }

                    float distance = Vector3.Distance(proj.transform.position, hit.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        target = hit.transform;                        
                        nbOfChain--;                        
                    }
                }
            }
            if (target != null)
            {
                // Réorientation vers la nouvelle cible
                Vector3 newDirection = (target.position - proj.transform.position).normalized;
                //proj.transform.forward = newDirection;
                proj.transform.rotation = Quaternion.LookRotation(newDirection);                
                // On peut éventuellement instancier un effet visuel pour le chain.
                return; // Le projectile ne se libère pas, il continue sa trajectoire.
            }
        }
        

        // Si aucune cible n'est trouvée, on libère le projectile normalement.
        base.OnHit();
    }
}

public class PiercingProjectileDecorator : ProjectileDecorator
{
    public int pierceCount;

    // Le constructeur doit recevoir le comportement de base et le nombre de cibles à traverser.
    public PiercingProjectileDecorator(IProjectileBehaviour decoratedBehaviour, int pierceCount)
        : base(decoratedBehaviour)
    {
        this.pierceCount = pierceCount;
    }

    public override void OnHit()
    {
        if (pierceCount > 0)
        {
            pierceCount--;
            Debug.Log("Projectile perçant : il peut encore traverser " + pierceCount + " cibles.");
            // Ici, on n'appelle pas base.OnHit(), ce qui permet de ne pas libérer le projectile.
            // Vous pouvez éventuellement jouer un effet visuel ou sonore pour indiquer le perçage.
        }
        else
        {
            // Plus de traversée possible, on appelle le comportement de base qui libère le projectile.
            base.OnHit();
        }
    }
}