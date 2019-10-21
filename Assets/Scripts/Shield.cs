using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject player;
    private float playerHor;
    private float playerVer;
    private bool fireButtonDown;
    private bool fireButtonUp;
    public bool allowedToMove;
    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.name == "Player 1")
        {

            playerHor = Input.GetAxis("HorizontalRStick");
            playerVer = Input.GetAxis("VerticalRStick");
            fireButtonDown = Input.GetButton("Fire1");
            fireButtonUp = Input.GetButtonUp("Fire1");
            allowedToMove = player.GetComponent<Movement>();
        }
        if (player.name == "Player 2")
        {
            playerHor = Input.GetAxis("HorizontalRStickP2");
            playerVer = Input.GetAxis("VerticalRStickP2");
            fireButtonDown = Input.GetButtonDown("Fire1P2");
            fireButtonUp = Input.GetButtonUp("Fire1P2");
            allowedToMove = player.GetComponent<MovementP2>();
        }
        if (player.name == "Player 3")
        {
            playerHor = Input.GetAxis("HorizontalRStickP3");
            playerVer = Input.GetAxis("VerticalRStickP3");
            fireButtonDown = Input.GetButtonDown("Fire1P3");
            fireButtonUp = Input.GetButtonUp("Fire1P3");
            allowedToMove = player.GetComponent<MovementP3>();
        }
        if (player.name == "Player 4")
        {
            playerHor = Input.GetAxis("HorizontalRStickP4");
            playerVer = Input.GetAxis("VerticalRStickP4");
            fireButtonDown = Input.GetButtonDown("Fire1P4");
            fireButtonUp = Input.GetButtonUp("Fire1P4");
            allowedToMove = player.GetComponent<MovementP4>();
        }
        if (fireButtonDown)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;

            if (allowedToMove = player.GetComponent<Movement>())
            {
                player.GetComponent<Movement>().enabled = false;
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                player.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
        }
        else if (fireButtonUp)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            if (allowedToMove = player.GetComponent<Movement>())
            {
                player.GetComponent<Movement>().enabled = true;

                player.GetComponent<Rigidbody2D>().gravityScale = 6;

            }
        }

    }
}
