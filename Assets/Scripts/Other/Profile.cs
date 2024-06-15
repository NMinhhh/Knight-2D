using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{


    public static Profile Instance;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }


    private GameObject go;
    [Header("Select Weapon")]
    [SerializeField] private GameObject[] guns;

    void Start()
    {
        foreach(GameObject go in guns)
        {
            go.SetActive(false);
        }
        guns[GameManager.Instance.selectedWeaponIndex].SetActive(true);
    }



}
