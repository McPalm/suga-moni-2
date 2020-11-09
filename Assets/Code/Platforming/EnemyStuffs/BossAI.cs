using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossAI : MonoBehaviour, IOnStageReset, PlatformingCharacter.IStompable
{
    Vector2 spawn;
    public Transform LeftSide;
    public Transform RightSide;


    public AudioClip FireballNoise;
    public GameObject FireballPrefab;
    public GameObject PigProjectilePrefab;
    public Transform SpawnPoint;

    public ObjectRef Player;

    float SqrDistanceToplayer => (Player.GameObject.transform.position - transform.position).sqrMagnitude;

    Coroutine Coroutine;

    int hits = 0;

    public ParticleSystem KillParticles;
    public ParticleSystem ChargeParticles;
    public AudioClip ChargeSound;
    public PlayableDirector Anvil;
    public PlayableDirector Credits;

    Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        spawn = transform.position;
        Coroutine = StartCoroutine(Routine(0));
    }

    IEnumerator Routine(int phase)
    {
        int r = 3;
        if (phase == 0)
        {
            transform.position = spawn;
            yield return new WaitForSeconds(1f);
        }
        while (SqrDistanceToplayer > 27f * 27f)
            yield return new WaitForSeconds(.1f);
        if(phase % 2 == 0)
        {

            yield return MoveTo(LeftSide.transform.position);
            transform.SetForward(1f);
        }
        else
        {
            yield return MoveTo(RightSide.transform.position);
            transform.SetForward(-1f);
        }
        Aim(1f);
    fireball:
        r+= 2;
        for (int i = 0; i < 4 + phase; i++)
        {
            Aim(.7f - phase * .1f);
            if (SqrDistanceToplayer < 16f)
                goto juke;
            if (i == r % (4 + phase))
                ShootPig();
            else
                ShootFireball();
            yield return new WaitForSeconds(.3f - phase * .08f);
        }

        Aim(.5f);
        yield return new WaitForSeconds(.9f - phase * .05f);
        goto fireball;
    juke:
        // yield return MoveTo(RightSide.transform.position);
        yield return new WaitForSeconds(.5f);
        goto juke;
        
    }

    IEnumerator KillMe()
    {
        yield return MoveTo(RightSide.position + Vector3.right);
        transform.SetForward(-1);
        yield return new WaitForSeconds(.2f);
        ChargeParticles.Play();
        this.Noise(ChargeSound);
        yield return new WaitForSeconds(ChargeSound.length - .0666f);
        
        Anvil.Play();
        yield return new WaitForSeconds(0.0666f);
        ChargeParticles.Stop();
        KillParticles.Play();
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(.2f);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1f);
        yield return CreditRoll();
    }

    void Aim(float ammount)
    {
        target = Vector2.Lerp(target, Player.GameObject.transform.position, ammount);
    }

    void ShootFireball()
    {
        Vector3 playerPosition = target;

        this.Noise(FireballNoise, volume: .75f);
        playerPosition += Vector3.down * hits * .5f;
        var fireball = Instantiate(FireballPrefab, SpawnPoint.position, Quaternion.identity);
        fireball.transform.right = playerPosition - SpawnPoint.position;
    }

    void ShootPig()
    {
        Vector3 playerPosition = target;
        playerPosition += Vector3.up * hits * .1f;

        this.Noise(FireballNoise, volume: .75f);
        var fireball = Instantiate(PigProjectilePrefab, SpawnPoint.position, Quaternion.identity);
        fireball.transform.right = playerPosition - SpawnPoint.position;
    }

    public void OnStageReset()
    {
        hits = 0;
        if (Coroutine != null)
            StopCoroutine(Coroutine);
        Coroutine = StartCoroutine(Routine(0));
    }

    public IEnumerator MoveTo(Vector2 destination)
    {
        Vector2 start = transform.position;
        for (float f = 0; f < 1f; f += Time.deltaTime)
        {
            transform.position = Vector2.Lerp(start, destination, f);
            yield return null;
        }
        transform.position = destination;
    }

    public void Stomp(PlatformingCharacter source)
    {
        hits++;

        StopCoroutine(Coroutine);
        if(hits >= 3)
        {

            Coroutine = StartCoroutine(KillMe());
        }
        else
        {
            Coroutine = StartCoroutine(Routine(hits));
        }
    }

    IEnumerator CreditRoll()
    {
        Time.timeScale = 0f;
        Credits.Play();
        while (Credits.time < Credits.duration)
            yield return null;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
