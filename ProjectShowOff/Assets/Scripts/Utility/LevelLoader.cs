using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static string loadingSceneName ="LoadLevelScene";

    public static void LoadLevel(string targetScene) {
        PlayerPrefs.SetString("TargetScene", targetScene);
        SceneManager.LoadScene(loadingSceneName);
    }

    void Start() {
        StartCoroutine(LoadLevelAsync());
    }


    IEnumerator LoadLevelAsync() {
        string targetScene = PlayerPrefs.GetString("TargetScene");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);

        while (!asyncLoad.isDone) {
            yield return null;
        }
    }

    public void OpenTargetScene() {
        
        //Scene
    }
}
