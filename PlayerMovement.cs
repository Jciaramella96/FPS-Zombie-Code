using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float sprint_Value = 12f;
    public float speed = 12f;
    private float sprint = 20f;
    private float sprintDecriment = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    Vector3 velocity;
    bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    // Update is called once per frame
    void Update()
    {
        Sprint();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);


        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void Sprint()
    {
        if (sprint_Value > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
            {
                speed = sprint;
                sprint_Value -= sprintDecriment * Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = 12f;
            }
        }
        else
        {


        }
        if (sprint_Value != 15f)
        {
            sprint_Value += (sprintDecriment / 2f) * Time.deltaTime;
        }

        if (sprint_Value > 15f)
        {
            sprint_Value = 15f;
        }
    }







}//class
