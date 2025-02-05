using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int speed;
    public int damage;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Alive _alive))
        {
            Debug.Log("Hit => " + collision.gameObject.name);
            _alive.ChangeLife(-damage);
        }
        Destroy(gameObject); //à retourner dans la POOL
    }
}
