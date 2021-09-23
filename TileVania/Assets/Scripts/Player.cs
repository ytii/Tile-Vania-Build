using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // config
    [SerializeField]
    float runSpeed = 6.5f; // player's run speed
    [SerializeField]
    float jumpSpeed = 28f;
    [SerializeField]
    float climbSpeed = 5f;
    [SerializeField]
    Vector2 deathKick = new Vector2(25f, 25f);

    // state
    bool isAlive = true;

    // cached components
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    float gravityScaleAtStart;

    // message then methods
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        
        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
        PlayerDeath();
    }

        private void Run()
        {
            float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // -1 to 1
            Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVelocity;

            // have run then animate to run if not then idle
            bool playerhasHorizontalVelocity = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
            myAnimator.SetBool("Running", playerhasHorizontalVelocity);
        }

        private void ClimbLadder()
        {
            if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
            { 
                myAnimator.SetBool("Climbing", false);
                myRigidBody.gravityScale = gravityScaleAtStart;
                return;
            }
         
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
            myRigidBody.velocity = climbVelocity;
            myRigidBody.gravityScale = 0f;

            bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
        }

        private void Jump()
        {
            if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                Vector2 jumpVelocityAdded = new Vector2(0f, jumpSpeed);
                myRigidBody.velocity += jumpVelocityAdded;
            }
        }

        private void PlayerDeath()
        {
            if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
            {
                isAlive = false;
                myAnimator.SetTrigger("Dead");
                GetComponent<Rigidbody2D>().velocity = deathKick;
                FindObjectOfType<GameSession>().ProcessPlayerDeath();
            }
        }

        private void FlipSprite()
        {
            // if the play moves horrizontally
            bool playerhasHorizontalVelocity = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
            if (playerhasHorizontalVelocity)
            {
                // reverse the current scaling of x axis
                transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f);
            }
        }
}
