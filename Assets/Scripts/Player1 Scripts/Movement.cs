using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    //Player stats
    public float speed;
    public float jumpspeed = 35;
    public bool isGrounded;
    private Rigidbody2D rb;
    public GameObject arm;
    public SpriteRenderer circle;
    private bool isFaded;
    private bool handIsEmpty = true;
    private float horizontalInput;
    private SpriteRenderer sprite;

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
    //audio
    public AudioSource[] sounds;
    public AudioSource weaponPickup;
    public AudioSource jump1;
    public AudioSource shieldActivate;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        sounds = GetComponents<AudioSource>();
        weaponPickup = sounds[0];
        jump1 = sounds[1];
    }

    // Update is called once per frame
    void Update()
    {
        //Move Left and Right
        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            GetComponent<SpriteRenderer>().flipX = horizontalInput < 0f;
            arm.GetComponent<SpriteRenderer>().flipX = horizontalInput < 0f;
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().flipX = horizontalInput < 0f;
            anim.SetBool("isRunning", true);
        }
        else if (horizontalInput == 0)
        {
            anim.SetBool("isRunning", false);
        }

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

        //Jump
        //Jump
        if ((isGrounded == true) && (Input.GetButtonDown("Jump") == true))
        {
            jump1.Play();
            Vector3 jump = new Vector3(0, jumpspeed, 0);
            rb.AddForce(jump, ForceMode2D.Impulse);            
        }
        
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position - new Vector3(0, sprite.bounds.extents.y - 0.5f, 0), Vector2.down, 0.5f);
        if (hit)
        {
            isGrounded = true;
            anim.SetBool("isFalling", false);
        }
        else
        {
            isGrounded = false;
            anim.SetBool("isJumping", true);
        }
        // Boudning
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minScreenBounds.x + 1, maxScreenBounds.x - 1), Mathf.Clamp(transform.position.y, minScreenBounds.y + 1, maxScreenBounds.y - 3), transform.position.z);

        //Arm movement
        float angle = Mathf.Atan2(-Input.GetAxis("HorizontalRStick"), Input.GetAxis("VerticalRStick")) * Mathf.Rad2Deg;
        if (usingHammer == false)
        {
            arm.transform.localPosition = new Vector3(Input.GetAxis("HorizontalRStick"), Input.GetAxis("VerticalRStick"), 0).normalized;

            // Rotation of arm
           
            arm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if (Input.GetAxis("HorizontalRStick") >= 0)
        {
            arm.transform.localPosition = new Vector3(-1, 0, 0).normalized;
            arm.transform.rotation = Quaternion.AngleAxis(90, new Vector3(0, 0, 90));
        }
        else if(Input.GetAxis("HorizontalRStick") <= 0)
        {
            arm.transform.localPosition = new Vector3(1, 0, 0).normalized;
            arm.transform.rotation = Quaternion.AngleAxis(90, new Vector3(0,0,-90));
        }




        //Aiming circle

        //__________________________________________________________________________________________________________________________________________
        //Rotation of PlayerCircle
        if ((Input.GetAxis("HorizontalRStick") != 0) || (Input.GetAxis("VerticalRStick") != 0))
        {
            circle.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        //fading in or out the Aiming circle
        if ((Input.GetAxis("HorizontalRStick") != 0)  && isFaded == true|| (Input.GetAxis("VerticalRStick") != 0) && isFaded == true)
        {
            //fade in the aiming circle
            StartCoroutine("fadeIn");
            isFaded = false;
        }
        if ((Input.GetAxis("HorizontalRStick") == 0) && (Input.GetAxis("VerticalRStick") == 0) && isFaded == false)
        {
            //fade out the aiming circle
            StartCoroutine("fadeOut");
            isFaded = true;
        }
//      ___________________________________________________________________________________________________________________________

    }
        //Getting the Jetpack
    void OnTriggerEnter2D(Collider2D col)
    { 
        if (col.gameObject.tag == "Jetpack" && usingJetpack == false && handIsEmpty == true)
        {
            weaponPickup.Play();
            Debug.Log("Time to fly");
            GameObject ChildJetpack = Instantiate(Jetpack, arm.transform.position, Quaternion.identity);
            ChildJetpack.transform.parent = arm.transform;
            usingJetpack = true;
            handIsEmpty = false;
        }

        if(col.gameObject.tag == ("Hammer") && usingHammer == false && handIsEmpty == true)
        {
            weaponPickup.Play();
            Debug.Log("hULK SMASH");
            GameObject ChildHammer = Instantiate(hammer, arm.transform.position, Quaternion.identity);
            ChildHammer.transform.parent = arm.transform;
            usingHammer = true;
            handIsEmpty = false;
        }

        if(col.gameObject.tag == ("Hook") && usingHook == false && handIsEmpty == true)
        {
            weaponPickup.Play();
            Debug.Log("Swinging");
            GameObject childHook = Instantiate(Grapplinghook, arm.transform.position, Quaternion.identity);
            childHook.transform.parent = arm.transform;
            usingHook = true;
            handIsEmpty = false;
            GetComponent<DistanceJoint2D>().enabled= true;
        }

        if (col.gameObject.tag == ("Shield") && usingShield == false && handIsEmpty == true)
        {
            weaponPickup.Play();
            Debug.Log("Captain America my dudes");
            GameObject ChildShield = Instantiate(shield, arm.transform.position, Quaternion.identity);
            ChildShield.transform.parent = arm.transform;
            usingShield = true;
            handIsEmpty = false; 
        }
    }
    IEnumerator fadeOut()
    {
        for(float f = 1f; f >= -0.05f; f -= 0.10f)
        {
            Color c = circle.material.color;
            c.a = f;
            circle.material.color = c;
            yield return null;
        }
    }
    IEnumerator fadeIn()
    {
        for (float f = 0.05f; f <= 1; f += 0.10f)
        {
            Color c = circle.material.color;
            c.a = f;
            circle.material.color = c;
            yield return null;
        }
    }
}
