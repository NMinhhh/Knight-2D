using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMenu : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private CinemachineConfiner cam;
    [SerializeField] private List<Map> listMaps;
    [SerializeField] private GameObject map;
    public void OpenMap(int id)
    {
        for (int i = 0; i < listMaps.Count; i++)
        {
            int idx = i;
            if (listMaps[i].mapId == id)
            {
                //player.position = listMaps[i].pos.position;
                //cam.m_BoundingShape2D = listMaps[i].confiner;
                //CanvasManager.Instance.CloseUI(map);
                //listMaps[i].script.Notice(3);
                //return;
                
            }
        }
    }
    
    [System.Serializable]
    class Map
    {
        public int mapId;
        public bool isWin;
        public Transform pos;
        public PolygonCollider2D confiner;
        public MapControl script;
    }
}
