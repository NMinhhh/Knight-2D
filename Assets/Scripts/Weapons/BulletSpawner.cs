using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner Instance {  get; private set; }

    [SerializeField] private Transform holder;
    private List<GameObject> poolObjects = new List<GameObject>();
    [SerializeField] private WeaponObject weaponData;
    Weapon weapon;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        weapon = weaponData.GetWeapon(GameManager.Instance.GetWeaponSelectedID());    
        for(int i = 0; i < weapon.bullet; i++)
        {
            GameObject go = Instantiate(weapon.bulletIcon);
            go.SetActive(false);
            poolObjects.Add(go);
            go.transform.parent = holder;
        }
    }

    public GameObject Spawn(Vector3 pos, Quaternion ro)
    {
        GameObject go = GetPooledObject();
        go.transform.position = pos;
        go.transform.rotation = ro;
        return go;
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < poolObjects.Count; i++)
        {
            if (!poolObjects[i].activeInHierarchy)
            {
                return poolObjects[i];
            }
        }
        GameObject go = Instantiate(weapon.bulletIcon);
        go.SetActive(false);
        poolObjects.Add(go);
        go.transform.parent = holder;
        return go;
    }

    public void DespawnObject(GameObject go)
    {
        go.SetActive(false);
    }

}
