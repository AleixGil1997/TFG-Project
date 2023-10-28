using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 10f;

    public float sensitivity = 2.0f; // Mouse sensitivity
    public float maxYAngle; // Max inclination angle up and down

    private Vector2 currentRotation = Vector2.zero;

    private GameObject playerCamera;
    private GameObject aboveCamera;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        maxYAngle = 90.0f; // (if I don't put it here, it doesn't work)

        playerCamera = GameObject.Find("Player Camera");
        aboveCamera = GameObject.Find("Above Player Camera Rotation Point");
    }

    // Update is called once per frame
    void Update()
    {
        // Player movement using arrows
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Camera rotation using mouse
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        currentRotation.x += mouseX * sensitivity;
        currentRotation.y -= mouseY * sensitivity; // Minus sign inverts mouse movement

        // Limits vertical rotation between -maxYAngle and +maxYAngle
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);

        // Apply horizontal rotation to the character and the camera
        transform.localRotation = Quaternion.AngleAxis(currentRotation.x, Vector3.up);

        // Adjusts the movement direction depending on the camera rotation
        Vector3 moveDirection = Quaternion.Euler(0, currentRotation.x, 0) * new Vector3(moveHorizontal, 0, moveVertical);
        rb.velocity = moveDirection * speed;

        // Apply vertical rotation only to the camera
        playerCamera.transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.right);
        aboveCamera.transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.right);
    }
}
