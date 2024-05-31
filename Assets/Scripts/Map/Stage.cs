using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stage 
{
    public EnemyObj[] enemyObj;
    public bool isBoss;
    public float cooldown;
    public int GetEnemyObjLength() => enemyObj.Length;

    public EnemyObj GetEnemyObj(int idx) => enemyObj[idx];

}
