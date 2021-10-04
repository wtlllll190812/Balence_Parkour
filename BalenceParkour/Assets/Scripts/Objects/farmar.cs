using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class farmar : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Plate"))
        {
            CameraManager.instance.shakeTime = 0.05f;
        }
    }
}
