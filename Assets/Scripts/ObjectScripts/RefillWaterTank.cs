﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillWaterTank : MonoBehaviour
{

    public AudioSource waterRefil;
    //private float jetpackfuel;

    void Start(){
         waterRefil = GetComponent<AudioSource>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.transform.GetChild(0).GetChild(0).name == ("WaterJetpack(Clone)"))
        { 
            Debug.Log("waterrefill");
            col.GetComponentInChildren<WaterJetpack>().JetpackFuel = 500;
        }
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            waterRefil.Play();
        }
    }
}

