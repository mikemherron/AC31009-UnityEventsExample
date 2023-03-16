using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public GameObject firedBy;
    public Rigidbody2D rb;
    public Vector2 direction;
   
    void Update() 
    {
        rb.MovePosition(rb.position + direction.normalized * speed * Time.fixedDeltaTime);
    }
}
