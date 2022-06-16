using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 12f;
    [SerializeField]
    private float gravity = -9.1f;
    [SerializeField]
    private float groundDistance = 0.4f;
    [SerializeField]
    private float jumpHeight = 2f;
    [SerializeField]
    private LayerMask groundMask;

    private float x;
    private float z;
    private CharacterController controller;
    private Vector3 velocity;
    private Transform groundCheck;
    private bool onGround;


    private void Start()
    {
        Screen.lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        groundCheck = transform.GetChild(2);
    }

    private void Update()
    {
        onGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(onGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && onGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
