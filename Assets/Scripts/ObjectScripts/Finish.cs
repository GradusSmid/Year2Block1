using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public AudioSource victory;
    private int nextLevel;

    void Start(){
        victory = GetComponent<AudioSource>();
        nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void Update()
    {
        if(nextLevel == SceneManager.sceneCountInBuildSettings)
        {
            nextLevel = SceneManager.GetActiveScene().buildIndex - SceneManager.GetActiveScene().buildIndex;
        }
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
        SceneManager.LoadScene(nextLevel);
        yield return null;
    }
}
