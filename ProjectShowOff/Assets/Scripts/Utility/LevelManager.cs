using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    string targetScene;
    public void LoadLevel() {
        LevelLoader.LoadLevel(targetScene);
    }
}
