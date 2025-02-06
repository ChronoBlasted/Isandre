using UnityEngine;

public class Spawner : MonoBehaviour
{
    //public GameObject toSpawn;
    public Transform where;

    public virtual GameObject SpawnObject(GameObject _toSpawn)
    {
       return Instantiate(_toSpawn.gameObject, transform.position, transform.rotation); //R�cup�rer depuis la Pool
    }

}
