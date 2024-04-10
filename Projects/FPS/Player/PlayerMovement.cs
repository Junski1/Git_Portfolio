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
    [SerializeField] float dashForce = 10f;

    public float speedMultiplier = 1f;

    public float gravity = -9.81f*2;
    [SerializeField] CharacterController controller;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

   public int jumpCount = 0;

    float dashTimer =0.5f;
    float dashTimerMax = 0f;

    public bool isGrounded;

    Vector3 velocity;

    WeaponSwitching Gun;

    Katana katana;
    // Start is called before the first frame update
    // Update is called once per frame


    void Start()
    {
        Gun = gameObject.GetComponentInChildren<WeaponSwitching>();
        katana = GetComponentInChildren<Katana>();
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
        if (Gun.selectedWeapon== 1)
        {
            Dash();
        }
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
            jumpCount = 0;
        }
    }
    void Jump()
    {
        int maxJumps =0;

        if (Gun.selectedWeapon == 0)
        {
            maxJumps = 1;
            
        }
        else if( Gun.selectedWeapon == 1)
        {
            maxJumps = 2;
        }

        if (!isGrounded&&jumpCount<1)
        {
            jumpCount++;
        }

        if (isGrounded && Input.GetButtonDown("Jump")||jumpCount<maxJumps&& Input.GetButtonDown("Jump"))
        {
            
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                jumpCount++;
        }
    }
    
    void Dash()
    {
        
        if (Time.time >= dashTimerMax && Input.GetKeyDown(KeyCode.LeftShift)&& katana.canDash)
        {
            dashTimerMax = Time.time + 1 / dashTimer;
            if (direction == transform.forward|| direction.magnitude == 0)
            {
                katana.Dash();
            }
            
            if(direction.magnitude == 0)
            {
                controller.Move(gameObject.transform.forward * dashForce);
            }
            else
            {
                controller.Move(direction * dashForce);
            }
            
        }
    }
}
