using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public void Load(string name)
    {
        CanvasManager.Instance.LoadScene(name);
    }
}
