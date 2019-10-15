using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        Vector3 forceDirection;
        
        if (col.gameObject.tag == "Player" && Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Smashh");
            forceDirection = col.transform.position - transform.position;
            col.gameObject.GetComponentInParent<Rigidbody2D>().AddForceAtPosition(forceDirection.normalized * 500, transform.position);
            }
           
    }
}
