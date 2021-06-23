using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoController : MonoBehaviour
{
    public UnityEvent onVideoEnded;
    private VideoPlayer vPlayer;

    private void Awake()
    {
        vPlayer = GetComponent<VideoPlayer>();
        vPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        onVideoEnded?.Invoke();
        //vp.playbackSpeed = vp.playbackSpeed / 10.0F;
    }

}
