using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDashAttackData",menuName ="Data/Dash Attack Data")]
public class EnemyDashAttackData : ScriptableObject
{
    public float dashSpeed = 5;
    public float distance = 1.5f;
    public float radius = .5f;
    public float damage = 10;
    public LayerMask whatIsPlayer;
    public LayerMask whatIsShield;
}
