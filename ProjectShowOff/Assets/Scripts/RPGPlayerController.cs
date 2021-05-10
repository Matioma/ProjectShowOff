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
    bool isCasting = false;


    IndicatorView indicatorView;

    public void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;

        indicatorView = FindObjectOfType<IndicatorView>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (isCasting) {
                CastSpell();
                return;
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                navMeshAgent.SetDestination(hit.point);
            }
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            StartCasting();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isCasting)
            {
                StopCasting();
            }
        }
    }

    void StartCasting() {
        isCasting = true;
        indicatorView.Show();
    }

    void StopCasting() {
        isCasting = false;
        indicatorView.Hide();
    }

    void CastSpell() {
        Debug.Log("Skill Used");
        StopCasting();
    }
}
