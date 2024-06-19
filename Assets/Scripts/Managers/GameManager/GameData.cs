using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance {  get; private set; }

    [Header("Weapon Data")]
    [SerializeField] private WeaponObject weaponData;

    [Header("Avatar Data")]
    [SerializeField] private AvatarData avatarData;

    [Header("Map Data")]
    [SerializeField] private MapData mapData;

    [Header("Coin Data")]
    [SerializeField] private CoinData coinData;

    [Header("Diamod")]
    [SerializeField] private DiamondData diamondData;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        SetWeaponData();
        SetMapData();
        SetAvatarData();
    }

    public WeaponObject GetWeaponData() => weaponData;

    public AvatarData GetAvatarData() => avatarData;

    public MapData GetMapData() => mapData;

    public CoinData GetCoinData() => coinData;

    public DiamondData GetDiamondData() => diamondData;

    public void SetDataGame()
    {
        SetWeaponData();
        SetMapData();
        SetAvatarData();
    }

    void SetWeaponData()
    {
        for (int i = 0; i < GameManager.Instance.GetAllWeaponPurchased().Count; i++)
        {
            int idxWeaponPurchased = GameManager.Instance.GetWeaponPurchased(i);
            if(idxWeaponPurchased == GameManager.Instance.selectedWeaponIndex)
                GameManager.Instance.SetWeaponSelected(weaponData.GetWeapon(idxWeaponPurchased));
            weaponData.WeaponPurchased(idxWeaponPurchased);
        }
    }

    void SetAvatarData()
    {
        for (int i = 0; i < GameManager.Instance.GetAllAvatarPurchased().Count; i++)
        {
            int idAvatarPurchased = GameManager.Instance.GetAvatarPurchased(i);
            avatarData.AvatarPurchased(idAvatarPurchased);
        }
    }

    public void SetMapData()
    {
        for (int i = 0; i < GameManager.Instance.GetAllMapUnlock().Count; i++)
        {
            if (i == GameData.Instance.mapData.GetMapLength())
                return;
            int level = GameManager.Instance.GetMapUnlock(i);
            mapData.MapUnlock(level);
        }

        for (int i = 0; i < GameManager.Instance.GetAllMapWin().Count; i++)
        {
            int level = GameManager.Instance.GetMapWin(i);
            mapData.MapWin(level);
        }
    }
   

}
