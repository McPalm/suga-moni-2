using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    [Scene]
    public string Scene;
    public StageData[] nearbyStages;
    public int BuildIndex => UnityEngine.SceneManagement.SceneUtility.GetBuildIndexByScenePath(Scene);
    public bool IsLoaded => SceneManager.GetSceneByBuildIndex(BuildIndex).isLoaded; // SceneManager.GetSceneByBuildIndex(BuildIndex).IsValid() &&    I dont think I need this, ideally isLoaded is defaulted to false on an invalid scene.

    public void Load()
    {
        if (IsLoaded)
            return;

        SceneManager.LoadScene(BuildIndex, LoadSceneMode.Additive);
    }

    public void UnLoad()
    {
        if(IsLoaded)
            SceneManager.UnloadSceneAsync(BuildIndex);
    }

}
