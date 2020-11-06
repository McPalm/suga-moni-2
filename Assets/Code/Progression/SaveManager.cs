using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public StoryFlag[] flags;

    public void Save(StageManager manager)
    {
        var cp = CheckPoint.lastCP;
        if(cp)
        {
            var stage = cp.GetComponentInParent<Stage>();
            var name = stage.stageData.name;
            PlayerPrefs.SetString("stage", name);
        }
        else
        {
            PlayerPrefs.SetString("stage", "");
        }
        foreach(var flag in flags)
        {
            PlayerPrefs.SetInt(flag.name, flag.IsActive ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public StageData Load(StageManager manager)
    {
        StageData data = null;

        var name = PlayerPrefs.GetString("stage");
        if(name.Length > 0)
        {
            data = manager.Stages.FirstOrDefault(s => s.name == name);
        }
        foreach(var flag in flags)
        {
            bool active = PlayerPrefs.GetInt(flag.name) == 1;
            flag.IsActive = false;
            if (active)
                flag.Activate();
        }

        return data;
    }

    public void ResetSave()
    {
        PlayerPrefs.SetString("stage", "");
        foreach(var flag in flags)
        {
            PlayerPrefs.SetInt(flag.name, 0);
        }
    }
}
