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
        transform.DOScale(Vector3.one, .1f).SetEase(Ease.OutBack);
    }

    private void Update()
    {
        transform.position += ProjectileData.speed * Time.deltaTime * transform.forward;

    }

    private void OnDisable()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Release();
        }
    }

    private void Release()
    {
        if (gameObject.activeSelf) PoolManager.Instance[ProjectileData.type].Release(gameObject);
    }
}
