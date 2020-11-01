﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public string[] Stages;


    public Transform CameraContainer;

    Stage current;
    public StageData StartStage;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<KillPlayer>().OnReset += StageManager_OnReset;
        foreach(string stage in Stages)
        {
            SceneManager.LoadScene(stage, LoadSceneMode.Additive);
        }
    }

    

    internal void Enter(Stage stage)
    {
        if(current != stage)
        {
            StartCoroutine(AnimateTransition(stage));
        }
    }

    IEnumerator AnimateTransition(Stage stage)
    {
        Time.timeScale = 0f;
        stage.ResetStage();
        Vector2 start = CameraContainer.transform.position;
        Vector2 destination = stage.transform.position;

        for(float f = 0; f < 1f; f += Time.unscaledDeltaTime * 2f)
        {
            CameraContainer.position = Vector2.Lerp(start, destination, f);
            yield return null;
        }

        CameraContainer.position = destination;

        current = stage;
        Time.timeScale = 1f;
    }

    private void StageManager_OnReset()
    {
        current?.ResetStage();
    }

}