using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static string loadingSceneName = "LoadLevelScene";

    public static void LoadLevel(string targetScene) {
        PlayerPrefs.SetString("TargetScene", targetScene);
        SceneManager.LoadScene(loadingSceneName);
    }

    void Start() {
        Invoke("StartLoading", 1.0f);
    }
    private void Update()
    {
        Debug.Log(Time.deltaTime);
    }


    private void StartLoading() {
        StartCoroutine(LoadLevelAsync());
    }
    IEnumerator LoadLevelAsync() {
        string targetScene = PlayerPrefs.GetString("TargetScene");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);

        while (!asyncLoad.isDone) {
            Debug.Log($"progress : {asyncLoad.progress}");
            yield return null;
        }
    }

    public static void DirectlyLoad(string target) {
        SceneManager.LoadScene(target);
    }
}
