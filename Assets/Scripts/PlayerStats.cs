using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public float health = 100;
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("oof");
        if (health <= 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
