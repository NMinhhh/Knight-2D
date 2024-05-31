using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMenu : MonoBehaviour
{
    [SerializeField] private GameObject mapContent;

    private MapData data;

    private GameObject[] mapUI;

    void Start()
    {
        data = GameData.Instance.GetMapData();
        int amount = mapContent.transform.childCount;
        mapUI = new GameObject[amount];
        for (int i = 0; i < amount; i++)
        {
            mapUI[i] = mapContent.transform.GetChild(i).gameObject;
        }
        GenerateMapMenuUI();
    }

    private void GenerateMapMenuUI()
    {
        for (int i = 0; i < mapUI.Length; i++)
        {
            int idx = i;
            MapItemUI mapItemUI = mapUI[i].GetComponent<MapItemUI>();
            Map map = data.GetMap(idx);
            mapItemUI.SetLevelText("Level " + map.level);
            mapItemUI.OnMapLock();
            if (!map.isUnlock)
            {
                mapItemUI.OnMapLock();
            }
            else
            {
                mapItemUI.OnMapUnlock();
                mapItemUI.OnMapSelected(idx, MapSelected);
            }
        }
    }

    void MapSelected(int level)
    {
        Map map = data.GetMap(level);
        GameManager.Instance.SetMapState(map.isWin);
        GameManager.Instance.SetSelectedMap(level);
    }
}
