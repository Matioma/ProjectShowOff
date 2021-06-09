using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WorldPlacementSettings {
    [SerializeField]
    List<GameObject> assets;

    [SerializeField]
    float perlinScale = 1;

    [SerializeField]
    float offsetX;
    [SerializeField]
    float offsetZ;
}




public class WorldItemGeneration : MonoBehaviour, IProcedural
{
    [SerializeField]
    List<GameObject> assets;

    [SerializeField]
    int perlinMapWidth;
    [SerializeField]
    int perlinMapDepth;

    [SerializeField]
    float scale = 1;

    [SerializeField]
    float offsetX;
    [SerializeField]
    float offsetZ;

    [Range(0, 1)]
    [SerializeField]
    float minValue = 0.5f;

    [SerializeField]
    int densityReducer = 1;


    float[,] getPerlinMap() {
        float[,] newHeights = new float[perlinMapWidth, perlinMapDepth];

        for (int x = 0; x < perlinMapWidth; x++) {
            for (int z = 0; z < perlinMapDepth; z++) {
                newHeights[x,z] = getPerlinNoiseValue(x, z);
            }
        }
        return newHeights;
    }
    float getPerlinNoiseValue(int x, int z)
    {
        float xCoord = (float)x / perlinMapWidth * scale + offsetX;
        float zCoord = (float)z / perlinMapDepth * scale + offsetZ;

        return Mathf.PerlinNoise(xCoord, zCoord);
    }


    void Start() {
        Generate();
    }


    void clearChildren() {
        int childs = transform.childCount;

        for (int i = childs-1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }


    public void Generate()
    {
        clearChildren();

        offsetX =Random.Range(0, 999.0f);
        offsetZ = Random.Range(0, 999.0f);

        float[,] perlinMap = getPerlinMap();
        for (int x = 0; x < perlinMapWidth; x += 1 * densityReducer)
        {
            for (int z = 0; z < perlinMapDepth; z += 1 * densityReducer)
            {

                Debug.Log(x + " = " + perlinMap[x, z]);
                if (perlinMap[x, z] > minValue)
                {

                    GameObject asset = Instantiate(assets[0], transform);
                    asset.transform.rotation = Quaternion.Euler(Random.Range(-90.0f, 90f), Random.Range(-90.0f, 90.0f), Random.Range(-90.0f, 90.0f));
                    asset.transform.position = getGroundPosition(x,z);
                    
                }
            }
        }
    }
    public void GenerateSameSeed()
    {
    }


    Vector3 getGroundPosition(float x, float z) {
        RaycastHit raycastHit;

        float positionY =0;
        if (Physics.Raycast(new Vector3(x, 120, z),Vector3.down, out raycastHit)){
            positionY = raycastHit.point.y;
        }
        return new Vector3(x, positionY, z);       
    }
}
