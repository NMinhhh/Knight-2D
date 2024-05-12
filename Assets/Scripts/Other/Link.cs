using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    private bool leftApp;

    private void OnApplicationPause(bool pause)
    {
        leftApp = pause;
    }

    public void Facebook()
    {
        StartCoroutine(OpenFacebook());
    }

    IEnumerator OpenFacebook(){
        Application.OpenURL("fb://profile.php?id=100024719178336");
        yield return new WaitForSeconds(1);
        if (leftApp)
        {
            leftApp = false;
        }
        else
        {
            Application.OpenURL("https://www.facebook.com/profile.php?id=100024719178336");
        }
    }
}
