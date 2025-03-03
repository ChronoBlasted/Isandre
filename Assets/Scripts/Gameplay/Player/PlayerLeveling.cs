using UnityEngine;

public class PlayerLeveling : MonoBehaviour
{
    [SerializeField] int currentXP;
    [SerializeField] int xpToLevelUp;

    [SerializeField] int playerLevel;

    [SerializeField] GameObject levelUpChest;
    [SerializeField] Vector2 radiusMinMaxSpawnChest;


    public void GainXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= xpToLevelUp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        currentXP = 0;
        playerLevel++;

/*        GameObject chest = Instantiate(levelUpChest);
        chest.transform.position = new Vector3(
            PlayerManager.Instance.transform.position.x + Random.Range(radiusMinMaxSpawnChest.x, radiusMinMaxSpawnChest.y),
            .1f,
            PlayerManager.Instance.transform.position.z + Random.Range(radiusMinMaxSpawnChest.x, radiusMinMaxSpawnChest.y));

*/
        float angle = Random.Range(0f, 360f);
        // Générer une distance aléatoire entre min et max
        float distance = Random.Range(radiusMinMaxSpawnChest.x, radiusMinMaxSpawnChest.y);

        // Calculer la position de spawn
        Vector3 spawnPosition = PlayerManager.Instance.transform.position + new Vector3(Mathf.Cos(angle) * distance, 0, Mathf.Sin(angle) * distance);

        // Instancier l'objet
        Instantiate(levelUpChest, spawnPosition, Quaternion.identity);

    }
}
