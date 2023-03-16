using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
  public GameObject bulletPrefab;
  public GameObject fireTarget;
  public AudioSource hitSound;
  public float fireDelay = 0f;

  private float timeToNextFire = 0f;

  void Start()
  {
    timeToNextFire = fireDelay * Random.value;
  }

  void Update()
  {
    timeToNextFire -= Time.deltaTime;
    if (timeToNextFire <= 0f)
    {
      timeToNextFire = fireDelay;
      GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
      Vector2 direction = fireTarget.transform.position - transform.position;
      direction.Normalize();

      BulletController controller = bullet.GetComponent<BulletController>();
      controller.direction = direction;
      controller.speed *= (float)0.5;
      controller.firedBy = gameObject;

      bullet.GetComponent<SpriteRenderer>().color = Color.red;
    }
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Bullet")
    {
      BulletController bullet = collision.gameObject.GetComponent<BulletController>();
      if (bullet.firedBy.tag == "Player")
      {
        Destroy(collision.gameObject);
        Destroy(gameObject);
      }
    }
  }
}
