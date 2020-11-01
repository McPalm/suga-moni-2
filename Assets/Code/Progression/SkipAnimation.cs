using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipAnimation : MonoBehaviour
{
    public StoryFlag flag;
       
    // Start is called before the first frame update
    IEnumerable Start()
    {
        yield return null;
        if (flag.IsActive)
            SkipAhead();
    }

    public void SkipAhead()
    {
        Debug.Log("Skipping Ahead!");
        var director = GetComponent<PlayableDirector>();
        director.time = director.duration;
        director.Evaluate();
    }
}
