using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float fltRunSpeed = 10f;
    [SerializeField] float fltJumpSpeed = 5f;
    [SerializeField] float fltClimbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2 (10f, 10f);
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform gun;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float fltGravityScale;

    bool blIsAlive = true;

    Animator myAnimator;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        fltGravityScale = myRigidbody.gravityScale;
    }

    
    void Update()
    {
        if(!blIsAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnFire(InputValue value)
    {
        if (!blIsAlive) { return; }
        Instantiate(Bullet, gun.position, transform.rotation);
    }

   void OnMove(InputValue value)
   {
       if(!blIsAlive) { return; }
       moveInput = value.Get<Vector2>();
       Debug.Log(moveInput);
   }

   void OnJump(InputValue value)
   {
       if(!blIsAlive) { return; }
       if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
       if(value.isPressed)
       {
           // do stuff
           myRigidbody.velocity += new Vector2 (0f, fltJumpSpeed);
       }
   }

   void Run()
   {
       Vector2 playerVelocity = new Vector2 (moveInput.x * fltRunSpeed, myRigidbody.velocity.y);
       myRigidbody.velocity = playerVelocity;

       bool blPlayerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
       myAnimator.SetBool("blIsRunning", blPlayerHasHorizontalSpeed);
   }

    void FlipSprite()
    {
        bool blPlayerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (blPlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
        
    }

    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            myRigidbody.gravityScale = fltGravityScale;
            myAnimator.SetBool("blIsClimbing", false);
            return;
        }
        Vector2 ClimbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * fltClimbSpeed);
        myRigidbody.velocity = ClimbVelocity;
        myRigidbody.gravityScale = 0f;

        bool blPlayerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("blIsClimbing", blPlayerHasVerticalSpeed);

    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            blIsAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = deathKick;
        }
    }
} // player movement
