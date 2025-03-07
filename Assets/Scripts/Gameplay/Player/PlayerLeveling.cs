using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerLeveling : MonoBehaviour
{
    [SerializeField] int currentXP;
    [SerializeField] int xpToLevelUp;

    [SerializeField] int playerLevel;

    [SerializeField] GameObject levelUpChest;
    [SerializeField] Vector2 radiusMinMaxSpawnChest;
    [SerializeField] LayerMask layer;


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
        xpToLevelUp = (int)(1.25f * xpToLevelUp);


        //Spawn chest
        bool validSpawn = false;
        Vector3 spawnPosition = Vector3.zero;

        for (int i = 0; i < 50; i++)
        {
            float angle = Random.Range(0f, 360f);
            float distance = Random.Range(radiusMinMaxSpawnChest.x, radiusMinMaxSpawnChest.y);

            spawnPosition = transform.position + new Vector3(Mathf.Cos(angle) * distance, 0, Mathf.Sin(angle) * distance);

            if (!Physics.Raycast(transform.position + Vector3.up * .5f, (spawnPosition - transform.position).normalized, Vector3.Distance(transform.position, spawnPosition), layer))
            {
                validSpawn = true;
                break;
            }
        }

        if (validSpawn)
        {
            Instantiate(levelUpChest, spawnPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(levelUpChest, transform.position, Quaternion.identity);
        }

    }

#if UNITY_EDITOR
    private void Update()
    {
            Debug.DrawRay(transform.position, transform.forward * 15, Color.red,.1f);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelUp();
        }
    }
#endif
}
