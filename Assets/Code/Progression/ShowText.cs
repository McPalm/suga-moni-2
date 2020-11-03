using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    [TextArea]
    public string text;

    // Start is called before the first frame update
    void Start()
    {
        var flagPickup = GetComponent<FlagPickup>();
        if(flagPickup)
        {
            flagPickup.OnTrigger += Show;
        }
    }

    public void Show()
    {
        FindObjectOfType<TextPrompt>().Show(text);
    }

}
