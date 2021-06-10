using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Optimizer))]
public class OptimizerTool : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Optimizer terrainBuilder = (Optimizer)target;

        if (GUILayout.Button("MergeAssets"))
        {
            terrainBuilder.Optimize();
        }
    }
}
