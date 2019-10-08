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
    private void OnTriggerEnter2D(Collider2D col)
    {
        Vector3 forceDirection;
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Smashh");
            forceDirection = col.transform.position - transform.position;
            col.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(forceDirection.normalized * 250, transform.position);
        }
    }
}
