using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plugins.Controllers;

namespace SunnyLand
{
    public class Player : MonoBehaviour
    {
        public float gravity = -25f;
        public float runSpeed = 8f;
        public float groundDamping = 20f; // how fast do we change direction?
        public float inAirDamping = 5f;
        public float jumpHeight = 3f;

        private CharacterController2D controller; //the player controller
        private Animator anim; //the animator
        private SpriteRenderer rend; //the sprite renderer
        private Vector3 velocity; // calculate velocity

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();//gets the animator
            rend = GetComponent<SpriteRenderer>();//gets the sprite renderer
            controller = GetComponent<CharacterController2D>();//gets the controller
        }

        // Update is called once per frame
        void Update()
        {
            //if the player is grounded
            if (controller.isGrounded)
            {
                //resets the y velocity
                velocity.y = 0f;
            }
            float inputH = Input.GetAxis("Horizontal");//left and/or right (A or D)
            float inputV = Input.GetAxis("Vertical");//Up and/or down (W or S)
            //if button is pressed (horizontal)
            if(inputH != 0)
            {
                //Check what direction the sprite should be flipped
                rend.flipX = inputH < 0;
            }
            //Move Horizontall
            velocity.x = inputH * runSpeed;
            if (controller.isGrounded && Input.GetButtonDown("Jump"))
            {

                velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
                Debug.Log("Jump Willy!");
            }

            //Apply Gravity
            velocity.y += gravity * Time.deltaTime;

            //Apply Velocity to controller
            controller.Move(velocity * Time.deltaTime);//moves character controller

            velocity = controller.velocity;

            UpdateAnim();
        }
        void UpdateAnim()
        {
            anim.SetBool("IsGrounded", controller.isGrounded);

            anim.SetFloat("JumpY", controller.velocity.normalized.y);

            anim.SetBool("IsRunning", controller.velocity.x != 0);
        }
    } 
}
