using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    
    Vector3 moveDirection;

    Rigidbody rb;
   public Vector3 movementVector;

    void start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        readyToJump = true;

        // this is to keep the player from just falling over
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            Debug.Log("Player tried to jump");
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else 
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        /*
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        } */
    }

    private void MovePlayer()
    {
        // calculate movement direction
        transform.position = transform.position + Camera.main.transform.forward * moveSpeed * Time.deltaTime;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);  

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {

            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            Vector3 tempVelocity = rb.velocity;
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        Debug.Log(rb.velocity);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        float y = rb.velocity.y;
        Debug.Log("Y is " + y);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
