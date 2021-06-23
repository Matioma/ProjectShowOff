using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayView : MonoBehaviour
{
    Player player;

    [SerializeField]
    ProgressBar trashProgressBar;
    [SerializeField]
    ProgressBar checkPointProgressBar;


    [SerializeField]
    GameObject menuParent;

    private void Awake()
    {
        onGameUnPaused();
        player = FindObjectOfType<Player>();

        player.onCheckPointReached.AddListener(updateCheckPointProgress);
        player.onTrashCollected.AddListener(updateTrashCountProgress);
        player.onGamePaused.AddListener(onGamePaused);
        player.onGameUnPaused.AddListener(onGameUnPaused);
    }

    void onGamePaused() {
        menuParent?.gameObject.SetActive(true);
    }

    void onGameUnPaused() {
        menuParent.gameObject.SetActive(false);
    }

    public void updateCheckPointProgress()
    {
        if (checkPointProgressBar != null) checkPointProgressBar.Progress = player.getCheckPointProgress();
    }
    public void updateTrashCountProgress() {
        if(trashProgressBar != null) trashProgressBar.Progress = player.getTrashPercent();
    }

    private void OnDestroy()
    {
        player.onCheckPointReached.RemoveListener(updateCheckPointProgress);
        player.onTrashCollected.RemoveListener(updateTrashCountProgress);
        player.onGamePaused.RemoveListener(onGamePaused);
        player.onGameUnPaused.RemoveListener(onGameUnPaused);
    }

    public void ToggleAudio() {
        player.ToggleAudio();
    }

    public void TogglePause() {
        player.TogglePause();
    }

    public void RestartLevel() {
        LevelLoader.LoadLevel(SceneManager.GetActiveScene().name)
        //SceneManager.LoadScene();
        player.TogglePause();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
