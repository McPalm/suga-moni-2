using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformingAnimator : MonoBehaviour
{
    public Animator Animator;
    PlatformingCharacter Character { get; set; }
    public AudioClip JumpSound;
    public AudioClip walkSound;
    float fallDuration = 0f;

    public ParticleSystem JumpParticles;
    public ParticleSystem RunParticles;

    void Start()
    {
        Character = GetComponent<PlatformingCharacter>();
        Character.OnJump += PlatformingCharacter_OnJump;
        Character.OnStomp += (o) => PlatformingCharacter_OnJump();
        Character.OnWallJump += PlatformingCharacter_OnJump;
        GetComponent<DoubleJump>().OnDoubleJump += PlatformingAnimator_OnDoubleJump;
        // Health = GetComponent<Health>();
        // Health.OnHurt += Health_OnHurt;
        // Health.OnKill += Health_OnKill;
    }



    private void Health_OnKill()
    {
        Animator.SetTrigger("Kill");
    }

    private void Health_OnHurt()
    {
        Animator.SetTrigger("Hurt");
    }

    public void WalkSound()
    {
        if(Character.Grounded)
        {
            var speed = Mathf.Abs(Character.Speed) * .025f;
            speed = speed < .07f ? speed : .07f;
            AudioPool.PlaySound(transform.position, walkSound, volume: Random.value * speed + speed, pitch: .8f + Random.value* .4f);
        }
    }

    private void PlatformingCharacter_OnJump()
    {
        Animator.SetTrigger("Jump");
        JumpParticles.Play();
        AudioPool.PlaySound(transform.position, JumpSound, volume: .45f, pitch: Random.value * .2f + .9f);
    }

    private void PlatformingAnimator_OnDoubleJump()
    {
        Animator.SetTrigger("DoubleJump");
        JumpParticles.Play();
        AudioPool.PlaySound(transform.position, JumpSound, volume: .45f, pitch: Random.value * .2f + .9f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        fallDuration = Character.Grounded ? 0f : fallDuration + Time.deltaTime;
        Animator.SetFloat("Speed", Mathf.Abs(Character.HMomentum - fallDuration * 3f));
        Animator.SetFloat("VSpeed", Character.VMomentum);
        Animator.SetBool("Grounded", Character.Grounded);
        Animator.SetBool("WallSliding", Character.WallSliding);
        if(Character.Grounded && Mathf.Abs(Character.HMomentum) > 2f)
        {
            if (RunParticles.isStopped)
                RunParticles.Play();
        }
        else
        {
            if (RunParticles.isPlaying)
                RunParticles.Stop();
        }
        // Animator.SetFloat("FallDuration", fallDuration);
        // Animator.SetBool("TouchingWall", Character.TouchingWall && Character.TouchingWallDirection != Character.Forward);
        // Animator.SetBool("Dead", Health.CurrentHealth <= 0);
    }
}
