using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public bool lockCursor;
    public Transform target;
    [SerializeField] private float mouseSensitivity = 10f;
    [SerializeField] private float distanceFromTarget = 2f;
    [SerializeField] Vector2 pitchMinMax = new Vector2(-10, 75);

    public float rotationSmoothTime = 0.12f;
    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;


    float yaw;
    float pitch;

    private void Start()
    {
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distanceFromTarget;
    }

}
