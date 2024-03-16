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
        File.WriteAllText(SAVE_FOLDER + saveName+".txt", saveString);
    }

    public static string Load(string saveName)
    {
        string file = SAVE_FOLDER + saveName + ".txt";
        if(File.Exists(file)) 
        {
            string saveString = File.ReadAllText(file);
            return saveString;
        }
        else
        {
            return null;
        }
    }
}
