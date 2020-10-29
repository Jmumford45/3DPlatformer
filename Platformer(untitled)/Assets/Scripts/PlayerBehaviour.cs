using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //a reference to our player's rigidbody component
    private Rigidbody rigidBody;
    //force to apply when player jumps
    public Vector2 jumpForce = new Vector2(0, 450);
    //how fast the player moves along the x-axis
    public float maxSpeed = 3.0f;
    //a modifier to the force applied
    public float speed = 50.0f;
    //The force to apply that we will get for the player's movement
    private float xMove;
    //set to true when the player can jump
    private bool shouldJump;
    private bool collidingWall;

    private bool onGround;
    private float yPrevious;

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + rigidBody.velocity, Color.red);
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        shouldJump = false;
        xMove = 0.0f;
        onGround = false;
        yPrevious = Mathf.Floor(transform.position.y);
        collidingWall = false;

    }

    private void FixedUpdate()
    {
        //move the player left and right
        Movement();

        //sets the camera to center on the player's position
        //keeping the camera's original depth
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, 
            Camera.main.transform.position.z);
    }

    private void Update()
    {
        //check if player is Grounded
        CheckGrounded();
        Jumping();
    }

    void CheckGrounded()
    {
        //check if the player is hitting something from the center of the object (origin) to slighlty below the bottome of it (distance)
        float distance = (GetComponent<CapsuleCollider>().height / 2 * this.transform.localScale.y) + .01f;
        Vector3 floorDirection = transform.TransformDirection(-Vector3.up);
        Vector3 origin = transform.position;

        if(!onGround)
        {
            //Check if there is something directly below us 
            if (Physics.Raycast(origin, floorDirection, distance))
            {
                onGround = true;
            }
        }
        //if we are currently grounded, are we falling down or jumping?
        else if(Mathf.Floor(transform.position.y) != yPrevious)
        {
            onGround = false;
        }
        //our current position will be our previous next frame
        yPrevious = Mathf.Floor(transform.position.y);
    }

    //If we hit something and we are not grounded, it must be a wall or a ceiling
    void OnCollisionStay(Collision collision)
    {
        if(!onGround)
        {
            collidingWall = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        collidingWall = false;
    }

    void Jumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            shouldJump = true;
        }
        //if the player should jump
        if(shouldJump && onGround)
        {
            rigidBody.AddForce(jumpForce);
            shouldJump = false;
        }
    }

    void Movement()
    {
        //get the player's movement (-1 for left, 1 for right, 0 for none)
        xMove = Input.GetAxis("Horizontal");

        //if player is touching wall do not apply force
        if(collidingWall && !onGround)
        {
            xMove = 0;
        }

        if(xMove != 0)
        {
            //setting the player horizontal movement
            float xSpeed = Mathf.Abs(xMove * rigidBody.velocity.x);

            if (xSpeed < maxSpeed)
            {
                Vector3 movementForce = new Vector3(1, 0, 0);
                movementForce *= xMove * speed;

                //RayCast from player to prevent stuck on wall behaviour
                //SweepTest to check direction traveling and check for colliders within a certain distance
                //will return true and fill raycast 
                RaycastHit hit;
                if (!rigidBody.SweepTest(movementForce, out hit, 0.05f))
                {
                    rigidBody.AddForce(movementForce);
                }
            }
            //check speed limit
            if(Mathf.Abs(rigidBody.velocity.x) > maxSpeed)
            {
                Vector2 newVelocity;
                newVelocity.x = Mathf.Sign(rigidBody.velocity.x) * maxSpeed;
                newVelocity.y = rigidBody.velocity.y;

                rigidBody.velocity = newVelocity;
            }
        }
        else
        {
            //if not moving get slightly slower
            Vector2 newVelocity = rigidBody.velocity;
            //reduce current speed by 10%
            newVelocity.x *= 0.9f;
            rigidBody.velocity = newVelocity;
        }
    }
}


