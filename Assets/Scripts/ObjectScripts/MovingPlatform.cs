using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    int Check = 0;
    void Update()
    {

        {
            while ((Check >= 0) && (Check != 500))
            {
                transform.Translate(Vector2.right * Time.deltaTime);
                Check++;
                if (Check == 500)
            {
                    while ((Check != 0)&&(Check <= 500))
                    {
                        transform.Translate(Vector2.left * Time.deltaTime);
                        Check++;
                    }
                }
            }

        }
    }
}
