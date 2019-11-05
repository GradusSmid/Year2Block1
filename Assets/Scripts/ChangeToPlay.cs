using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToPlay : MonoBehaviour
{
    void Start(){
    }

private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
          Debug.Log("next scene");
            SceneManager.LoadScene(1);
        }
    }

  
}