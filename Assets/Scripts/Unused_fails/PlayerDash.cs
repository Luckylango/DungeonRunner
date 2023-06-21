 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDash : MonoBehaviour

{
    public bool isDashing;

    private int dashAttempts;
    private float dashStartTime;

    PlayerMovement playerController;
    public Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        HandleDash();
    }

    void HandleDash()
    {
        bool isTryingToDash = Input.GetKeyDown(KeyCode.E);
        
        if (isTryingToDash && !isDashing)
        {
            if (dashAttempts <=300000)
            {
                OnStartDash();
            }
        }

        if (isDashing)
        {
            if (Time.time - dashStartTime <= 0.45f)
            {
                if (playerController.movementVector.Equals(Vector3.zero))
                {
                    // Player is not giving any input, just dash forward
                   rb.AddForce(transform.forward * 300f * Time.deltaTime, ForceMode.Impulse);
                }

                else
                { 
                    rb.AddForce(playerController.movementVector.normalized * 300f * Time.deltaTime, ForceMode.Impulse);
                }
            }

            else
            {
                OnEndDash();
            }
        }
    }   

    void OnStartDash()
    {
        isDashing = true;
        dashStartTime = Time.time;
        dashAttempts += 1;
    }

    void OnEndDash()
    {
        isDashing = false;
        dashStartTime = 0;
    }
}
