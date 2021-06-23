using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCursorVisible : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
