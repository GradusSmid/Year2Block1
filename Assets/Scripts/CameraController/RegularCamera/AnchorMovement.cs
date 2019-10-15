using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorMovement : MonoBehaviour
{
    //identify every player
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;


    // Update is called once per frame
    void FixedUpdate()
    {
        //Every player's X value is added up then defided by full number of players. This is now the Camera Anchor's Y value.
        
        //Every player's Y value is added up then defided by full number of players. This is now the Camera Anchor's Y value.
    }
}

//Important. The next step is to attach the springjoint with the camera to the anchor. this way it will rubberband.