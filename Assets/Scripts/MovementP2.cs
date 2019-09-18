using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementP2 : MonoBehaviour
{
    public float speed;
    public float jumpspeed;
    private bool isGrounded;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);

        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.1f))
        {
            isGrounded = true;
        }
        else
            isGrounded = false;


    }
}
