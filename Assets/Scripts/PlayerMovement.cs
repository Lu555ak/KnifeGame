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

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        groundCheck = this.gameObject.transform.GetChild(4);
    }

    // Update is called once per frame
    void Update()
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
