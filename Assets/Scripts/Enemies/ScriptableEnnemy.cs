using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Scriptable Objects/EnemyType")]
public class ScriptableEnnemy : ScriptableObject
{
    public string enemyName;
    public int enemyRange;
    public int enemyLife;
    public int ennemyspeed;
    public int AttackTime;
    public GameObject Projectile;           
}
