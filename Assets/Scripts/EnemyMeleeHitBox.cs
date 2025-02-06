using UnityEngine;

public class EnemyMeleeHitBox : MonoBehaviour
{
    public int speed;
    public int damage;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Alive _alive))
        {
            _alive.ChangeLife(-damage);
        }
    }
}
