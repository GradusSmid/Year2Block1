using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    bool didHit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && didHit == false)
        {
            didHit = true;
            StartCoroutine("Destroy");
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Debug.Log("Kaput");
        yield return new WaitForSeconds(1f);
        Debug.Log("Fixed");
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        didHit = false;
        yield return null;
    }
} 