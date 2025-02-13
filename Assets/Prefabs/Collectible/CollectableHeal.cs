using UnityEngine;

public class CollectableHeal : Collectable
{
    protected override void Update()
    {
        base.Update();

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 15 && other.gameObject.TryGetComponent<Alive>(out Alive alive))
        {
            alive.ChangeLife(10);
        }
    }


}
