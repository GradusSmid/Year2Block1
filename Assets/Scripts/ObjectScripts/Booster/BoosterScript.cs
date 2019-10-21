using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterScript : MonoBehaviour {

    public ParticleSystem BoosterParticles;
    // Update is called once per frame

    void OnTriggerStay2D(Collider2D other)//other here is where we check the thing that colides.
    {
        if ((other.gameObject.tag == "Player")|| (other.gameObject.tag == "Item"))//if the other is tagged with player, do the following.
        {
            Debug.Log("working");

            //boost player or object up
<<<<<<< HEAD:Assets/Scripts/ObjectScripts/BoosterScript.cs
            other.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(transform.up* 150);

=======
            other.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(transform.up* 80);
>>>>>>> e2c86ab5134706665d81d3b2222f44780c60c16f:Assets/Scripts/ObjectScripts/Booster/BoosterScript.cs
            //play animation/particle effect
            BoosterParticles.loop = true;
            BoosterParticles.Play();

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            //turn off patricles
            BoosterParticles.loop = false;
        }
    }
}