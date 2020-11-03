using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{

    public bool lockout = false;

    public ParticleSystem KillParticles;

    public Vector3 SpawnPoint { get; set; }
    public event System.Action OnReset;
    public event System.Action OnKill;

    public AudioClip[] DeathSounds;

    Mobile Mobile;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPoint = transform.position;
        Mobile = GetComponent<Mobile>();
    }

    private void FixedUpdate()
    {
        if (lockout == false && Mobile.Crushed)
            Kill();
    }

    public void Kill()
    {
        if(lockout == false)
            StartCoroutine(KillRoutine());
    }

    public IEnumerator KillRoutine()
    {
        lockout = true;


        OnKill?.Invoke();
        KillParticles.transform.position = transform.position;
        KillParticles.Play();
        var ren = GetComponent<SpriteRenderer>();
        ren.enabled = false;
        var mob = GetComponent<Mobile>();
        mob.enabled = false;

        if(DeathSounds.Length > 0)
        {
            AudioPool.PlaySound(transform.position, DeathSounds[Random.Range(0, DeathSounds.Length)], pitch: Random.value * .7f + .3f);
        }

        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(.1f);
        Time.timeScale = 1f;

        ScreenShake.Shake(1f);
        ScreenShake.Quake(1f);
        ScreenShake.Instance.StartWobble(new Vector2(mob.HMomentum, mob.VMomentum).normalized * .03f);

        yield return new WaitForSeconds(.5f);

        FindObjectOfType<Stage>().ResetStage();
        OnReset?.Invoke();

        ren.enabled = true;
        mob.enabled = true;
        transform.position = SpawnPoint;

        lockout = false;
    }
}
