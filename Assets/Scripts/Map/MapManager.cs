using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private MapData data;
    [SerializeField] private GameObject mapContent;

    [SerializeField] private GameObject[] mapUI;
    // Start is called before the first frame update
    void Start()
    {
        int amount = mapContent.transform.childCount;
        mapUI = new GameObject[amount];
        for (int i = 0; i < amount; i++)
        {
            mapUI[i] = mapContent.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < CoinManager.Instance.GetAllMapUnlock().Count; i++)
        {
            int level = CoinManager.Instance.GetMapUnlock(i);
            data.MapUnlock(level);
        }

        for (int i = 0; i < CoinManager.Instance.GetAllMapWin().Count; i++)
        {
            int level = CoinManager.Instance.GetMapWin(i);
            data.MapWin(level);
        }

        for (int i = 0;i < mapUI.Length; i++)
        {
            int idx = i;
            MapItemUI mapItemUI = mapUI[i].GetComponent<MapItemUI>();
            Map map = data.GetMap(idx);
            mapItemUI.SetLevelText("Level " + map.level);
            mapItemUI.OnMapLock();
            if(!map.isUnlock)
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
        CoinManager.Instance.SetMapState(map.isWin);
        CoinManager.Instance.SetSelectedMap(level);
    }
}
