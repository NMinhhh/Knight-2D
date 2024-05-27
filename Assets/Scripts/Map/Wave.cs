using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject[] enemyPref;

    public GameObject[] GetEnemy() => enemyPref;
}
