using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jumpspeed;
    private bool isGrounded;
    private bool waterJetpackUsing;
    public GameObject arm;

    public float JetpackFuel = 100;
    public Text jetpackfueltext;
    private Rigidbody2D rb;
    private LineRenderer lr;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //Move Left and Right

        if (Input.GetAxis("Horizontal") >= 0.90f || Input.GetAxis("Horizontal") <= -0.90f)
        {
            speed = 15;
        }
        else if (Input.GetAxis("Horizontal") >= 0.60f || Input.GetAxis("Horizontal") <= -0.60f)
        {
            speed = 10;
        }
        else
            speed = 5;


        float translation = Input.GetAxis("Horizontal") * speed;

        translation *= Time.deltaTime;

        transform.Translate(translation, 0, 0);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down), Color.blue);
        //Jump
        if (isGrounded == true)
        {
            float jump = Input.GetAxis("Jump") * jumpspeed * 10;

            jump *= Time.deltaTime;

            rb.AddForce(Vector3.up * jump, ForceMode2D.Impulse);

        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.2f))
        {
            isGrounded = true;
            
        }
        else
            isGrounded = false;

        //Arm movement

        arm.transform.localPosition = new Vector3(Input.GetAxis("HorizontalRStick"), Input.GetAxis("VerticalRStick"), 0).normalized;
        arm.transform.rotation = Quaternion.identity;

        // Water Jetpack
        //Grabbing Jetpack

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 forceDirection;
        RaycastHit hit = new RaycastHit();
                
        lr.enabled = false;
        lr.SetPosition(0, arm.transform.position);
        if (waterJetpackUsing == true)
        {
            //Using Jetpack
            if (Input.GetAxis("HorizontalRStick") >= 0.01 && JetpackFuel >= 0 || Input.GetAxis("VerticalRStick") >= 0.01 && JetpackFuel >= 0 || Input.GetAxis("HorizontalRStick") <= -0.01 && JetpackFuel >= 0 || Input.GetAxis("VerticalRStick") <= -0.01 && JetpackFuel >= 0)
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

                    if (hit.collider.gameObject.name == "Player 2")
                    {
                        forceDirection = hit.transform.position - transform.position;
                        hit.rigidbody.AddForceAtPosition(forceDirection.normalized * 15, transform.position);
                    }
                }
                else
                    lr.SetPosition(1, arm.transform.localPosition * 5000);
            }
        }
        jetpackfueltext.text = ("amount of fuel:" + JetpackFuel);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Jetpack")
        {
            Debug.Log("Lets fly");
            waterJetpackUsing = true;
        }
    }
}
