using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    private const string Save = "Save";
    void Awake()
    { 
        if(Instance == null)
        {
            Instance = this;
            LoadSaveGame();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        SaveSystem.Init();
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause)
            SaveGame();
    }

    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
            SaveGame();
    }

    public string GetSaveName(string nameData)
    {
        return SaveManager.Save + "_" + nameData;
    }

    public void LoadSaveGame()
    {
        string stringSave = SaveSystem.Load(GetSaveName("GameManager"));
        if(stringSave != null) 
        { 
            GameManager.Instance.FromJson(stringSave);
        }
        else
        {
            GameManager.Instance.AddWeaponPurchasedIndex(0);
            GameManager.Instance.AddMapUnlock(0);
            GameManager.Instance.AddAvatarPurchased(0);
            GameManager.Instance.AddCoin(1000000);
            GameManager.Instance.AddDiamond(1000000);
        }
    }

    public void SaveGame()
    {
        string saveString = JsonUtility.ToJson(GameManager.Instance);
        SaveSystem.Save(GetSaveName("GameManager"), saveString);
    }

    public void ResetGame()
    {
        string stringSave = SaveSystem.Load(GetSaveName("GameManager"));
        if (stringSave != null)
        {
            SaveSystem.DeleteFileSave(GetSaveName("GameManager"));
        }
        GameManager.Instance.ResetValue();
        LoadSaveGame();
        GameData.Instance.GetAvatarData().ResetAvatar();
        GameData.Instance.GetMapData().ResetMap();
        GameData.Instance.GetWeaponData().ResetWeaponData();
        GameData.Instance.SetDataGame();
    }
}
