using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 10f; // Added turn speed variable
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    private Vector3 gravityDirection = Vector3.down;

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

        //Ground Check
        if(!Physics.Raycast(transform.position + transform.up * 0.1f, - transform.up, groundDistance, groundLayer))
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }

        SwitchGravity();

    }

    public bool IsRunning()
    {
        return isRunning;
    }

    public bool IsFalling()
    {
        return isFalling;
    }

    private void SwitchGravity()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            ChangeGravityDirection(Vector3.right);
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            ChangeGravityDirection(Vector3.left);
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            ChangeGravityDirection(Vector3.forward);
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            ChangeGravityDirection(Vector3.back);
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    void ChangeGravityDirection(Vector3 direction)
    {
        gravityDirection = direction.normalized;
        Physics.gravity = gravityDirection * 9.81f; // Adjust the gravity strength as needed
        rb.rotation = Quaternion.FromToRotation(transform.up, -gravityDirection) * transform.rotation;
    }

}
