using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stage 
{
    public GameObject[] enemyPref;
    public bool isBoss;
    public float cooldown;

    public GameObject[] GetEnemy() => enemyPref;
}
