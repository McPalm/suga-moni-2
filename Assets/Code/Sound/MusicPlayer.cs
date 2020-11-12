using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] MusicClips;
    bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartMusic()
    {
        if(!isPlaying)
            StartCoroutine(MusicLoop());
    }

    public void StopMusic()
    {
        StopAllCoroutines();
        GetComponent<AudioSource>().Stop();
    }

    IEnumerator MusicLoop()
    {
        var source = GetComponent<AudioSource>();

        yield return new WaitForSeconds(3f);
        for(int i = 0; ; i++)
        {
            source.clip = MusicClips[i % MusicClips.Length];
            source.Play();
            while(source.isPlaying)
                yield return null;
           yield return new WaitForSeconds(30f);
        }
    }

}
