using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerP4 : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D col)
    {
        Vector3 forceDirection;

        if (col.gameObject.tag == "Player" && Input.GetButtonDown("Fire1P4"))
        {
            Debug.Log("Smashh");
            forceDirection = col.transform.position - transform.position;
            col.gameObject.GetComponentInParent<Rigidbody2D>().AddForceAtPosition(forceDirection.normalized * 500, transform.position);
        }

    }
}
