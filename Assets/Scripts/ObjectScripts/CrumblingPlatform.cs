using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    bool didHit;

    private void Update()
    {
        if (GetComponent<SpriteRenderer>().material.color.a <= 0.1)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            StartCoroutine("fadeIn");
        }
        if(GetComponent<SpriteRenderer>().material.color.a >= 0.9)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
            didHit = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && didHit == false)
        {
            didHit = true;
            StartCoroutine("fadeOut");
        }
    }

    IEnumerator fadeOut()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.1f/ 10)
        {
            Color c = this.GetComponent<SpriteRenderer>().material.color;
            c.a = f;
            this.GetComponent<SpriteRenderer>().material.color = c;
            yield return null;
        }
    }
    IEnumerator fadeIn()
    {
        for (float f = 0.05f; f <= 1; f += 0.1f/ 10)
        {
            Color c = this.GetComponent<SpriteRenderer>().material.color;
            c.a = f;
            this.GetComponent<SpriteRenderer>().material.color = c;
            yield return null;
        }
    }
} 