using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : WASDController
{

    public float jumpSpeed = 8.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SpecialAction()
    {
        if (controller.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
    }
}
