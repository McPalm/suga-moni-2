using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToken
{
    // getters & shared
    public bool JumpPressed => _jumpTimer > Time.timeSinceLevelLoad;
    public bool UsePressed => _useTimer > Time.timeSinceLevelLoad;
    public bool PassThrough => _passThroughTimer > Time.timeSinceLevelLoad;
    public bool InteractPressed => _interactTimer > Time.timeSinceLevelLoad;
    public Vector2 Direction { get; set; }
    public bool JumpHeld { get; set; }
    public bool UseHeld { get; set; }
    public bool ClimbHeld { get; set; }


    // from client
    public void ConsumeJump() => _jumpTimer = 0f;
    public void ConsumeUse() => _useTimer = 0f;
    public void ConsumeInteract() => _interactTimer = 0f;

    // from source
    public void PressJump() => _jumpTimer = Time.timeSinceLevelLoad + .15f;
    public void PressUse() => _useTimer = Time.timeSinceLevelLoad + .15f;
    public void PressPassThrough() => _passThroughTimer = Time.timeSinceLevelLoad + .2f;
    public void PressInteract() => _interactTimer = Time.timeSinceLevelLoad + .15f;

    // internal
    float _jumpTimer = 0f;
    float _useTimer = 0f;
    float _passThroughTimer = 0f;
    float _interactTimer = 0f;

    public InputSnapshot GetSnapshot() =>
        new InputSnapshot
        {
            direction = Direction,
            jumpHeld = JumpHeld,
            jumpBufferTimer = _jumpTimer - Time.timeSinceLevelLoad,
        };
}
