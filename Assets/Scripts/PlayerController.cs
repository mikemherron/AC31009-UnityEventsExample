using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float moveSpeed = 0f;
    public Rigidbody2D rigidBody;
    public Vector2 movement;
    public Animator animator;
    public int health = 10;
    public UIController uiController;
    public GameObject turrets;
    public AudioSource hitSound;
    public AudioSource gameOverSound;
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
                health -= 1;
                if(health <= 0) {
                    //Remove the turrets
                    Destroy(turrets);
                    //Hide the health display
                    uiController.HideHealth();
                    //Show the game over screen
                    uiController.ShowGameOver();
                    //Play the game over sound
                    gameOverSound.Play();
                    //Remove the player
                    Destroy(gameObject);
                } else {
                    //Update health UI
                    uiController.UpdateHealth(health);
                    //Play the hit sound
                    hitSound.Play();
                }
            }
        }
    }
}
