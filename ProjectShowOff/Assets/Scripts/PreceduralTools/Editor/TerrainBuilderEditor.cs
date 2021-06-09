using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainBuilder))]
public class TerrainBuilderEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        TerrainBuilder terrainBuilder = (TerrainBuilder)target;

        if (GUILayout.Button("Build Terrain")) {
            terrainBuilder.Generate();
        }

        if (GUILayout.Button("Build Terrain Same Seed"))
        {
            terrainBuilder.GenerateSameSeed();
        }
    }
}
