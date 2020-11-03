using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPickup : MonoBehaviour
{
    public StoryFlag storyFlag;
    public AudioClip audioClip;
    public event System.Action OnTrigger;

    // Start is called before the first frame update
    void Start()
    {
        if (storyFlag.IsActive)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Trigger();
    }

    public void Trigger()
    {
        storyFlag.Activate();
        OnTrigger?.Invoke();
        Destroy(gameObject);
        this.Noise(audioClip);
    }
}
