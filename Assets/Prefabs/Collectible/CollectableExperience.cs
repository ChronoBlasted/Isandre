using UnityEngine;

public class CollectableExperience : Collectable
{
    protected override void Update()
    {
        base.Update();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 15)
        {
            
        }
    }
}
