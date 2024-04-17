using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSlowingData", menuName = "Data/Slowing Data")]

public class EnemySlowingData : ScriptableObject
{
    public float slowingTimer;
    public Color slowingColor;
}
