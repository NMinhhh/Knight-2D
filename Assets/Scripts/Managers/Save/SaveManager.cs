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
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
       SaveSystem.Init();
        LoadSaveGame();
    }

    private void OnApplicationQuit()
    {
        //SaveGame();
    }

    private string GetSaveName(string nameData)
    {
        return SaveManager.Save + "_" + nameData;
    }

    public void LoadSaveGame()
    {
        string stringSave = SaveSystem.Load(GetSaveName("CoinManager"));
        if(stringSave != null) 
        { 
            CoinManager.Instance.FromJson(stringSave);
        }
        else
        {
            CoinManager.Instance.AddWeaponPurchasedIndex(0);
        }
    }

    public void SaveGame()
    {
        
        string saveString = JsonUtility.ToJson(CoinManager.Instance);
        SaveSystem.Save(GetSaveName("CoinManager"), saveString);
    }

}
