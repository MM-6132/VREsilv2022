using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot: MonoBehaviour {
  public Rigidbody bulletPrefab;
  public float shootSpeed = 10;
  public float fireRate = 0.5f; //how many bullets are fired/second
  public Transform player = null;

  private bool playerInRange = true;
  private float lastAttackTime = 0f;

  // Start is called before the first frame update
  void Start() {
  }


  void Update() {
    if (playerInRange) {
      //Rotate the enemy towards the player
      transform.rotation = Quaternion.LookRotation(player.position - transform.position, transform.up);

      if (Time.time - lastAttackTime >= 1f / fireRate) 
      {
        shootBullet();
        lastAttackTime = Time.time;
      }
    }
  }

  void shootBullet() {
    var projectile = Instantiate(bulletPrefab, transform.position, transform.rotation);
    //Shoot the Bullet in the forward direction of the player
    projectile.velocity = transform.forward * shootSpeed;
  }
}