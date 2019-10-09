using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAccess : MonoBehaviour {
    private bool touchingBooster = false;

    public GameObject Player;
    // Update is called once per frame
    void Update()
    {

            //check for player's collision with game object tagged Booster
            if (touchingBooster)
            {
                
            }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Booster")
        {
            touchingBooster = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Booster")
        {
            touchingBooster = false;
        }
    }
}