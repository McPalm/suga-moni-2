using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLoadTrigger : MonoBehaviour
{
    public Stage stage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<StageManager>().Enter(stage);
    }
}
