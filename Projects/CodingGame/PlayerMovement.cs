using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float movementX;
    float movementZ;
    Vector3 direction;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpHeight = 1f;

    public float speedMultiplier = 1f;

    public float gravity = -9.81f*2;
    [SerializeField] CharacterController controller;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    public bool isGrounded;

    Vector3 velocity;
    // Start is called before the first frame update
    // Update is called once per frame


    void Start()
    {
        speedMultiplier = 1;
    }
    void Update()
    {
        Grounded();
        movementX = Input.GetAxis("Horizontal");
        movementZ = Input.GetAxis("Vertical");

        direction = transform.right * movementX + transform.forward * movementZ;

        velocity.y += gravity * Time.deltaTime;

        walk(direction);
        Jump();
  
        controller.Move(velocity * Time.deltaTime);
    }
    void walk(Vector3 move)
    {
         controller.Move(move * speed*speedMultiplier*Time.deltaTime);
    }
    void Grounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded&& velocity.y < 0)
        {
           velocity.y = -2f;
        }
    }
    void Jump()
    {

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
