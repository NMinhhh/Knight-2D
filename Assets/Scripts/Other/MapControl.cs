using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{


    [SerializeField] private List<GameObject> spawner;
    [SerializeField] private GameObject warning;
    [SerializeField] private GameObject notice;
    [SerializeField] private float timeAppearBoss;
    private float timer;
    private bool isWin;
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < spawner.Count; i++)
        {
            if (spawner[i] == null)
            {
                spawner.RemoveAt(i);
            }
        }
        if(spawner.Count == 1)
        {
            timer += Time.deltaTime;
            warning.SetActive(true);
            if(timer >= timeAppearBoss)
            {
                warning.SetActive(false);
                spawner[0].SetActive(true);
            }
        }


    }

}
