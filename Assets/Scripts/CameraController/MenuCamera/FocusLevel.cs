﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusLevel : MonoBehaviour
{
    // this secures that the camera takes the level into focus
    public float HalfXBounds = 20f;
    public float HalfYBounds = 15f;
    public float HalfZBounds = 15f;

    public Bounds FocusBounds;


    // Update is called once per frame
    void Update()
    {
        Vector3 position = gameObject.transform.position;
        Bounds bounds = new Bounds();
        bounds.Encapsulate(new Vector3(position.x - HalfXBounds, position.y - HalfYBounds, position.z - HalfZBounds));
        bounds.Encapsulate(new Vector3(position.x + HalfXBounds, position.y + HalfYBounds, position.z + HalfZBounds));
        FocusBounds = bounds;
    }
}
