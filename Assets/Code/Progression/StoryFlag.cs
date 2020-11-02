using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StoryFlag : ScriptableObject
{

    [System.NonSerialized]
    public bool IsActive;

    public PlatformingCharacterProperties PlatformingCharacterProperties;
    public ObjectRef PlayerRefference;

    public void Activate()
    {
        IsActive = true;

        if (PlatformingCharacterProperties != null)
            PlayerRefference.GameObject.GetComponent<PlatformingCharacter>().Properties = PlatformingCharacterProperties;
    }

}
