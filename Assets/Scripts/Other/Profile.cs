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

    private List<Weapon> listWeapons;

    private GameObject go;
    [Header("Select Weapon")]
    [SerializeField] private GameObject[] guns;
    private List<int> amountOfGuns;
    private int selectWeapons;

    void Start()
    {
        SetWeapons();
        selectWeapons = amountOfGuns[0];
        guns[selectWeapons].SetActive(true);
    }

    private void Update()
    {
        TextCode();
    }

    void SetWeapons()
    {
        amountOfGuns = new List<int>();
        amountOfGuns.Clear();
        for(int i = 0; i < Shop.Instance.shopItemsList.Count; i++)
        {
            int idx = i;
            if (Shop.Instance.shopItemsList[idx].isPurchased) {
                UnlockGun(idx);
            }

            
        }
    }

    public void UnlockGun(int index)
    {
        amountOfGuns.Add(index);
    }

    void TextCode()
    {
        if (InputManager.Instance.mouseRight)
        {
            guns[selectWeapons].SetActive(false);
            
            if (amountOfGuns.IndexOf(selectWeapons) + 1 >= amountOfGuns.Count)
                selectWeapons = amountOfGuns[0];
            else 
                selectWeapons = amountOfGuns[amountOfGuns.IndexOf(selectWeapons) + 1];

            guns[selectWeapons].SetActive(true);
        }
    }

}
