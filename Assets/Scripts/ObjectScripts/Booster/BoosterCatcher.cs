using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterCatcher : MonoBehaviour {
    void OnTriggerStay2D(Collider2D other)//other here is where we check the thing that colides.
    {
        if ((other.gameObject.tag == "Player") || (other.gameObject.tag == "Item"))//if the other is tagged with player, do the following.
        {
            Debug.Log("working");

            //boost player or object up

            other.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(transform.up * 0);


        }
    }
}
