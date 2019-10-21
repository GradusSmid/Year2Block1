using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterStopper : MonoBehaviour
{
    Vector2 velocity;
    void OnTriggerStay2D(Collider2D other)//other here is where we check the thing that colides.
    {
        if ((other.gameObject.tag == "Player") || (other.gameObject.tag == "Item"))//if the other is tagged with player, do the following.
        {
            Debug.Log("working");

            //boost player or object up
            velocity = other.GetComponent<Rigidbody2D>().velocity;
            velocity = new Vector2 (0, 0);
            
        }
    }
}
