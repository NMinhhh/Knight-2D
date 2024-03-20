using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newSpawnData",menuName = "Data/Spawn Data")]
public class EnemySpawnData : ScriptableObject
{
    public GameObject spawnGO;
    public float cooldown = 0.3f;
    public int amountOfSpawnGO = 5;
    public float speed = 20;
    public float timeLife = 3;
    public float damage = 10;
}
