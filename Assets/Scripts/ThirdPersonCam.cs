using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public float sensX;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;

        yRotation += mouseX;

        // Rotate cam and oritentation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
