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
        fadeOut();
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Collider2D>().enabled = false;

        Debug.Log("Kaput");
        yield return new WaitForSeconds(1f);
        Debug.Log("Fixed");
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        didHit = false;
        yield return null;
    }

    IEnumerator fadeOut()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.10f)
        {
            Color c = this.GetComponent<SpriteRenderer>().material.color;
            c.a = f;
            this.GetComponent<SpriteRenderer>().material.color = c;
            yield return null;
        }
    }
    IEnumerator fadeIn()
    {
        for (float f = 0.05f; f <= 1; f += 0.10f)
        {
            Color c = this.GetComponent<SpriteRenderer>().material.color;
            c.a = f;
            this.GetComponent<SpriteRenderer>().material.color = c;
            yield return null;
        }
    }
} 