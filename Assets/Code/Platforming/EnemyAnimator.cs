using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    Animator Animator;
    Mobile mobile;

    public string[] trackedParameters;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        mobile = GetComponent<Mobile>();
        /*
        Health health = GetComponent<Health>();
        if(health)
            health.OnChangeHealth.AddListener(Health_OnChange);
            */
    }
    /*
    private void Health_OnChange(float h)
    {
        Animator.SetBool("Dead", h <= 0f);

    }
    */

    // Update is called once per frame
    void LateUpdate()
    {
        foreach(string para in trackedParameters)
        {
            switch (para)
            {
                case "Grounded":
                    Animator.SetBool("Grounded", mobile.Grounded);
                    break;
                case "Speed":
                    Animator.SetFloat("Speed", Mathf.Abs(mobile.HMomentum));
                    break;
            }
        }
    }
}
