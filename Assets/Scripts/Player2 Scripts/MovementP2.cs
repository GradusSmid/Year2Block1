using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementP2 : MonoBehaviour
{
    //Player stats
    public float speed;
    public float jumpspeed = 30;
    public bool isGrounded;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    public GameObject arm;
    private bool handIsEmpty = true;
    private float horizontalInput;
    //Jetpack values
    public GameObject Jetpack;
    private bool usingJetpack;
    //Hammer values
    public GameObject hammer;
    private bool usingHammer;
    //grapplinghook values
    public bool isSwinging;
    public GameObject Grapplinghook;
    private bool usingHook;
    //Shield values
    public GameObject shield;
    private bool usingShield;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("HorizontalP2");
        if (horizontalInput < 0f || horizontalInput > 0f)
        {
            GetComponent<SpriteRenderer>().flipX = horizontalInput > 0f;
            arm.GetComponent<SpriteRenderer>().flipX = horizontalInput > 0f;
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().flipX = horizontalInput > 0f;
            transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().flipX = horizontalInput > 0f;
            transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().flipX = horizontalInput > 0f;
        }
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
        if ((isGrounded == true) && (Input.GetButtonDown("JumpP2") == true))
        {
            Vector3 jump = new Vector3(0, jumpspeed, 0);
            rb.AddForce(jump, ForceMode2D.Impulse);
        }

        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position - new Vector3(0, sprite.bounds.extents.y + 0.5f, 0), Vector2.down, 0.1f);
        if (hit)
        {
            isGrounded = true;

        }
        else
            isGrounded = false;

        //Arm movement

        arm.transform.localPosition = new Vector3(Input.GetAxis("HorizontalRStickP2"), Input.GetAxis("VerticalRStickP2"), 0).normalized;
        arm.transform.rotation = Quaternion.identity;
        // Rotation of arm
        float angle = Mathf.Atan2(Input.GetAxis("HorizontalRStickP2"), -Input.GetAxis("VerticalRStickP2")) * Mathf.Rad2Deg;
        arm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
    //Getting the Jetpack
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Jetpack" && usingJetpack == false && handIsEmpty == true)
        {
            Debug.Log("Time to fly");
            GameObject ChildJetpack = Instantiate(Jetpack, arm.transform.position, Quaternion.identity);
            ChildJetpack.transform.parent = arm.transform;
            usingJetpack = true;
            handIsEmpty = false;
        }

        if (col.gameObject.tag == ("Hammer") && usingHammer == false && handIsEmpty == true)
        {
            Debug.Log("hULK SMASH");
            GameObject ChildHammer = Instantiate(hammer, arm.transform.position, Quaternion.identity);
            ChildHammer.transform.parent = arm.transform;
            usingHammer = true;
            handIsEmpty = false;
        }
        if (col.gameObject.tag == ("Hook") && usingHook == false && handIsEmpty == true)
        {
            Debug.Log("Swinging");
            GameObject childHook = Instantiate(Grapplinghook, arm.transform.position, Quaternion.identity);
            childHook.transform.parent = arm.transform;
            usingHook = true;
            handIsEmpty = false;
            GetComponent<DistanceJoint2D>().enabled = true;
        }

        if (col.gameObject.tag == ("Shield") && usingShield == false && handIsEmpty == true)
        {
            Debug.Log("Captain America my dudes");
            GameObject ChildShield = Instantiate(shield, arm.transform.position, Quaternion.identity);
            ChildShield.transform.parent = arm.transform;
            usingShield = true;
            handIsEmpty = false;
        }
    }
}

