using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterJetpack : MonoBehaviour
{
    private LineRenderer lr;
    public GameObject arm;
    private bool waterJetpackUsing;
    public float JetpackFuel = 100;
    private Rigidbody2D rb;
    public GameObject player;

    private float playerHor;
    private float playerVer;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponentInParent<LineRenderer>();
        rb = GetComponentInParent<Rigidbody2D>();
        arm = transform.parent.gameObject;
        player = transform.parent.gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 forceDirection;
        RaycastHit hit = new RaycastHit();

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

            //Using Jetpack
            if (playerHor >= 0.01 && JetpackFuel >= 0 || playerVer >= 0.01 && JetpackFuel >= 0 || playerHor <= -0.01 && JetpackFuel >= 0 || playerVer <= -0.01 && JetpackFuel >= 0)
            {
                rb.AddForce(-arm.transform.localPosition * 15);
                lr.enabled = true;
                JetpackFuel--;

                //hit other players
                if (Physics.Raycast(arm.transform.position, transform.TransformDirection(arm.transform.localPosition), out hit, Mathf.Infinity))
                {
                    Debug.Log("Did Hit" + hit.collider.gameObject.name);
                    Debug.DrawRay(arm.transform.position, transform.TransformDirection(arm.transform.localPosition), Color.blue);
                    lr.SetPosition(1, hit.point);

                    if (hit.collider.gameObject.name == "Player 2" || hit.collider.gameObject.name == "Player 1")
                    {
                        forceDirection = hit.transform.position - transform.position;
                        hit.rigidbody.AddForceAtPosition(forceDirection.normalized * 15, transform.position);
                    }
                }
                else
                    lr.SetPosition(1, arm.transform.localPosition * 5000);
            }
    }
}
