using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(WorldItemGeneration))]
public class WorldPopulationEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        WorldItemGeneration terrainBuilder = (WorldItemGeneration)target;

        if (GUILayout.Button("Generate"))
        {
            terrainBuilder.Generate();
        }

        //if (GUILayout.Button("Build Terrain Same Seed"))
        //{
        //    terrainBuilder.GenerateSameSeed();
        //}
    }
}
