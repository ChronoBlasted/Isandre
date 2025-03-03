using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    public ProjectileData projectileData;
    public LayerMask layer;

    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] ParticleSystem ps;

    public GameObject lastHitEnemy;

    public int damage;

    // Champ pour le comportement décoré du projectile.
    public IProjectileBehaviour projectileBehaviour;

    public void Init(int damage)
    {
        this.damage = (damage == 0 ? projectileData.damage : damage);
        lastHitEnemy = null;
        trailRenderer.Clear();
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, .1f).SetEase(Ease.OutBack);
        
        projectileBehaviour = new BasicProjectileBehaviour(this);

        if (PlayerManager.Instance.playerWeapon != null)
        {
            foreach (var decoratorFunc in PlayerManager.Instance.playerWeapon.bulletDecoratorFuncs)
            {
                projectileBehaviour = decoratorFunc(projectileBehaviour);
            }
        }
    }

    private void Update()
    {
        transform.position += projectileData.speed * Time.deltaTime * transform.forward;
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if (!gameObject.activeSelf) return;

        GameObject vfx = PoolManager.Instance[ResourceType.BulletImpact].Get();
        ParticleSystem particleSystem = vfx.GetComponent<ParticleSystem>();
        vfx.transform.position = collision.GetContact(0).point;
        particleSystem.Play();

        if (collision.gameObject.layer == 16)
        {
            if (collision.gameObject.TryGetComponent(out Alive _alive))
            {
                Debug.Log("Collision avec enemy ou player");
                _alive.ChangeLife(-damage);
            }
            HandleHit();
            return;
        }

        if (collision.gameObject.layer == 9)
        {
            Debug.Log("Collision avec un mur");
            HandleHit();
            return;
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.activeSelf) return;

        GameObject vfx = PoolManager.Instance[ResourceType.BulletImpact].Get();
        ParticleSystem particleSystem = vfx.GetComponent<ParticleSystem>();
        vfx.transform.position = transform.position;
        particleSystem.Play();

        if (other.gameObject.layer == 16)
        {
            lastHitEnemy = other.gameObject;
            if (other.gameObject.TryGetComponent(out Alive _alive))
            {
                Debug.Log("Collision avec enemy ou player");
                _alive.ChangeLife(-damage);
            }
            HandleHit();
            return;
        }

        if (other.gameObject.layer == 9)
        {
            Debug.Log("Collision avec un mur");
            HandleHit();
            return;
        }
    }

    private void HandleHit()
    {
        if (projectileBehaviour != null)
            projectileBehaviour.OnHit();
        else
            DoHit();
    }

    // Méthode de secours si aucun décorateur n'est défini.
    public virtual void DoHit()
    {
        AudioManager.Instance.PlaySound(projectileData.hitAudioName);
        Release();
    }

    public void Release()
    {
        PoolManager.Instance[projectileData.type].Release(gameObject);
    }
}
