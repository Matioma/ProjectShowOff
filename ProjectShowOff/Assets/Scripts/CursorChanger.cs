using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public Texture2D crosshair;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    void Awake()
    {
        SetCursor(crosshair);   
    }

    public void SetCursor(Texture2D crosshair) {
        Cursor.SetCursor(crosshair, hotSpot, cursorMode);
    }
}
