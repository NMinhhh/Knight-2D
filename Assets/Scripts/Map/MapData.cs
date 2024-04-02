using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMapData",menuName ="Map/ Map Data Base")]
public class MapData : ScriptableObject
{
    public Map[] maps;

    public int GetMapLength()
    {
        return maps.Length;
    }

    public Map GetMap(int level)
    {
        return maps[level];
    }

    public void MapUnlock(int level)
    {
        maps[level].isUnlock = true;
    }

    public void MapWin(int level)
    {
        maps[level].isWin = true;
    }

}
