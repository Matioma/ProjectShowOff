using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(PlatformTransition))]
public class PlatformTransitionEditor : Editor
{
    public void OnSceneGUI()
    {
        var t = target as PlatformTransition;
        //t.targetPosition = Handles.PositionHandle()


        EditorGUI.BeginChangeCheck();
        Vector3 newTargetPosition = Handles.PositionHandle(t.targetPosition, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(t, "Change Look At Target Position");
            t.targetPosition = newTargetPosition;
          
        }

        //var tr = t.transform;
        //var pos = tr.position;
        // display an orange disc where the object is
        //var color = new Color(1, 0.8f, 0.4f, 1);
        //Handles.color = color;
        //Handles.DrawWireDisc(pos, tr.up, 1.0f);
        //// display object "value" in scene
        //GUI.color = color;
        //Handles.Label(pos, t.value.ToString("F1"));
    }
}
