using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RPGPlayerController : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    
    NavMeshAgent navMeshAgent;

    public void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                navMeshAgent.SetDestination(hit.point);
            }
        }
    }
}
