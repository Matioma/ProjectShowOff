using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class IndicatorView : MonoBehaviour
{
    [SerializeField]
    Image indicatorImage;

    [SerializeField]
    Camera cam;



    public void Show(){
        indicatorImage.enabled = true;
    }

    public void Hide(){
        indicatorImage.enabled = false;
    }

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        this.transform.position = getRayPosition();
    }


    Vector3 getRayPosition() {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point + new Vector3(0,0.1f,0);
        }
        return new Vector3(0,0,0);
    }
}
