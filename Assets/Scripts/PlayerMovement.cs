using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float fltRunSpeed = 10f;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;

    Animator myAnimator;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
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
