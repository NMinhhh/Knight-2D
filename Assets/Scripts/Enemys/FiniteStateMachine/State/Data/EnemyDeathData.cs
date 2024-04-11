using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDeathData", menuName = "Data/Death Data")]
public class EnemyDeathData : ScriptableObject
{
    public GameObject particle;
    public int amountOfEx = 1;
    public float radius = 1;
}