using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTransparent : MonoBehaviour
{
    Material initialMaterial;

    Dictionary<Renderer, Material> materials = new Dictionary<Renderer, Material>();

    private void Awake()
    {
        saveMaterials();
        MakeObjectTransparent();
    }

    void saveMaterials() {
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            materials.Add(renderer,renderer.material);
        }
    }

    public void MakeObjectTransparent() {
        Material transparentMaterial = Resources.Load("Material/Transparent", typeof(Material)) as Material;
  
        foreach (var keyValueMaterial in materials)
        {
            keyValueMaterial.Key.material = transparentMaterial;
        }
    }

    public void MakeObjectNormal() {
        foreach (var keyValueMaterial in materials)
        {
            keyValueMaterial.Key.material = keyValueMaterial.Value;
        }
        Destroy(this);
    }
}
