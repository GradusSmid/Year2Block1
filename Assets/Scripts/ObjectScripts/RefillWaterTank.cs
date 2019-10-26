using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillWaterTank : MonoBehaviour
{

    public AudioSource waterRefil;
    //private float jetpackfuel;

    void Start(){
         waterRefil = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {   
            GetComponentInChildren<WaterJetpack>().JetpackFuel=500;
            Debug.Log(GetComponentInChildren<WaterJetpack>().JetpackFuel);
        }
        
    }
        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.gameObject.tag == "Player"){
                waterRefil.Play();
            }
    }

}

