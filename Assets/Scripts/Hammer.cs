using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private bool fireButtonDown;
    public GameObject player;

    public AudioSource hammer;
    private Animator anim;
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
        }
        if (player.name == "Player 2")
        {
            fireButtonDown = Input.GetButton("Fire1P2");
        }
        if (player.name == "Player 3")
        {
            fireButtonDown = Input.GetButton("Fire1P3");
        }
        if (player.name == "Player 4")
        {
            fireButtonDown = Input.GetButton("Fire1P4");
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
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("HammerSwing"))
            {
                this.gameObject.transform.rotation = Quaternion.AngleAxis(90, new Vector3(0, 0, 0));
                col.gameObject.GetComponentInParent<Rigidbody2D>().AddForceAtPosition(forceDirection * 300, transform.position);
            }
            
        }
        else
        anim.SetBool("Swing", false);
    }
}
