using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ChestLevelUpData", menuName = "Data/ChestLevelUpData", order = 1)]
public class ChestLevelUpData : ScriptableObject
{
    public List<PowerUp> basicPowerUp;
    public List<PowerUp> rarePowerUp;
    public List<PowerUp> epicPowerUp;

    public float timeToOpen;
}
