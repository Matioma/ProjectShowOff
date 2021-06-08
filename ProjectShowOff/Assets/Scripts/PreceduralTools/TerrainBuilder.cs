using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBuilder : MonoBehaviour, IProcedural
{
    [SerializeField]
    int width = 256;
    [SerializeField]
    int depth = 256;

    [SerializeField]
    int height = 30;

    [SerializeField]
    float scale = 20;

    [SerializeField]
    float offsetX = 100f;
    [SerializeField]
    float offsetZ = 100f;

    TerrainData getTerrainData(TerrainData terrainData) {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, height, depth);

        terrainData.SetHeights(0, 0, generateHeights());
        return terrainData;
    }

    float[,] generateHeights() {
        float[,] heights = new float[width, depth];
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < depth; z++) {
                heights[x, z] = CalculateHeight(x, z);
             }
        }

        return heights;
    }

    float CalculateHeight(int x , int z) {
        float xCoord = (float)x / width * scale + offsetX;
        float zCoord = (float)z / depth * scale + offsetZ;

        return Mathf.PerlinNoise(xCoord, zCoord);
    }
    public void Generate()
    {
        offsetX = Random.Range(0, 9999f);
        offsetZ = Random.Range(0, 9999f);

        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = getTerrainData(terrain.terrainData);
    }
}
