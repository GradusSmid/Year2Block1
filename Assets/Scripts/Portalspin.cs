﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portalspin : MonoBehaviour
{

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
      transform.Rotate(0, 0, 10 * Time.deltaTime);
    }
}
