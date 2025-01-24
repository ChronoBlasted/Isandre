using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    public ProjectileData ProjectileData;

    [SerializeField] TrailRenderer trailRenderer;

    public void Init()
    {
        trailRenderer.Clear();

        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, .1f);

        Invoke(nameof(Release), 10f);
    }

    private void Update()
    {
        transform.position += ProjectileData.speed * Time.deltaTime * transform.forward;

    }

    private void OnDisable()
    {
        CancelInvoke();
        StopAllCoroutines();
        DOTween.Kill(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        Release();
    }

    private void Release()
    {
        PoolManager.Instance[ProjectileData.type].Release(gameObject);
    }
}
