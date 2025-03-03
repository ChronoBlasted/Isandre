using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    public ProjectileData projectileData;
    public LayerMask layer;

    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] ParticleSystem ps;

    public int damage;

    public void Init(int damage)
    {
        this.damage = damage;
        
        if (damage == 0)
            damage = projectileData.damage;

        trailRenderer.Clear();

        //ps.transform.SetParent(transform);

        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, .1f).SetEase(Ease.OutBack);
    }

    private void Update()
    {
        transform.position += projectileData.speed * Time.deltaTime * transform.forward;

    }

    private void OnDisable()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (!gameObject.activeSelf) return;
        GameObject vfx = PoolManager.Instance[ResourceType.BulletImpact].Get();
        ParticleSystem particleSystem = vfx.GetComponent<ParticleSystem>();
        vfx.transform.position = collision.GetContact(0).point;
        particleSystem.Play();

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(collision.gameObject.TryGetComponent(out Alive _alive))
            {
                Debug.Log("Ennemy or player collision");               

                _alive.ChangeLife(-damage);               
            }
            DoHit();
            return;
        }

        if (collision.gameObject.layer == 9)
        {
            Debug.Log("Wall Collision");
            DoHit();
            return;
        }
    }

    private void DoHit()
    {
        /*
        ps.transform.position = transform.position;
        ps.transform.SetParent(null);
        ps.Play();
        */

        AudioManager.Instance.PlaySound(projectileData.hitAudioName);

        Release();
    }

    private void Release()
    {
        PoolManager.Instance[projectileData.type].Release(gameObject);
    }
}
