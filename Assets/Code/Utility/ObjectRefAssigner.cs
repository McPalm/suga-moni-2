using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRefAssigner : MonoBehaviour
{
    public ObjectRef ObjectRef;
    
    void OnEnable()
    {
        ObjectRef.GameObject = gameObject;
    }
}
