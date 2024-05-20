using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEnemy : Enemy
{
    [SerializeField] private GameObject acidPref;
    [SerializeField] private float acidDamage;
    [SerializeField] private float acidTimelife;

    private Acid script;
    private GameObject go;


    protected override void Dead()
    {
        base.Dead();
        SpawnAcidGo();
    }

    void SpawnAcidGo()
    {
        go = Instantiate(acidPref, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        script = go.GetComponent<Acid>();
        script.CreateAcidSwawp(acidDamage, acidTimelife);
    }
}
