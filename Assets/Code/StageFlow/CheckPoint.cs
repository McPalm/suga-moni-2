using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    static CheckPoint lastCP;

    public AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActivateCP();
        var kill = collision.GetComponent<KillPlayer>();
        if(kill)
        {
            kill.SpawnPoint = transform.position + Vector3.up * .5f;
        }
    }

    public void ActivateCP()
    {
        if (lastCP == this)
            return;
        AudioPool.PlaySound(transform.position, audioClip);
        lastCP?.DeactivateCP();
        lastCP = this;
        GetComponent<Animator>().SetBool("On", true);
    }

    public void DeactivateCP()
    {
        GetComponent<Animator>().SetBool("On", false);
    }
}
