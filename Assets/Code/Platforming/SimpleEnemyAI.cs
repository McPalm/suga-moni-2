using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAI : MonoBehaviour, IOnStageReset
{
    PlatformingCharacter Controls;

    InputToken InputToken;
    public bool turnAtLedges;

    Coroutine Coroutine;

    Vector3 startPosition;
    Quaternion startRotation;

    void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        Coroutine = StartCoroutine(DaLoop());
    }

    public void OnStageReset()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        if (Coroutine != null)
            StopCoroutine(Coroutine);
        Coroutine = StartCoroutine(DaLoop());
    }
    

    IEnumerator DaLoop()
    {
        Controls = GetComponent<PlatformingCharacter>();
        InputToken = new InputToken();
        Controls.InputToken = InputToken;
        int direction = Controls.Forward;
        InputToken.Direction = new Vector2(direction, 0f);
        yield return new WaitForSeconds(.1f);

    MoveForward:
        for(; ; )
        {
            InputToken.Direction = new Vector2(direction, 0f);
            if (Controls.TouchingWall && Controls.TouchingWallDirection == direction)
                goto Turn;
            if (turnAtLedges && Controls.Grounded && Controls.OnEdge)
                goto Turn;
            yield return new WaitForFixedUpdate();
        }
    Turn:
        direction = -direction;
            InputToken.Direction = new Vector2(direction, 0f);
        yield return new WaitForSeconds(.1f);
        goto MoveForward;

    }
}
