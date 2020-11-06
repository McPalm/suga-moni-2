using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    static public CheckPoint lastCP { get; private set; }

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
        if(lastCP)
            lastCP.DeactivateCP();
        lastCP = this;
        GetComponent<Animator>().SetBool("On", true);
        FindObjectOfType<StageManager>().Save();
    }

    public void DeactivateCP()
    {
        GetComponent<Animator>().SetBool("On", false);
    }
}
