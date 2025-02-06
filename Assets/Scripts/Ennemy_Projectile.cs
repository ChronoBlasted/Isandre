using UnityEngine;

public class Ennemy_Projectile : MonoBehaviour
{
    public int speed;
    public int damage;
    public bool OnlyOneHit;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Alive _alive))
        {
            _alive.ChangeLife(-damage);
        
            if(OnlyOneHit)
            Destroy(gameObject); //à retourner dans la POOL
        }

    }
}
