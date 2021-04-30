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
        navMeshAgent.updateRotation = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                // navMeshAgent.velocity = new Vector3();
                navMeshAgent.SetDestination(hit.point);
            }
        }
    }

    void LateUpdate()
    {
        // transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
    }
}
