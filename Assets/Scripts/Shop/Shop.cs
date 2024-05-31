using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Weapon")]

    [SerializeField] private GameObject ItemTemplate;
    [SerializeField] private Transform shopScrollView;
    [SerializeField] private DetailWeaponUI detailWeaponUI;

    WeaponObject weaponData;

    [Header("Avatar")]
    [SerializeField] private GameObject avatarItemTemplate;
    [SerializeField] private Transform avatarContent;
    [SerializeField] private SelectedAvatar selectedAvatar;

    AvatarData avatarData;

    [Header("Coin")]
    [SerializeField] private List<Coin> coinList;

    [SerializeField] private GameObject coinItemTemplate;
    [SerializeField] private Transform coinCotent;



    void Start() 
    {
        GetData();
        GenerateAvatarItemUI();
        GenerateWeaponItemUI();
        GenerateCoinItemUI();
    }

    void GetData()
    {
        weaponData = GameData.Instance.GetWeaponData();
        avatarData = GameData.Instance.GetAvatarData();
    }

    #region Weapon
    public void GenerateWeaponItemUI()
    {
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
            weaponItemUI.OnClickDetailButton(idx, OpenDeatailWeaponUI);
            if (!weapon.isPurchased)
            {
                weaponItemUI.OnWeaponUnlockButton(idx, BuyWeaponItem);
                weaponItemUI.SetWeaponPrice(weapon.price.ToString());
            }
            else
            {
                if (idx == GameManager.Instance.selectedWeaponIndex)
                {
                    weaponItemUI.OnWeaponEquipButton();
                    GameManager.Instance.SetWeaponSelected(weapon);
                }
                else
                {
                    weaponItemUI.OnWeaponUnEquipButton(idx, EquipWeapon);
                }
            }
        }
    }

    void OpenDeatailWeaponUI(int idx)
    {
        Weapon weapon = weaponData.GetWeapon(idx);
        detailWeaponUI.SetNameText(weapon.name);
        detailWeaponUI.SetImage(weapon.image);
        detailWeaponUI.SetIndexText($"Damage: {weapon.damage},Bullet: {weapon.bullet},Reload: {weapon.reload},Bonus Speed: {weapon.bonusMovementSpeedPercent}%,Bonus Health: {weapon.bonusHealthPercent}%");
        detailWeaponUI.SetInfoText("dfasafas");
        detailWeaponUI.gameObject.SetActive(true);
    }


    public void CloseDetailWeaponUI()
    {
        detailWeaponUI.gameObject.SetActive(false);
    }

    void BuyWeaponItem(int idx)
    {
        Weapon weapon = weaponData.GetWeapon(idx);
        if (GameManager.Instance.HasEnoughCoins(weapon.price))
        {
            GameManager.Instance.UseCoins(weapon.price);
            GameManager.Instance.AddWeaponPurchasedIndex(idx);
            weaponData.WeaponPurchased(idx);
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
        WeaponItemUI oldWeaponItemUI = GetWeaponItemUI(GameManager.Instance.GetWeaponSelectedIndex());
        //New Weapon Equiped
        newWeaponItemUI.OnWeaponEquipButton();
        //Old Weapon UnEquiped
        oldWeaponItemUI.OnWeaponUnEquipButton(GameManager.Instance.GetWeaponSelectedIndex(), EquipWeapon);
        //Change ID Weapon
        GameManager.Instance.ChangeWeaponIndex(idx);
        //Set Weapon Selected
        GameManager.Instance.SetWeaponSelected(weaponData.GetWeapon(idx));
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
        if (GameManager.Instance.HasEnoughCoins(avatar.price))
        {
            avatarData.AvatarPurchased(idx);
            GameManager.Instance.AddAvatarPurchased(idx);
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
            selectedAvatar.GenerateAvatarUI();
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
        if (GameManager.Instance.HasEnenoughDiamond(coin.price))
        {
            GameManager.Instance.UseDiamond(coin.price);
            GameManager.Instance.AddCoin(coin.value);
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
