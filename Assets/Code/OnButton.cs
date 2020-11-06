using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnButton : MonoBehaviour
{
    public KeyCode key;

    public UnityEvent OnKeyDown;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(key))
        {
            OnKeyDown.Invoke();
        }
    }
}
