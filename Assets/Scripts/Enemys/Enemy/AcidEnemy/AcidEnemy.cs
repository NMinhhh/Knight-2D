using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEnemy : Enemy
{
    [SerializeField] private GameObject acidPref;
    [SerializeField] private float acidDamage;
    [SerializeField] private float acidTimelife;

    private Acid acidScript;
    private GameObject acidGo;


    protected override void Dead()
    {
        base.Dead();
        CreateAcidGo();
    }

    void CreateAcidGo()
    {
        acidGo = Instantiate(acidPref, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        acidScript = acidGo.GetComponent<Acid>();
        acidScript.CreateAcidSwawp(acidDamage, acidTimelife);
    }
}
