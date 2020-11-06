using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(string.IsNullOrEmpty(PlayerPrefs.GetString("LoadMode")))
        {
            NewGame();
        }
    }


    public void NewGame()
    {
        PlayerPrefs.SetString("LoadMode", "NewGame");
        SceneManager.LoadScene(1);
    }


    public void LoadGame()
    {
        PlayerPrefs.SetString("LoadMode", "LoadGame");
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
