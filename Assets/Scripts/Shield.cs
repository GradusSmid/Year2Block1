using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject player;
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
            fireButtonDown = Input.GetButton("Fire1");
            fireButtonUp = Input.GetButtonUp("Fire1");
        }
        if (player.name == "Player 2")
        {
            fireButtonDown = Input.GetButton("Fire1P2");
            fireButtonUp = Input.GetButtonUp("Fire1P2");
        }
        if (player.name == "Player 3")
        {
            fireButtonDown = Input.GetButton("Fire1P3");
            fireButtonUp = Input.GetButtonUp("Fire1P3");
        }
        if (player.name == "Player 4")
        {
            fireButtonDown = Input.GetButton("Fire1P4");
            fireButtonUp = Input.GetButtonUp("Fire1P4");
        }
        if (fireButtonDown)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
            player.GetComponent<Movement>().enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            player.GetComponent<Rigidbody2D>().gravityScale = 0;

        }
        else if (fireButtonUp)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            player.GetComponent<Movement>().enabled = true;
            player.GetComponent<Rigidbody2D>().gravityScale = 6;
        }

    }
}
