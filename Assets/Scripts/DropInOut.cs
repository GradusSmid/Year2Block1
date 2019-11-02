using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInOut : MonoBehaviour
{
    
    List<GameObject> Players;
    private void Start()
    {
        Players = gameObject.GetComponent<CameraBehaviour>().Players;
    }
}
