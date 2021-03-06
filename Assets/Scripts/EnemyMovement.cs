using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float fltMoveSpeed = 1;
    Rigidbody2D myRigidbody;


    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        myRigidbody.velocity = new Vector2(fltMoveSpeed, 0f);   
    }

    void OnTriggerExit2D(Collider2D other)
    {
        fltMoveSpeed = -fltMoveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {

        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }
} // Enemy Movement
