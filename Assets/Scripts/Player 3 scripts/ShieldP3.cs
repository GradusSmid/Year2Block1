using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldP3 : MonoBehaviour
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
        if (player.name == "Player 3")
        {
            fireButtonDown = Input.GetButton("Fire1P3");
            fireButtonUp = Input.GetButtonUp("Fire1P3");
        }

        if (fireButtonDown)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
            player.GetComponent<MovementP3>().enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            player.GetComponent<Rigidbody2D>().gravityScale = 0;

        }
        else if (fireButtonUp)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            player.GetComponent<MovementP3>().enabled = true;
            player.GetComponent<Rigidbody2D>().gravityScale = 6;
        }

    }
}
