using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Lightsaber : MonoBehaviour
{

    private GameObject laser;
    private Vector3 fullSize;
    private bool activate = false;
    private AudioSource source;
    public AudioClip saberOn;
    public AudioClip saberOff;
    public AudioClip saberMovingSound;
    public AudioClip saberHum;
    public AudioClip saberHit;
    private ControllerVelocity controllerVelocity;
    public GameObject robotCollisionExplosion;
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.spatialBlend = 1;
        laser = transform.Find("SingleLine-LightSaber").gameObject;
        fullSize = laser.transform.localScale; 
        laser.transform.localScale = new Vector3(0, 0, 0);
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(saber);
    }

    // Update is called once per frame
    void Update()
    {
        if(source.isPlaying == false && activate){
            source.PlayOneShot(saberHum);
        }
        if(activate && laser.transform.localScale.y < fullSize.y)
        {
            laser.SetActive(true);
            laser.transform.localScale += new Vector3(0, 0.0005f, 0);
        } else if (activate == false && laser.transform.localScale.y > 0) {
             laser.transform.localScale += new Vector3(0, -0.0005f, 0);
             if(laser.transform.localScale.y < 0.002f)
                laser.transform.localScale = new Vector3(0, 0, 0);
        }else if(activate == false)
            laser.SetActive(false);
    }
    void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Robot" && activate){
            source.Stop();
            source.PlayOneShot(saberHit);
                            GameObject explosion = (GameObject)Instantiate(robotCollisionExplosion,transform.position, transform.rotation);
            collision.gameObject.GetComponent<EnemyAI>().TakeDamage(50);
        }
    }
    public void saber(ActivateEventArgs arg){
        controllerVelocity = arg.interactor.GetComponent<ControllerVelocity>();
        Vector3 velocity = controllerVelocity ? controllerVelocity.Velocity : Vector3.zero;
        activate = !activate;
        if(activate){
            source.PlayOneShot(saberOn);
            laser.transform.localScale = new Vector3(fullSize.x, 0, fullSize.z);
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        else{
            source.Stop();
            source.PlayOneShot(saberOff);
        }
    }
}
