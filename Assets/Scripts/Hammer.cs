using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private bool fireButtonDown;
    public GameObject player;
    public GameObject circle;
    public AudioSource hammer;
    private Animator anim;

    private float playerHor;
    private float playerVer;
    // Update is called once per frame
    private void Start()
    {
        player = transform.parent.gameObject.transform.parent.gameObject;
        hammer = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player.name == "Player 1")
        {
            fireButtonDown = Input.GetButton("Fire1");
            playerHor = Input.GetAxis("HorizontalRStick");
            playerVer = Input.GetAxis("VerticalRStick");

            circle = player.transform.GetChild(2).gameObject;
        }
        if (player.name == "Player 2")
        {
            playerHor = Input.GetAxis("HorizontalRStickP2");
            playerVer = Input.GetAxis("VerticalRStickP2");
            fireButtonDown = Input.GetButton("Fire1P2");
        }
        if (player.name == "Player 3")
        {
            playerHor = Input.GetAxis("HorizontalRStickP3");
            playerVer = Input.GetAxis("VerticalRStickP3");
            fireButtonDown = Input.GetButton("Fire1P3");
        }
        if (player.name == "Player 4")

        {
            fireButtonDown = Input.GetButton("Fire1P4");
            playerHor = Input.GetAxis("HorizontalRStickP4");
            playerVer = Input.GetAxis("VerticalRStickP4");
        }

    }
    private void OnTriggerStay2D(Collider2D col)
    {
        Vector3 forceDirection;
        
        if (col.gameObject.tag == "Player" && fireButtonDown)
        {
            hammer.Play();
            Debug.Log("Smashh");
            anim.SetBool("Swing", true);
            forceDirection = col.transform.position - transform.position;
            col.gameObject.GetComponentInParent<Rigidbody2D>().AddForceAtPosition(forceDirection * 300 , Vector3.up);   
        }
        else
        anim.SetBool("Swing", false);
    }
}
