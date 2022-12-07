using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class GameMenuManager : MonoBehaviour
{
    public GameObject menu;
    public InputActionProperty showButton;
    public Transform head;
    public float spawnDistance = 2;
    public AudioSource music;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
           
        }
        if(menu.activeSelf){
         menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
         menu.transform.forward *= -1;
        }
    }
    public void SliderChanger(float value)
    {
        music.volume = value/10;
        Debug.Log(music.volume);
        audioSource.volume = value;
    }
}
