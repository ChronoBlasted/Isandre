using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    public ProjectileData projectileData;

    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] ParticleSystem ps;

    public void Init()
    {
        trailRenderer.Clear();

        ps.transform.SetParent(transform);

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

        if (collision.gameObject.layer == 9)
        {
            ps.transform.position = transform.position;
            ps.transform.SetParent(null);
            ps.Play();

            AudioManager.Instance.PlaySound(projectileData.hitAudioName);

            Release();
        }
    }

    private void Release()
    {
        PoolManager.Instance[projectileData.type].Release(gameObject);
    }
}
