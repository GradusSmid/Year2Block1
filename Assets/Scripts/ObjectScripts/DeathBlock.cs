using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBlock : MonoBehaviour {
    // Update is called once per frame

    void OnTriggerStay2D(Collider2D other)//other here is where we check the thing that colides.
    {
        if (other.gameObject.tag == "Player")//if the other is tagged with player, do the following.
        {
            Debug.Log("deathtrap triggered");
            Destroy(other.gameObject); //kill player
        }
    }
}