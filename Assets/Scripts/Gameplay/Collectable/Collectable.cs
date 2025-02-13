using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Transform model;
    public int rotationSpeed;

    protected virtual void Update()
    {
        model.Rotate(Vector3.forward, rotationSpeed);
        
    }

}
