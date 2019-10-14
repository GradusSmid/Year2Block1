using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterScript : MonoBehaviour {

    public ParticleSystem BoosterParticles;
    // Update is called once per frame


    void OnTriggerStay2D(Collider2D other)//other here is where we check the thing that colides.
    {
        if (other.gameObject.tag == "Player")//if the other is tagged with player, do the following.
        {
            Debug.Log("working");

            //boost player or object up
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 20);
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