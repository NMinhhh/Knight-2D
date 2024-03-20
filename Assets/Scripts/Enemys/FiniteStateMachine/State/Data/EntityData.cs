using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newEntityData",menuName ="Data/Entity Data")]
public class EntityData : ScriptableObject
{
    public Vector2 sizeCheck;
    public LayerMask whatIsPlayer;

}
