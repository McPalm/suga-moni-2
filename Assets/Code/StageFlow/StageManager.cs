using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public Transform CameraContainer;

    public PlatformingCharacter Player;

    public StageData[] Stages;
    Stage current;
    public StageData StartStage;
    public SaveManager SaveManager;

    public AnimationCurve CameraTransitionCurve;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        FindObjectOfType<KillPlayer>().OnReset += StageManager_OnReset;

        if (PlayerPrefs.GetString("LoadMode") == "LoadGame")
        {
            var startStage = SaveManager.Load(this);
            if (startStage != null)
                StartStage = startStage;
        }

        Debug.Log("Loading...");
        StartStage.Load();
        while (StartStage.IsLoaded == false)
            yield return null;
        Debug.Log("Loaded");
        current = FindObjectOfType<Stage>();
        var cp = current.GetComponentInChildren<CheckPoint>();
        if (cp)
            Player.transform.position = cp.transform.position + Vector3.up;
        CameraContainer.transform.position = current.transform.position;
        yield return new WaitForSeconds(.5f);
        foreach(var stage in StartStage.nearbyStages)
        {
            stage.Load();
        }
    }

    

    internal void Enter(Stage stage)
    {
        if(current != stage)
        {
            StartCoroutine(AnimateTransition(stage));
            foreach(var neighbor in stage.stageData.nearbyStages)
            {
                neighbor.Load();
            }
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
            CameraContainer.position = Vector2.LerpUnclamped(start, destination, CameraTransitionCurve.Evaluate(f));
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

    public void Save()
    {
        SaveManager.Save(this);
    }

}
