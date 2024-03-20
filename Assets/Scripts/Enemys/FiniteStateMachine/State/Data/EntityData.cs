using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newEntityData",menuName ="Data/Entity Data")]
public class EntityData : ScriptableObject
{
    public Vector2 sizeCheck;
    public LayerMask whatIsPlayer;

    public float maxHealth;

    public float knockbackSpeed = 0;
    public float hurtTime = 0.1f;

    public float damageTimeCon = 0.1f;

}
