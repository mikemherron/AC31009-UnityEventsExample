using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerHealthUpdatedEvent : UnityEvent<int> { }

public class PlayerController : MonoBehaviour
{
    public PlayerHealthUpdatedEvent onPlayerHealthUpdated;

    public GameObject bulletPrefab;
    public int health = 10;
    public float moveSpeed = 0f;
    public Rigidbody2D rigidBody;
    public Vector2 movement;
    public Animator animator;
    public AudioSource hitSound;
    
    void Start()
    {
        if(onPlayerHealthUpdated == null) {
            onPlayerHealthUpdated = new PlayerHealthUpdatedEvent();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction.Normalize();
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            BulletController controller = bullet.GetComponent<BulletController>();
            controller.direction = direction;
            controller.firedBy = gameObject;
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate() 
    {
        rigidBody.MovePosition(rigidBody.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet") {
            BulletController controller = collision.gameObject.GetComponent<BulletController>();
            if (controller.firedBy.tag == "Turret") {
                Destroy(collision.gameObject);
                health--;
                onPlayerHealthUpdated.Invoke(health);
                if(health > 0) {
                    hitSound.Play();
                }
            }
        }
    }
}