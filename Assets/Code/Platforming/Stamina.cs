using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stamina : MonoBehaviour
{

    PlatformingCharacter pc;

    public bool HasStamina => exhausted == false && currentstamina > 0f;

    float currentstamina = 0;
    public float maxStamina = 1f;

    bool exhausted = false;

    public UnityEvent<float> OnValueChanged;

    float rechargeCooldown = .7f;
    float rechargeTimer = 0f;
    bool CanRecharge => Time.timeSinceLevelLoad > rechargeTimer && pc.Grounded;

    float expenseCarryover = 0f;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlatformingCharacter>();
        // pc.OnJump += Pc_OnJump;  // I dont think this contributes to a good playstyle, the fact taht being in air interupts recovery is quite good enough.
        pc.OnWallJump += Pc_OnWallJump;
        currentstamina = maxStamina;
    }

    private void Pc_OnWallJump()
    {
        expenseCarryover += .19f;
    }

    private void Pc_OnJump()
    {
        expenseCarryover += .19f;
    }

    public void Spend(float stamina)
    {
        expenseCarryover += stamina;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float expenditure = expenseCarryover;
        expenseCarryover = 0f;
        if (pc.Climbing)
        {
            if (pc.VMomentum > -.1f)
                expenditure += Time.fixedDeltaTime * (pc.VMomentum * .23f + .1f);
        }

        currentstamina -= expenditure;
        if (expenditure > 0f)
            rechargeTimer = Time.timeSinceLevelLoad + rechargeCooldown;
        else if (CanRecharge)
            currentstamina += Time.fixedDeltaTime * (pc.HMomentum == 0f ? 1.5f : .5f);


        if (currentstamina <= 0f)
            exhausted = true;
        if (currentstamina >= maxStamina)
            exhausted = false;

        pc.lowStamina = !HasStamina;

        currentstamina = Mathf.Clamp(currentstamina, 0f, maxStamina);

        OnValueChanged.Invoke(currentstamina / maxStamina);
    }
}
