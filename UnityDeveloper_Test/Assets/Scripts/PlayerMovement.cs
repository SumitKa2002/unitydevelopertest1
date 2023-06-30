using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float turnSpeed = 10f; // Added turn speed variable
    private Rigidbody rb;
    private Transform cameraT;
    private bool isRunning = false;
    private bool isFalling = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraT = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        float horizontal = 0f;
        float vertical = 0f;

        // Check for WASD key inputs
        if (Input.GetKey(KeyCode.W))
            vertical = 1f;
        else if (Input.GetKey(KeyCode.S))
            vertical = -1f;
        if (Input.GetKey(KeyCode.A))
            horizontal = -1f;
        else if (Input.GetKey(KeyCode.D))
            horizontal = 1f;

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDirection = cameraT.TransformDirection(direction);
            moveDirection.y = 0f;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);

            rb.MovePosition(transform.position + moveDirection.normalized * moveSpeed * Time.fixedDeltaTime);
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    public bool IsRunning()
    {
        return isRunning;
    }




    /*
    private Transform cameraT;

    private CharacterController controller;
    private bool isRunning = false;
    private bool isFalling = false;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravityMultiplier = 3.0f;

    private float velocity;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private float gravity = -9.81f;





    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraT = Camera.main.transform;
    }

    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        // Check for WASD key inputs
        if (Input.GetKey(KeyCode.W))
            vertical = 1f;
        else if (Input.GetKey(KeyCode.S))
            vertical = -1f;

        if (Input.GetKey(KeyCode.A))
            horizontal = -1f;
        else if (Input.GetKey(KeyCode.D))
            horizontal = 1f;

        if (controller.isGrounded && velocity < 0.0f)
        {
            velocity = -1.0f;
        }
        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
        }

        Vector3 direction = (cameraT.right * horizontal + transform.up * velocity + cameraT.forward * vertical).normalized;
        
        controller.Move(direction * speed * Time.deltaTime);

        /*if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    

    public bool IsRunning()
    {
        return isRunning;
    }

    public bool IsFalling()
    {
        if (!controller.isGrounded)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
        return isFalling;
    }*/

}
