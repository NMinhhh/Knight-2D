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
    public List<int> amountOfGuns {  get; private set; }
    private int selectWeapons;

    void Start()
    {
        //GameManager.Instance.Load();
        SetWeapons(GameManager.Instance.gunUnlock);
        selectWeapons = amountOfGuns[0];
        guns[selectWeapons].SetActive(true);
    }

    private void Update()
    {
        if (InputManager.Instance.mouseRight)
        {
            SelectedWeapon();
        }
    }

    public void SetWeapons(int[] amount)
    {
        amountOfGuns = new List<int>();
        amountOfGuns.Clear();
        amountOfGuns.AddRange(amount);
    }

    public void UnlockGun(int index)
    {
        amountOfGuns.Add(index);
    }

    public void SelectedWeapon()
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
