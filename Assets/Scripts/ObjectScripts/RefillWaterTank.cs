using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillWaterTank : MonoBehaviour
{
    //private float jetpackfuel;

    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.GetComponentInChildren<WaterJetpack>().JetpackFuel =500;
        }
        
    }
}
