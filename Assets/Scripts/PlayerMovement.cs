using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float fltRunSpeed = 10f;
    [SerializeField] float fltJumpSpeed = 5f;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    CapsuleCollider2D myCapsuleCollider;

    Animator myAnimator;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    
    void Update()
    {
        Run();
        FlipSprite();
    }

   void OnMove(InputValue value)
   {
       moveInput = value.Get<Vector2>();
       Debug.Log(moveInput);
   }

   void OnJump(InputValue value)
   {
       if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
} // player movement
