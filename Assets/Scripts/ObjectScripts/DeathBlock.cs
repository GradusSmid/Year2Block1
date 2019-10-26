using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBlock : MonoBehaviour {
    
public AudioSource death;

    void Start(){
        death = GetComponent<AudioSource>();
}
    void OnTriggerStay2D(Collider2D other)//other here is where we check the thing that colides.
    {
        if (other.gameObject.tag == "Player")//if the other is tagged with player, do the following.
        {
            death.Play();
            Debug.Log("deathtrap triggered");
            Destroy(other.gameObject); //kill player
        }
    }
}