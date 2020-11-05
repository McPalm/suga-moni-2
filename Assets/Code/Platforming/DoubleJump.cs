using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlatformingCharacter;

public class DoubleJump : MonoBehaviour, IPlatformingTechnique
{

    public event System.Action OnDoubleJump;

    public float chargeDuration = 1f;
    public float power = 15f;
    public float hPower = 15f;


    public int washFrames;

    public int denyFrames = 0;

    PlatformingCharacter PC;

    void Start()
    {
        PC = GetComponent<PlatformingCharacter>();
        PC.OnStomp += PC_OnStomp;
    }

    private void PC_OnStomp(PlatformingCharacter obj)
    {
        denyFrames = 10;
        available = true;
    }

    private bool available;

    public bool Enabled => enabled;

    void Jump(Vector2 direction)
    {
        if(direction.x < -.5f)
        {
            PC.VMomentum = hPower;
            PC.HMomentum = -hPower;
            transform.SetForward(-1);
        }
        else if (direction.x > .5f)
        {
            PC.VMomentum = hPower;
            PC.HMomentum = hPower;
            transform.SetForward(1);
        }
        else
        {
            PC.VMomentum = power;
        }
        available = false;
        PC.ForceJumpUntillPeak = true;
        OnDoubleJump?.Invoke();
    }

    public (bool jumpConsumed, int cyoteTime) SimulateFrame(InputSnapshot input, int cyoteTime)
    {
        // double jump if...
        // we are in air
        // and jump was pressed
        // ..well that was easy
        // but also not right after bumping on someones head
        bool jumpConsumed = false;
        denyFrames--;

        if(PC.Grounded)
        {
            available = true;
        }
        else
        {
            if(available && input.jumpBufferTimer > 0f)
            {
                if (AboutToStomp())
                {
                    // ride it out i guess?
                }
                else
                {
                    if (denyFrames <= 0)
                        Jump(input.direction);
                    jumpConsumed = true;
                    cyoteTime = 0;
                }
            }
        }
        return (jumpConsumed, cyoteTime);
    }

    public bool AboutToStomp()
    {
        if (PC.VMomentum > 2f)
            return false;

        var hits = Physics2D.BoxCastAll(
                origin: new Vector2(transform.position.x, transform.position.y - PC.radius + .08f),
                size: new Vector2(PC.radius * .9f, .1f),
                angle: 0f,
                direction: Vector2.down,
                distance: -PC.VMomentum * Time.fixedDeltaTime * 6 + .34f,
                layerMask: PC.Player + PC.Ground
                );

        return hits.Length > 1;
    }
}
