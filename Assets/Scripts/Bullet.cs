using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float fltBulletSpeed = 20f;
    Rigidbody2D myRigidbody;
    PlayerMovement player;
    float fltXspeed;

    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        fltXspeed = player.transform.localScale.x * fltBulletSpeed;
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(fltXspeed, 0f);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
