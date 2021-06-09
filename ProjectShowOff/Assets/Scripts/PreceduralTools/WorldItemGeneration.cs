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


enum RotationProperties{ 
    NoRotation,
    RandomRotationX,
    RandomRotationY,
    RandomRotationZ,
    RandomRotation,
    AlongNormal,
    AlongNormalRandomY

}




public class WorldItemGeneration : MonoBehaviour, IProcedural
{

    [SerializeField]
    RotationProperties rotation;
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
    [Range(1,100)]
    int densityReducer = 1;


    float[,] getPerlinMap() {
        float[,] newHeights = new float[perlinMapWidth, perlinMapDepth];

        for (int x = 0; x < perlinMapWidth; x++) {
            for (int z = 0; z < perlinMapDepth; z++) {
                newHeights[x, z] = getPerlinNoiseValue(x, z);
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
    }


    void clearChildren() {
        int childs = transform.childCount;

        for (int i = childs - 1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }


    public void Generate()
    {
        clearChildren();

        offsetX = Random.Range(0, 999.0f);
        offsetZ = Random.Range(0, 999.0f);

        float[,] perlinMap = getPerlinMap();
        for (int x = 0; x < perlinMapWidth; x += 1 * densityReducer)
        {
            for (int z = 0; z < perlinMapDepth; z += 1 * densityReducer)
            {
                if (perlinMap[x, z] > minValue)
                {

                    int assetId = Random.Range(0, assets.Count);
                    GameObject asset = Instantiate(assets[assetId], transform);

                    switch (rotation) {
                        case RotationProperties.NoRotation:
                            break;
                        case RotationProperties.RandomRotation:
                            asset.transform.rotation = Quaternion.Euler(Random.Range(-90.0f, 90f), Random.Range(-90.0f, 90.0f), Random.Range(-90.0f, 90.0f));
                            break;

                        case RotationProperties.RandomRotationX:
                            asset.transform.rotation = Quaternion.Euler(Random.Range(-90.0f, 90f), 0, 0);
                            break;
                        case RotationProperties.RandomRotationY:
                            asset.transform.rotation = Quaternion.Euler(0, Random.Range(-90.0f, 90f), 0);
                            break;
                        case RotationProperties.RandomRotationZ:
                            asset.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-90.0f, 90f));
                            break;
                        case RotationProperties.AlongNormal:
                            asset.transform.rotation = Quaternion.LookRotation(Vector3.forward, getNormal(x, z));
                            break;
                        case RotationProperties.AlongNormalRandomY:
                            asset.transform.rotation = Quaternion.LookRotation(Vector3.forward, getNormal(x, z));
                            Vector3 rotation = asset.transform.localEulerAngles;
                            asset.transform.localRotation = Quaternion.Euler(rotation.x, Random.Range(-90.0f, 90.0f), rotation.z);
                            break;
                    }

                    asset.transform.position = getGroundPosition(x, z);
                }
            }
        }
    }
    public void GenerateSameSeed()
    {
    }




    Vector3 getGroundPosition(float x, float z) {
        RaycastHit raycastHit;

        float positionY = 0;
        if (Physics.Raycast(new Vector3(x, 120, z), Vector3.down, out raycastHit)) {
            positionY = raycastHit.point.y;
        }
        return new Vector3(x, positionY, z);
    }

    Vector3 getNormal(float x, float z) {

        RaycastHit raycastHit;
        if (Physics.Raycast(new Vector3(x, 120, z), Vector3.down, out raycastHit))
        {
          return  raycastHit.normal;
        }
        return Vector3.up;
    }
}