using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerDashing : MonoBehaviour
{
    Rigidbody rig;
    CapsuleCollider collider;

    float originalHeight;

    [Header("Sliding")]
    public float reducedHeight;
    public float slideSpeed = 10f;

    [Header("Dashing")]
    public bool isLeftDashing;
    public bool isRightDashing;
    public float dashSpeed;

    private PlayerMovement pM;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        pM = GetComponent<PlayerMovement>();
        collider = GetComponent<CapsuleCollider>();
        originalHeight = originalHeight = collider.height;
    }

    // Update is called once per frame
    // The actual inputs to perform the action wanted
    void Update()
    {
        if (pM.grounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                isLeftDashing = true;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                isRightDashing = true;
            }
            if (Input.GetKeyDown(KeyCode.W))
                Sliding();
            else if (Input.GetKeyUp(KeyCode.W))
                GoUp();
        }
    }

    // Sliding mechanic
    private void Sliding()
    {

        collider.height = reducedHeight;
        rig.AddForce(transform.forward * slideSpeed, ForceMode.VelocityChange);
    }

    private void GoUp()
    {
        collider.height = originalHeight;
    }


    // Dashing mechanic
    private void FixedUpdate()
    {
        if (isLeftDashing)
            LDashing();
        if (isRightDashing)
            RDashing();
    }

    private void LDashing()
    {
        rig.AddForce(transform.right * dashSpeed, ForceMode.Force);
        isLeftDashing = false;
    }
    private void RDashing()
    {
        rig.AddForce(-transform.right * dashSpeed, ForceMode.Force);
        isRightDashing = false;
    }
}
