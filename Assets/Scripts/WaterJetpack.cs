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
    public AudioSource watershoot;
    private float playerHor;
    private float playerVer;
    private bool buttonDown;
    private bool buttonUp;
    private bool button;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        rb = GetComponentInParent<Rigidbody2D>();
        arm = transform.parent.gameObject;
        player = transform.parent.gameObject.transform.parent.gameObject;
        watershoot = GetComponent<AudioSource>();
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
            buttonDown = Input.GetButtonDown("Fire1");
            buttonUp = Input.GetButtonUp("Fire1");
            button = Input.GetButton("Fire1");
        }
        if(player.name == "Player 2")
        {
            playerHor = Input.GetAxis("HorizontalRStickP2");
            playerVer = Input.GetAxis("VerticalRStickP2");
            buttonDown = Input.GetButtonDown("Fire1P2");
            buttonUp = Input.GetButtonUp("Fire1P2");
            button = Input.GetButton("Fire1P2");
        }
        if (player.name == "Player 3")
        {
            playerHor = Input.GetAxis("HorizontalRStickP3");
            playerVer = Input.GetAxis("VerticalRStickP3");
            buttonDown = Input.GetButtonDown("Fire1P3");
            buttonUp = Input.GetButtonUp("Fire1P3");
            button = Input.GetButton("Fire1P3");
        }
        if (player.name == "Player 4")
        {
            playerHor = Input.GetAxis("HorizontalRStickP4");
            playerVer = Input.GetAxis("VerticalRStickP4");
            buttonDown = Input.GetButtonDown("Fire1P4");
            buttonUp = Input.GetButtonUp("Fire1P4");
            button = Input.GetButton("Fire1P4");
        }
        Debug.DrawRay(arm.transform.position,arm.transform.localPosition , Color.blue);
        //Using Jetpack
        if (playerHor != 0 && JetpackFuel >= 0 && button || playerVer != 0 && JetpackFuel >= 0 && button)
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
                        hit.rigidbody.AddForceAtPosition(forceDirection.normalized * 45, transform.position);
                    }
            }
                else
                    lr.SetPosition(1, arm.transform.localPosition * 5000);
        }

            //playing audio (im proud of this one)
         if (playerHor != 0 && JetpackFuel >= 0 && buttonDown == true|| playerVer != 0 && JetpackFuel >= 0 && buttonDown == true)
        {
            watershoot.Play();
            watershoot.loop = true;
        }
        if (playerHor !=0 && JetpackFuel >= 0 && buttonUp == true|| playerVer !=0 && JetpackFuel >= 0 && buttonUp == true)
        {
            watershoot.Stop();
        }
        if (JetpackFuel == 0){
            watershoot.Stop();
        }



    }
}
