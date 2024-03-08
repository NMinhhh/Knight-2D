using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public class LoadingMono : MonoBehaviour { }

    private static Action onLoaderCallBack;

    private static AsyncOperation asyncOperation;

    public static bool isLoadFill;

    public static void LoadScene(String scene)
    {
        onLoaderCallBack = () =>
        {
            GameObject loading = new GameObject();
            loading.AddComponent<LoadingMono>().StartCoroutine(LoadSceneAsync(scene));    
        };

        SceneManager.LoadScene(SceneIndexs.LOADING.ToString());
    }
    
    public static IEnumerator LoadSceneAsync(String scene)
    {
        yield return null;
        asyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            isLoadFill = true;
            yield return null;
        }
    }

    public static void AllowSceneActivation()
    {
        asyncOperation.allowSceneActivation = true;
        isLoadFill = false;
    }

    public static void LoaderCallBack()
    {

        if (onLoaderCallBack != null)
        {
            onLoaderCallBack();
            onLoaderCallBack = null;
        }
    }
}
