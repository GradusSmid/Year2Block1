using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToPlay : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
          
           StartCoroutine("FinishLine");
        }
    }

    IEnumerator FinishLine()
    {
        yield return new WaitForSeconds(1.6f);
        SceneManager.LoadScene(1);
        yield return null;
    }
    }
