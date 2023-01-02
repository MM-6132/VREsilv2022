using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) {
           if (collision.gameObject.tag == "Player"){
                collision.gameObject.GetComponent<PlayerStats>().TakeDamage(20);
           }
    }
}
