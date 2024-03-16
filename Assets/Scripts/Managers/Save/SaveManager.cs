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
    }
    private void Start()
    {
        SaveSystem.Init();
        LoadSaveGame();
    }
    
    private string GetSaveName(string nameData)
    {
        return SaveManager.Save + "_" + nameData;
    }

    public void LoadSaveGame()
    {
        string stringSave = SaveSystem.Load(GetSaveName(""));
        if(stringSave != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(stringSave);
            
        }
    }

    public void SaveGame()
    {
        SaveObject saveObject = new SaveObject();
        string saveString = JsonUtility.ToJson(saveObject);
        SaveSystem.Save(GetSaveName(""), saveString);
    }

    class SaveObject
    {
        public int coin;
        public int[] amountGun;
        public string ak;
    }
}
