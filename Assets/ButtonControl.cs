using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{

    private AudioSource source;
    public AudioClip buttonSound;

    private bool on = false;
    private bool buttonHit = false;
    private GameObject button;

    private float buttonDownDistance = 0.05f;
    private float buttonReturnSpeed = 0.001f;
    private float buttonOriginalY;

    public Light spotLight;

    private float buttonHitAgainTime = 0.5f;
    private float canHitAgain;


    // Start is called before the first frame update
    private void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        //get button and position, button is in child position 0, on very top
        button = transform.GetChild(0).gameObject;
        buttonOriginalY = button.transform.position.y;

        spotLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(buttonHit == true)
        {
            //play the sound once
            source.PlayOneShot(buttonSound);

            
            buttonHit = false;

            //if on is true make false, if on is false make it true
            on = !on;

            //change position of button
            button.transform.position = new Vector3(button.transform.position.x, button.transform.position.y - buttonDownDistance, button.transform.position.z);


            //turn on off spotlight here
            if(on)
            {
                spotLight.enabled = true;
                XRPlayerController.movementMode = true;
            } else
            {
                spotLight.enabled = false;
                XRPlayerController.movementMode = false;

            }
        }

        //return the button to original position if pushed
        if (button.transform.position.y < buttonOriginalY)
        {
            button.transform.position += new Vector3(0, buttonReturnSpeed, 0);
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        //Debug.Log("An Object has entered the trigger");

        if (other.CompareTag("GameController") && canHitAgain < Time.time)
        {
            canHitAgain = Time.time + buttonHitAgainTime;
            buttonHit = true;
            print("CONTROLLER: Detected collision between button and controller");
        }

    }

    //private void OnTriggerStay (Collider other)
    //{
    //    Debug.Log("An Object is within the trigger");
    //}

    //private void OnTriggerExit (Collider other)
    //{
    //    Debug.Log("an Object has left the trigger.");
    //}
}
