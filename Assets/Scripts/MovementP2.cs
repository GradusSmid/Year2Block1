using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementP2 : MonoBehaviour
{
    //Player stats
    public float speed;
    public float jumpspeed;
    private bool isGrounded;
    private Rigidbody2D rb;
    public GameObject arm;
    private bool handIsEmpty = true;
    //Jetpack values
    public GameObject Jetpack;
    private bool usingJetpack;
    //Hammer values
    public GameObject hammer;
    private bool usingHammer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move Left and Right
        if (Input.GetAxis("HorizontalP2") >= 0.90f || Input.GetAxis("HorizontalP2") <= -0.90f)
        {
            speed = 15;
        }
        else if (Input.GetAxis("HorizontalP2") >= 0.60f || Input.GetAxis("HorizontalP2") <= -0.60f)
        {
            speed = 10;
        }
        
        else speed = 5;
        float translation = Input.GetAxis("HorizontalP2") * speed;

        translation *= Time.deltaTime;

        transform.Translate(translation, 0, 0);

        //Jump
        if (isGrounded == true)
        {
            float jump = Input.GetAxis("JumpP2") * jumpspeed;

            jump *= Time.deltaTime;

            rb.AddForce(Vector3.up * jump, ForceMode2D.Impulse);

        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.1f))
        {
            isGrounded = true;
        }
        else
            isGrounded = false;

        //Arm movement

        arm.transform.localPosition = new Vector3(Input.GetAxis("HorizontalRStickP2"), Input.GetAxis("VerticalRStickP2"), 0).normalized;
        arm.transform.rotation = Quaternion.identity;

    }
    //Getting the Jetpack
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Jetpack" && usingJetpack == false)
        {
            Debug.Log("Time to fly");
            GameObject ChildJetpack = Instantiate(Jetpack, arm.transform.position, Quaternion.identity);
            ChildJetpack.transform.parent = arm.transform;
            usingJetpack = true;
        }

        if (col.gameObject.tag == ("Hammer") && usingHammer == false && handIsEmpty == true)
        {
            Debug.Log("hULK SMASH");
            GameObject ChildHammer = Instantiate(hammer, arm.transform.position, Quaternion.identity);
            ChildHammer.transform.parent = arm.transform;
            usingHammer = true;
            handIsEmpty = false;
        }
    }
}

