using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnStuckScript : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)//other here is where we check the thing that colides.
    {
        if (other.gameObject.tag == "Player")//if the other is tagged with player, do the following.
        {
            Debug.Log("Teleported player up to secure he's not stuck");
            Vector3 tempPos = other.transform.position;
            tempPos.y += 2f;
            other.transform.position = tempPos;
        }
    }
}
