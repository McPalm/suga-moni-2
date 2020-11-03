using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPrompt : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;

    public void Show(string text)
    {
        StartCoroutine(ShowLoop(text));
    }

    public IEnumerator ShowLoop(string text)
    {
        this.text.text = text;
        //Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(.5f);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(5f);
        this.text.text = "";
    }
}
