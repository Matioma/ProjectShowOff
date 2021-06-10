using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Optimizer : MonoBehaviour
{

    private void Start()
    {
        
    }

    public void Optimize() {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();


        //List<Mesh> mesh = GroupedMeshes(meshFilters);

        //Debug.Log(mesh.Count);

        //foreach (var m in mesh)
        //{
        //    GameObject gameObject = new GameObject();
        //    gameObject.transform.parent = this.transform;
        //    gameObject.AddComponent<MeshFilter>();
        //    gameObject.AddComponent<MeshRenderer>();

        //    gameObject.transform.GetComponent<MeshFilter>().sharedMesh = m;

        //    //filter.sharedMesh = m;
        //    gameObject.SetActive(true);
        //}

        ////Debug.Log(meshFilters.Length);
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];



        int i = 1;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }

        transform.GetComponent<MeshFilter>().sharedMesh = new Mesh();
        transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);

        transform.gameObject.SetActive(true);
    }


    List<Mesh> GroupedMeshes(MeshFilter[] meshes)
    {
        var GroupedMeshesById = new Dictionary<int, List<CombineInstance>>();

        // Group each instance of mesh in a dictionary
        foreach (var m in meshes)
        {
            Debug.Log(m.sharedMesh);

            if (GroupedMeshesById.ContainsKey(m.sharedMesh.GetInstanceID()))
            {
                CombineInstance combineInstance = new CombineInstance();
                combineInstance.mesh = m.sharedMesh;
                GroupedMeshesById[m.sharedMesh.GetInstanceID()].Add(combineInstance);
            }
            else {
                GroupedMeshesById.Add(m.sharedMesh.GetInstanceID(), new List<CombineInstance>());

                CombineInstance combineInstance = new CombineInstance();
                combineInstance.mesh = m.sharedMesh;

                GroupedMeshesById[m.sharedMesh.GetInstanceID()].Add(combineInstance);
            }
            

            //if (!GroupedMeshesById.TryGetValue(m.sharedMesh.GetInstanceID(), out tmp)) { 
                
            //}


            //if (!GroupedMeshesById.TryGetValue(m.sharedMesh.GetInstanceID(), out tmp))
            //{
            //    tmp = new List<CombineInstance>();
            //    GroupedMeshesById.Add(m.sharedMesh.GetInstanceID(), tmp);
            //}

            //var ci = new CombineInstance();
            //ci.mesh = m.sharedMesh;
            //ci.transform = m.transform.localToWorldMatrix;
            //tmp.Add(ci);
        }


        List<Mesh> GroupedMeshes = new List<Mesh>();

        foreach (var group in GroupedMeshesById) {
            Mesh mesh = new Mesh();
            mesh.CombineMeshes(group.Value.ToArray());
            GroupedMeshes.Add(mesh);
        }






        //// Combine meshes and build combine instance for combined meshes
        //var list = new List<CombineInstance>();
        //foreach (var e in helper)
        //{
        //    var m = new Mesh();
        //    m.CombineMeshes(e.Value.ToArray());
        //    var ci = new CombineInstance();
        //    ci.mesh = m;
        //    list.Add(ci);
        //}

        //// And now combine everything
        //var result = new Mesh();
        //result.CombineMeshes(list.ToArray(), false, false);

        ////// It is a good idea to clean unused meshes now
        ////foreach (var m in list)
        ////{
        ////    DestroyImmediate(m.mesh);
        ////}
        ///

        return GroupedMeshes;
    }
}
