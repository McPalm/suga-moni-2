using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerRelay : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    public float delay = 0f;

    public StoryFlag RequiresFlag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(Enter());
    }

    private IEnumerator Enter()
    {
        if (RequiresFlag == null || RequiresFlag.IsActive)
        {

            if (delay > 0f)
                yield return new WaitForSeconds(delay);
            OnEnter.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(Exit());
    }

    private IEnumerator Exit()
    {
        if (RequiresFlag == null || RequiresFlag.IsActive)
        {
            if (delay > 0f)
                yield return new WaitForSeconds(delay);
            OnExit.Invoke();
        }
    }
}
