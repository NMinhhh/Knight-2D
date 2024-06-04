using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedWeaponUI : MonoBehaviour
{
    public static SelectedWeaponUI Instance;
    [SerializeField] private WeaponObject weaponData;
    [SerializeField] private Image image;
    [SerializeField] private Text nameText;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
        SelectedWeapon();
    }

    public void SelectedWeapon()
    {
        Weapon weapon = weaponData.GetWeapon(GameManager.Instance.GetWeaponSelectedID());
        image.sprite = weapon.image;
        nameText.text = weapon.name;
    }


}
