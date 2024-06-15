using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    [SerializeField] private GameObject[] maps;
    [SerializeField] private GameObject[] mapMusics;

    private int selectedMap;
    private void Start()
    {
        selectedMap = GameManager.Instance.GetSelectedMap();
        MapActive(selectedMap);
    }

    void MapActive(int level)
    {
        mapMusics[level].SetActive(true);
        maps[level].SetActive(true);
    }

}
