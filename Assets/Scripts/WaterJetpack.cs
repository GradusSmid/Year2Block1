using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterJetpack : MonoBehaviour
{
    public LineRenderer lr;
    public GameObject arm;
    private bool waterJetpackUsing;
    public float JetpackFuel = 500;
    private Rigidbody2D rb;
    public GameObject player;

    private float playerHor;
    private float playerVer;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        rb = GetComponentInParent<Rigidbody2D>();
        arm = transform.parent.gameObject;
        player = transform.parent.gameObject.transform.parent.gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 forceDirection;
      //  RaycastHit hit = new RaycastHit();

        lr.enabled = false;
        lr.SetPosition(0, arm.transform.position);

        if(player.name == "Player 1")
        {
             playerHor = Input.GetAxis("HorizontalRStick");
             playerVer = Input.GetAxis("VerticalRStick");
        }
        if(player.name == "Player 2")
        {
            playerHor = Input.GetAxis("HorizontalRStickP2");
            playerVer = Input.GetAxis("VerticalRStickP2");
        }
        if (player.name == "Player 3")
        {
            playerHor = Input.GetAxis("HorizontalRStickP3");
            playerVer = Input.GetAxis("VerticalRStickP3");
        }
        if (player.name == "Player 4")
        {
            playerHor = Input.GetAxis("HorizontalRStickP4");
            playerVer = Input.GetAxis("VerticalRStickP4");
        }
        Debug.DrawRay(arm.transform.position,arm.transform.localPosition , Color.blue);
        //Using Jetpack
        if (playerHor >= 0.01 && JetpackFuel >= 0 && Input.GetButton("Fire1") || playerVer >= 0.01 && JetpackFuel >= 0 && Input.GetButton("Fire1") || playerHor <= -0.01 && JetpackFuel >= 0 && Input.GetButton("Fire1") || playerVer <= -0.01 && JetpackFuel >= 0 && Input.GetButton("Fire1"))
        {
            rb.AddForce(-arm.transform.localPosition * 28);
            lr.enabled = true;
            JetpackFuel--;
            
            RaycastHit2D hit = Physics2D.Raycast(arm.transform.position, arm.transform.localPosition, 10);
            //hit other players
            if (hit.collider != null)
            {
                    Debug.Log("Did Hit" + hit.collider.gameObject.name);
                    
                    lr.SetPosition(1, hit.point);

                    if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Player")
                    {
                        forceDirection = hit.transform.position - transform.position;
                        hit.rigidbody.AddForceAtPosition(forceDirection.normalized * 25, transform.position);
                    }
            }
                else
                    lr.SetPosition(1, arm.transform.localPosition * 5000);
        }


        //This is double.
        RaycastHit2D col = Physics2D.Raycast(player.transform.position, Vector2.down, 1.2f);
        if (col.collider.tag == "Waterfuel")
        {
            if (JetpackFuel <= 500)
            {
                JetpackFuel=500;
            }
        } 
    }
}
