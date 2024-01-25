using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GlideLocomotion : LocomotionProvider
{
    public Transform rigRoot;
    public Transform trackedTransform; // camera or controller, null for thumbstick

    public float velocity = 2f; // meters per second
    public float rotationSpeed = 100f; // degrees per second

    private bool isMoving;

    private void Start()
    {
        if (rigRoot == null)
            rigRoot = transform;
    }

    private void Update()
    {
        if (!isMoving && !CanBeginLocomotion())
            return;

        float forward = Input.GetAxis("XRI_Right_Primary2DAxis_Vertical");
        float sideways = Input.GetAxis("XRI_Right_Primary2DAxis_Horizontal");
        if (forward == 0f && sideways == 0f)
        {
            isMoving = false;
            EndLocomotion();
            return;
        }

        if (!isMoving)
        {
            isMoving = true;
            BeginLocomotion();
        }

        if (forward != 0f)
        {
            Vector3 moveDirection = Vector3.forward;
            if (trackedTransform != null)
            {
                moveDirection = trackedTransform.forward;
                moveDirection.y = 0f;
            }

            moveDirection *= -forward * velocity * Time.deltaTime;
            rigRoot.Translate(moveDirection);
        }

        if (trackedTransform == null && sideways != 0f)
        {
            float rotation = sideways * rotationSpeed * Time.deltaTime;
            rigRoot.Rotate(0, rotation, 0);
        }
    }



    //public Transform trackedTransform; // camera or controller, null for thumbstick
    //public Transform rigRoot;
    //public float velocity = 2f; // meters per second
    //public float rotationSpeed = 100f; // degrees per second

    //private void Start()
    //{
    //    if (rigRoot == null)
    //        rigRoot = transform;
    //}

    //private void Update()
    //{
    //    float vertical = Input.GetAxis("XRI_Right_Primary2DAxis_Vertical");
    //    if (vertical != 0f)
    //    {
    //        Vector3 moveDirection = Vector3.forward; // trackedTransform.forward;
    //        moveDirection.y = 0;
    //        moveDirection *= -vertical * velocity * Time.deltaTime;
    //        rigRoot.Translate(moveDirection);
    //    }

    //    float horizontal = Input.GetAxis("XRI_Right_Primary2DAxis_Horizontal");
    //    if (horizontal != 0f)
    //    {
    //        float rotation = horizontal * rotationSpeed * Time.deltaTime;
    //        rigRoot.Rotate(0, rotation, 0);
    //    }
    //}
}
