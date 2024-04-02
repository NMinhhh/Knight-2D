using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop Instance;

    [SerializeField] private WeaponObject weaponData;

    [SerializeField] private GameObject ItemTemplate;
    [SerializeField] private Transform shopScrollView;
    [SerializeField] private Animator anim;

    Button btnBuy;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        GenerateShopUI();
    }

    void GenerateShopUI()
    {
        for (int i = 0; i < CoinManager.Instance.GetAllWeaponPurchased().Count; i++)
        {
            int idxWeaponPurchased = CoinManager.Instance.GetWeaponPurchased(i);
            weaponData.WeaponPurchased(idxWeaponPurchased);
        }

        for (int i = 0; i < weaponData.GetWeaponsLength(); i++)
        {
            int idx = i;
            Weapon weapon = weaponData.GetWeapon(idx);
            WeaponItemUI weaponItemUI = Instantiate(ItemTemplate, shopScrollView).GetComponent<WeaponItemUI>();
            weaponItemUI.SetWeaponName(weapon.name);
            weaponItemUI.SetWeaponImage(weapon.image);
            weaponItemUI.SetWeaponDamage(weapon.damage.ToString());
            weaponItemUI.SetWeaponBullets(weapon.bullet.ToString());
            weaponItemUI.SetWeaponReload(weapon.reload.ToString());

            if (!weapon.isPurchased)
            {
                weaponItemUI.OnWeaponUnlockButton(idx, BuyItem);
                weaponItemUI.SetWeaponPrice(weapon.price.ToString());
            }
            else
            {
                if (idx == CoinManager.Instance.selectedWeaponIndex)
                {
                    weaponItemUI.OnWeaponEquipButton();
                    CoinManager.Instance.SetWeaponSelected(weapon);
                }
                else
                {
                    weaponItemUI.OnWeaponUnEquipButton(idx, EquipWeapon);
                }
            }
        }
    }

    void BuyItem(int idx)
    {
        Weapon weapon = weaponData.GetWeapon(idx);
        if (CoinManager.Instance.HasEnoughCoins(weapon.price))
        {
            CoinManager.Instance.UseCoins(weapon.price);
            CoinManager.Instance.AddWeaponPurchasedIndex(idx);
            EquipWeapon(idx);
        }
        else
        {
            anim.SetTrigger("noCoin");
        }

    }

    void EquipWeapon(int idx)
    {
        //Get newWeaponUI
        WeaponItemUI newWeaponItemUI = GetWeaponItemUI(idx);
        //Get oldWeaponUI
        WeaponItemUI oldWeaponItemUI = GetWeaponItemUI(CoinManager.Instance.GetWeaponSelectedIndex());
        //New Weapon Equiped
        newWeaponItemUI.OnWeaponEquipButton();
        //Old Weapon UnEquiped
        oldWeaponItemUI.OnWeaponUnEquipButton(CoinManager.Instance.GetWeaponSelectedIndex(), EquipWeapon);
        //Change ID Weapon
        CoinManager.Instance.ChangeWeaponIndex(idx);
        //Set Weapon Selected
        CoinManager.Instance.SetWeaponSelected(weaponData.GetWeapon(idx));
    }

    public WeaponItemUI GetWeaponItemUI(int i)
    {
        return shopScrollView.GetChild(i).GetComponent<WeaponItemUI>();
    }


}
