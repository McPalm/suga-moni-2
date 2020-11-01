using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public InputToken InputToken { get; private set; }
    Vector2 heldDirection;
    float direction = 0f;
    float softTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        InputToken = new InputToken();
        foreach (var ir in GetComponents<IInputReader>())
            ir.InputToken = InputToken;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();

        var x = value.x;
        if (Mathf.Abs(x) > .65f || x == 0f)
        {
            direction = Mathf.Round(x);
            softTimer = Time.timeSinceLevelLoad + .1f;
        }
        else if(softTimer < Time.timeSinceLevelLoad)
        {
            direction = Mathf.Round(x);
        }
        else
        {
            x = x * .2f + heldDirection.x * .8f;
            value = new Vector2(x, value.y);
        }
        

        InputToken.Direction = value;
        if (heldDirection.y > -.5f && InputToken.Direction.y < -.5f)
            InputToken.PressPassThrough();
        heldDirection = InputToken.Direction;
    }

    public void HandleUse(InputAction.CallbackContext e)
    {
        if (e.started)
        {
            InputToken.PressUse();
            InputToken.UseHeld = true;
        }
        else if(e.canceled)
        {
            InputToken.UseHeld = false;
        }
    }

    public void HandleInteract(InputAction.CallbackContext e)
    {
        if (e.started)
        {
            InputToken.PressInteract();
        }
    }

    public void OnJump(InputAction.CallbackContext e)
    {
        if (e.started)
        {
            InputToken.PressJump();
            InputToken.JumpHeld = true;
        }
        else if(e.canceled)
        {
            InputToken.JumpHeld = false;
        }
    }

    public void OnClimb(InputAction.CallbackContext e)
    {
        if (e.started)
            InputToken.ClimbHeld = true;
        else if(e.canceled)
            InputToken.ClimbHeld = false;
    }
}
