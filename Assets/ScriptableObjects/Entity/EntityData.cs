using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEntityData", menuName = "ScriptableObjects/NewEntityData", order = 0)]
public class EntityData : ScriptableObject
{
    public int health;
    public float walkSpeed;
    public float runSpeed;
    public float dashForce;
}
