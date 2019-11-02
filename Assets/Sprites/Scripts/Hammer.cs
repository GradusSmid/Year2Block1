using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private bool fireButtonDown;
    public GameObject player;

    public AudioSource hammer;
    // Update is called once per frame
    private void Start()
    {
        player = transform.parent.gameObject.transform.parent.gameObject;
        hammer = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (player.name == "Player 1")
        {
            fireButtonDown = Input.GetButtonDown("Fire1");
        }
        if (player.name == "Player 2")
        {
            fireButtonDown = Input.GetButtonDown("Fire1P2");
        }
        if (player.name == "Player 3")
        {
            fireButtonDown = Input.GetButtonDown("Fire1P3");
        }
        if (player.name == "Player 4")
        {
            fireButtonDown = Input.GetButtonDown("Fire1P4");
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        Vector3 forceDirection;
        
        if (col.gameObject.tag == "Player" && fireButtonDown)
        {
            hammer.Play();
            Debug.Log("Smashh");
            forceDirection = col.transform.position - transform.position;
            col.gameObject.GetComponentInParent<Rigidbody2D>().AddForceAtPosition(forceDirection.normalized * 500, transform.position);
            }
           
    }
}
