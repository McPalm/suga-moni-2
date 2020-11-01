using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPickup : MonoBehaviour
{
    public StoryFlag storyFlag;

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
        Destroy(gameObject);
    }
}
