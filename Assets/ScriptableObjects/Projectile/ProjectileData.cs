using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileData", menuName = "ScriptableObjects/NewProjectileData", order = 2)]
public class ProjectileData : ResourceObject
{
    public string hitAudioName = "BulletHit";
    public float speed;
    public float size;
}