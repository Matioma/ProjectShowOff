using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayView : MonoBehaviour
{
    Player player;

    //[SerializeField]
    //Slider CheckPointsProgress;
    [SerializeField]
    Slider TrashProgress;
   

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        player.onCheckPointReached.AddListener(updateCheckPointProgress);
        player.onTrashCollected.AddListener(updateTrashCountProgress);
    }
    public void updateCheckPointProgress()
    {
       // CheckPointsProgress.value = player.getCheckPointProgress();
    }
    public void updateTrashCountProgress() {
        Debug.Log(player.getTrashPercent());
        TrashProgress.value = player.getTrashPercent();
    }

    private void OnDestroy()
    {
        player.onCheckPointReached.RemoveListener(updateCheckPointProgress);
        player.onTrashCollected.RemoveListener(updateTrashCountProgress);
    }
}
