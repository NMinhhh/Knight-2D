using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem 
{
    public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
    // Start is called before the first frame update
    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string saveName,string saveString)
    {
        File.WriteAllText(SAVE_FOLDER + saveName, saveString);
    }

    public static string Load(string saveName)
    {
        if(File.Exists(SAVE_FOLDER + saveName)) 
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + saveName);
            return saveString;
        }
        else
        {
            return null;
        }
    }
}
