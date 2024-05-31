using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyObj
{
    public GameObject[] enemyPref;
    public int amountOfEnemy;

    public GameObject GetEnemyRan() => enemyPref[Random.Range(0, enemyPref.Length)];
    
}
