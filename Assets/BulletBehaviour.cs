using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
	public GameObject robotCollisionExplosion;
    public GameObject smCollisionExplosion;

    void OnCollisionEnter(Collision collision) {
        //Debug.Log("Salt");
        //collisionExplosion.Play();
            if (collision.gameObject.name.Contains("SM"))
            {
                GameObject explosion = (GameObject)Instantiate(smCollisionExplosion,transform.position, transform.rotation);
                Destroy(gameObject);
            }
           if (collision.gameObject.tag == "Robot"){
                GameObject explosion = (GameObject)Instantiate(robotCollisionExplosion,transform.position, transform.rotation);
                Destroy(explosion, 2f);
                Destroy(gameObject);
           }

    }
}
