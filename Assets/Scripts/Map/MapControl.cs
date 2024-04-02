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
        switch (level)
        {
            case 0:
                maps[0].SetActive(true);
                break;
            case 1:
                maps[1].SetActive(false);
                break;
            case 2:
                maps[2].SetActive(true);
                break;

        }
    }

}
