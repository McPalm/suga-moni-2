using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public Selectable DefaultSelect;

    private void OnEnable()
    {
        DefaultSelect.Select();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
