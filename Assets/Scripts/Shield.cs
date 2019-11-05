using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject player;
    private bool fireButtonDown;
    private bool fireButtonUp;
    public bool allowedToMove = true;
    public AudioSource shieldActivate;
    // Start is called before the first frame update


    void Start()
    {
        player = transform.parent.gameObject.transform.parent.gameObject;
        shieldActivate = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player.name == "Player 1")
        {
            fireButtonDown = Input.GetButtonDown("Fire1");
            fireButtonUp = Input.GetButtonUp("Fire1");
            player.GetComponent<Movement>().enabled = allowedToMove;
        }
        if (player.name == "Player 2")
        {
            fireButtonDown = Input.GetButtonDown("Fire1P2");
            fireButtonUp = Input.GetButtonUp("Fire1P2");
            player.GetComponent<MovementP2>().enabled = allowedToMove;
        }
        if (player.name == "Player 3")
        {
            fireButtonDown = Input.GetButtonDown("Fire1P3");
            fireButtonUp = Input.GetButtonUp("Fire1P3");
            player.GetComponent<MovementP3>().enabled = allowedToMove;
        }
        if (player.name == "Player 4")
        {
            fireButtonDown = Input.GetButtonDown("Fire1P4");
            fireButtonUp = Input.GetButtonUp("Fire1P4");
            player.GetComponent<MovementP4>().enabled = allowedToMove;
        }
        if (fireButtonDown)
        {
            Debug.Log("Shield");
            GetComponent<BoxCollider2D>().isTrigger = false;
            allowedToMove = false;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            player.GetComponent<Rigidbody2D>().mass = 1000;
            player.GetComponent<Rigidbody2D>().angularDrag = 0;
            shieldActivate.Play();
        }
        else if (fireButtonUp)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            allowedToMove = true;;
            player.GetComponent<Rigidbody2D>().gravityScale = 7;
            player.GetComponent<Rigidbody2D>().mass = 1;
            player.GetComponent<Rigidbody2D>().angularDrag = 0.05f;
        }

    }
}
