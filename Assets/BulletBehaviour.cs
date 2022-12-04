using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
	public GameObject collisionExplosion;

    void OnCollisionEnter(Collision collision) {
        //Debug.Log("Salt");
        //collisionExplosion.Play();
           if (collision.gameObject.tag == "Robot"){
                GameObject explosion = (GameObject)Instantiate(collisionExplosion,transform.position, transform.rotation);
                Destroy(explosion, 2f);
                Destroy(gameObject);
           }
    }
}
