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

    private void Awake()
    {
        player = FindObjectOfType<Player>();

        player.onCheckPointReached.AddListener(updateCheckPointProgress);
        player.onTrashCollected.AddListener(updateTrashCountProgress);
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
    }

    public void ToggleAudio() {
        player.ToggleAudio();
    }

    public void TogglePause() {
        player.TogglePause();
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
