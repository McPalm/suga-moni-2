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
    public bool unlockDoubleJump;
    public bool startMusic;
    public Material SkinUnlock;

    public void Activate()
    {
        IsActive = true;

        if (PlatformingCharacterProperties != null)
            PlayerRefference.GameObject.GetComponent<PlatformingCharacter>().Properties = PlatformingCharacterProperties;
        if (unlockDoubleJump)
            PlayerRefference.GameObject.GetComponent<DoubleJump>().enabled = true;
        if (startMusic)
            FindObjectOfType<MusicPlayer>().StartMusic();
        if (SkinUnlock != null)
        {
            PlayerRefference.GameObject.GetComponent<SpriteRenderer>().material = SkinUnlock;
            foreach(var particles in  PlayerRefference.GameObject.GetComponentsInChildren<ParticleSystemRenderer>())
            {
                particles.material = SkinUnlock;
            }
        }
    }

}
