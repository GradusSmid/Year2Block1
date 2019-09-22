using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public GameObject arm;
    public GameObject player;

    private float playerHor;
    private float playerVer;
    // Start is called before the first frame update
    void Start()
    {
        arm = transform.parent.gameObject;
        player = transform.parent.gameObject.transform.parent.gameObject;
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
            col.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(forceDirection.normalized * 1000, transform.position);
        }
    }
}
