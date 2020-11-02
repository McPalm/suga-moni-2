using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
    public void ResetStage()
    {
        foreach(var director in GetComponentsInChildren<PlayableDirector>())
        {
            var skip = director.GetComponent<SkipAnimation>();
            if (skip && skip.flag.IsActive)
            {
                skip.SkipAhead();
            }
            else
            {

                director.Pause();
                director.time = director.initialTime;
                director.Evaluate();
                if (director.playOnAwake)
                    director.Play();
            }
        }

        foreach(var reset in GetComponentsInChildren<IOnStageReset>(true))
        {
            reset.OnStageReset();
        }
    }
}
