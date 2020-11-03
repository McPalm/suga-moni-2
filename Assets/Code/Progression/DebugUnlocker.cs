using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUnlocker : MonoBehaviour
{
    public StoryFlag[] StartUnlocked;

#if UNITY_EDITOR
    // Start is called before the first frame update
    void Start()
    {
        foreach(var flag in StartUnlocked)
        {
            flag.Activate();
        }
    }
#endif
}
