using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public AudioSource victory;

    void Start(){
        victory = GetComponent<AudioSource>();
    }

private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            victory.Play();
           StartCoroutine("FinishLine");
        }
    }

    IEnumerator FinishLine()
    {
        yield return new WaitForSeconds(1.6f);
        SceneManager.LoadScene(0);
        yield return null;
    }
}
