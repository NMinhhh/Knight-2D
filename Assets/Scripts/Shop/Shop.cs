using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private WeaponObject weaponData;

    [SerializeField] private GameObject ItemTemplate;
    [SerializeField] private Transform shopScrollView;
    [Header("Avatar")]

    [SerializeField] private AvatarData avatarData;

    [SerializeField] private GameObject avatarItemTemplate;
    [SerializeField] private Transform avatarContent;

    [Header("Coin")]
    [SerializeField] private List<Coin> coinList;

    [SerializeField] private GameObject coinItemTemplate;
    [SerializeField] private Transform coinCotent;



    void Start() 
    {
        GenerateAvatarItemUI();
        GenerateWeaponItemUI();
        GenerateCoinItemUI();
    }

    #region Weapon
    public void GenerateWeaponItemUI()
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
            weaponItemUI.SetWeaponImage(weapon.image, weapon.widthImage);
            weaponItemUI.SetWeaponDamage(weapon.damage.ToString());
            weaponItemUI.SetWeaponBullets(weapon.bullet.ToString());
            weaponItemUI.SetWeaponReload(weapon.reload.ToString());

            if (!weapon.isPurchased)
            {
                weaponItemUI.OnWeaponUnlockButton(idx, BuyWeaponItem);
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

    void BuyWeaponItem(int idx)
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
        SelectedWeaponUI.Instance.SelectedWeapon();
    }

    public WeaponItemUI GetWeaponItemUI(int i)
    {
        return shopScrollView.GetChild(i).GetComponent<WeaponItemUI>();
    }
    #endregion

    #region Avatar

    public void GenerateAvatarItemUI()
    {
        int count = avatarContent.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(avatarContent.GetChild(i).gameObject);
        }

        for(int i = 0; i < CoinManager.Instance.GetAllAvatarPurchasedIndex().Count; i++)
        {
            int idAvatarPurchased = CoinManager.Instance.GetAvatarPurchased(i);
            avatarData.AvatarPurchased(idAvatarPurchased);
        }

        for (int i = 0; i < avatarData.GetAvatarLenth(); i++)
        {
            int idx = i;
            Avatar avatar = avatarData.GetAvatar(idx);
            if (!avatar.isPurchased)
            {
                AvatarItemUI avatarItemUI = Instantiate(avatarItemTemplate, avatarContent).GetComponent<AvatarItemUI>();
                avatarItemUI.SetID(avatar.id);
                avatarItemUI.SetImage(avatar.image);
                avatarItemUI.SetName(avatar.name);
                avatarItemUI.SetPrice(avatar.price.ToString());
                avatarItemUI.OnAvatarUnlock(idx, BuyAvatarItem);
            }
        }

        for (int i = 0; i < avatarData.GetAvatarLenth(); i++)
        {
            int idx = i;
            Avatar avatar = avatarData.GetAvatar(idx);
            if(avatar.isPurchased)
            {
                AvatarItemUI avatarItemUI = Instantiate(avatarItemTemplate, avatarContent).GetComponent<AvatarItemUI>();
                avatarItemUI.SetImage(avatar.image);
                avatarItemUI.SetName(avatar.name);
                avatarItemUI.AvatarPurchased();
            }
        }
    }
    
    void BuyAvatarItem(int idx)
    {
        Avatar avatar = avatarData.GetAvatar(idx);
        if (CoinManager.Instance.HasEnoughCoins(avatar.price))
        {
            CoinManager.Instance.AddAvatarPurchased(idx);
            int count = avatarContent.childCount;
            for (int i = 0; i < count; i++)
            {
                AvatarItemUI avatarItemUI = avatarContent.GetChild(i).GetComponent<AvatarItemUI>();
                if (avatarItemUI.id == idx)
                {
                    avatarItemUI.AvatarPurchased();
                }
            }
            GenerateAvatarItemUI();
            SelectedAvatar.Instance.GenerateAvatarUI();
        }
    }


    #endregion

    #region Coin

    void GenerateCoinItemUI()
    {
        for (int i = 0; i < coinList.Count; i++)
        {
            int idx = i;
            Coin coin = coinList[idx];
            CoinItemUI coinItemUI = Instantiate(coinItemTemplate, coinCotent).GetComponent<CoinItemUI>();
            coinItemUI.SetImage(coin.image);
            coinItemUI.SetValue(coin.value);
            coinItemUI.SetPrice(coin.price);
            coinItemUI.OnButtonBuy(idx, BuyCoinItem);
        }
    }

    void BuyCoinItem(int idx)
    {
        Coin coin = coinList[idx];
        if (CoinManager.Instance.HasEnenoughDiamond(coin.price))
        {
            CoinManager.Instance.UseDiamond(coin.price);
            CoinManager.Instance.PickupCoins(coin.value);
        }
    }

    #endregion
    [System.Serializable]
    class Coin
    {
        public Sprite image;
        public int value;
        public int price;
    }
}
