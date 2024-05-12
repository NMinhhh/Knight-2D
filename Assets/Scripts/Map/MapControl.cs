using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    [SerializeField] private GameObject[] maps;

    private int selectedMap;
    private void Start()
    {
        selectedMap = CoinManager.Instance.GetSelectedMap();
        MapActive(selectedMap);
    }

    void MapActive(int level)
    {
        maps[level].SetActive(true);
    }

}
