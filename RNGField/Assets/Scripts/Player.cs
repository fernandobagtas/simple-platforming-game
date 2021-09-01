using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //having [SerializeField] here will expose the setting in the unity Inspector
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;

    private bool SpaceKeyPressed;

    private float horizontalInput;

    private Rigidbody rigidbodyComponent;

    private int superJumpsRemaining;
    private int doubleJumpsRemaining;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceKeyPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    // FixedUpdate is called once every physics update
    void FixedUpdate()
    {
        //Horizontal movement
        //This is first in this method because we want to keep the horizontal movement while in the air. If for instance this is put after the collider check, then it will exit out of this method and never update the horizontal movement while in the air
        rigidbodyComponent.velocity = new Vector3(horizontalInput * 2, rigidbodyComponent.velocity.y, 0);
        //Debug.Log(rigidbodyComponent.velocity);

        //Check if the player's feet is not colliding with any layer that's not the player or equipments (AKA in the air) 
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            
            //Debug.Log("doubleJumpsRemaining = " + doubleJumpsRemaining);
            if (doubleJumpsRemaining == 0)
            {
                return;
            }
            else { }
        }

        if (SpaceKeyPressed)
        {
            Debug.Log("space pressed. Velocity:" + ForceMode.VelocityChange);


            //jump force
            float jumpPower = 5.5f;
            if (superJumpsRemaining > 0)
            {
                jumpPower *= 2;
                superJumpsRemaining--;
            }

            if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0 && doubleJumpsRemaining > 0)
            {
                doubleJumpsRemaining--;
                rigidbodyComponent.velocity = new Vector3(rigidbodyComponent.velocity.x, 0); //reset y velocity to 0 so the jump doesn't get affected by any existing forces like gravity
            }

            //jump code
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            SpaceKeyPressed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Coin layer = 6
        //if player collides with coin, destroy coin and increase super jump instance
        switch (other.gameObject.layer)
        {
            //Coin layer = 6
            //if player collides with coin, destroy coin and increase super jump instance
            case 6:
                Destroy(other.gameObject);
                superJumpsRemaining++;
                break;

            //Orb layer = 7
            //if player collides with orb, destroy orb and increase double jump instance
            case 7:
                Destroy(other.gameObject);
                doubleJumpsRemaining++;
                break;

            default:
                break;
        }
    }
}
