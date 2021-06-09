using System.Collections;
using System.Collections.Generic;
using UnityEngine;



enum OperationType { 
    Add,
    Subract,
    Multiply,
    Devide
}


[System.Serializable]
class PerlinNoiseParameters {
    [SerializeField]
    public OperationType operationType;

    [SerializeField]
    public float offsetX = 100f;
    [SerializeField]
    public float offsetZ = 100f;


    [SerializeField]
    public float scale = 20;

    [SerializeField]
    [Range(0,1)]
    public float prominance = 1;

}

public class TerrainBuilder : MonoBehaviour, IProcedural
{
    [SerializeField]
    int width = 256;
    [SerializeField]
    int depth = 256;

    [SerializeField]
    int height = 30;

    [SerializeField]
    List<PerlinNoiseParameters> perlinNoises;



    TerrainData getTerrainData(TerrainData terrainData) {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, height, depth);


        terrainData.SetHeights(0, 0, getCombinedPerlinNoises());
        return terrainData;
    }



    float[,] getCombinedPerlinNoises() {
        float[,] newHeights = new float[width, depth];

        float maxValue = 0;
        foreach (var parameter in perlinNoises){
            for (int x = 0; x < width; x++) {
                for (int z = 0; z < depth; z++) {
                    switch (parameter.operationType) {
                        case OperationType.Add:
                            newHeights[x, z] += getPerlinNoiseValue(x, z, parameter) * parameter.prominance;
                            break;
                        case OperationType.Subract:
                            newHeights[x, z] -= getPerlinNoiseValue(x, z, parameter) * parameter.prominance;
                            break;
                        case OperationType.Multiply:
                            newHeights[x, z] *= getPerlinNoiseValue(x, z, parameter) * parameter.prominance;
                            break;

                        case OperationType.Devide:
                            newHeights[x, z] /= getPerlinNoiseValue(x, z, parameter) * parameter.prominance;
                            break;
                    }
                    if (newHeights[x, z] > maxValue) maxValue = newHeights[x, z];
                }
            }        
        }

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                newHeights[x, z] = newHeights[x, z] / maxValue;
            }
        }
        return newHeights;
    }

    float getPerlinNoiseValue(int x, int z, PerlinNoiseParameters perameters ) {
        float xCoord = (float)x / width * perameters.scale + perameters.offsetX;
        float zCoord = (float)z / depth * perameters.scale + perameters.offsetZ;

        return Mathf.PerlinNoise(xCoord, zCoord);
    }


    //float[,] generateHeights(PerlinNoiseParameters perlinNoiseParameters) {
    //    float[,] newHeights = new float[width, depth];

    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int z = 0; z < depth; z++)
    //        {
    //            heights[x, z] = CalculateHeight(x, z, scale);
    //        }
    //    }
    //    return f
    //}


    //float[,] generateHeights(float[,] heights) {

    //    float[,] newHeights = heights;
    //    if (scale2 == 0) return newHeights;
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int z = 0; z < depth; z++)
    //        {
    //            newHeights[x, z] -= CalculateHeight(x, z, perlinNoises[1]);
    //        }
    //    }
    //    return newHeights;
    //}



    //float[,] generateHeights() {

    //    float[,] heights = new float[width, depth];
    //    for (int x = 0; x < width; x++) {
    //        for (int z = 0; z < depth; z++) {
    //            heights[x, z] = CalculateHeight(x, z, perlinNoises[0]);
    //         }
    //    }

    //    return heights;
    //}

    //float CalculateHeight(int x , int z,float scale) {
    //    float xCoord = (float)x / width * scale + offsetX;
    //    float zCoord = (float)z / depth * scale + offsetZ;

    //    return Mathf.PerlinNoise(xCoord, zCoord);
    //}


    float CalculateHeight(int x, int z, PerlinNoiseParameters pPerlinNoiseParam)
    {
        float xCoord = (float)x / width * pPerlinNoiseParam.scale + pPerlinNoiseParam.offsetX;
        float zCoord = (float)z / depth * pPerlinNoiseParam.scale + pPerlinNoiseParam.offsetZ;

        return Mathf.PerlinNoise(xCoord, zCoord);
    }
    public void Generate()
    {
        foreach(var data in perlinNoises)
        {
            data.offsetX = Random.Range(0, 9999f);
            data.offsetZ = Random.Range(0, 9999f);
        }
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = getTerrainData(terrain.terrainData);
    }

    public void GenerateSameSeed()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = getTerrainData(terrain.terrainData);
    }
}
