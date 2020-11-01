using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnTouch : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        var play = collision.GetComponent<KillPlayer>();
        if (play)
            play.Kill();
    }
}
