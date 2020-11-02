using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float multiplier = 1f;
    public float speed = 60f;
    public float decaySpeed = 1f;

    public float Trauma = 0f;
    public float Drunk = 0f;
    public float Rumble = 0f;
    Vector2 WobbleDirection;
    float WobbleDuration;

         
    Vector3 orig;

    float time;

    Camera c;

    private void Start()
    {
        orig = transform.localPosition;
        c = GetComponent<Camera>();
    }

    public void StartWobble(Vector2 direction)
    {
        WobbleDuration = Mathf.PI * 3f;
        WobbleDirection = direction;
    }

    private void Update()
    {
        Vector3 offset = Vector3.zero;
        float roll = 0f;
        
        if(WobbleDuration > 0f)
        {
            offset += (Vector3)(WobbleDirection * Mathf.Sin(WobbleDuration) * WobbleDuration);
            WobbleDuration -= Time.deltaTime * 18f;
        }
        if (Trauma > 0f)
        {
            time = Time.timeSinceLevelLoad * speed * .66f + Time.realtimeSinceStartup * speed * .33f;
            float magnitude = Trauma * Trauma;
            offset += magnitude * new Vector3(.5f - Mathf.PerlinNoise(1f, time), .5f - Mathf.PerlinNoise(10f, time));
            roll += magnitude * ( .5f - Mathf.PerlinNoise(20f, time)) * 3f;
            Trauma -= (Trauma > .66f) ? Time.unscaledDeltaTime * Trauma  * 1.5f *  decaySpeed: Time.unscaledDeltaTime * decaySpeed;
        }
        if(Rumble > 0f)
        {
            Rumble -= Time.unscaledDeltaTime * .5f * decaySpeed;
        }
        if(Drunk > 0f)
        {
            float magnitude = Drunk * Drunk;
            offset += magnitude * new Vector3( .5f - Mathf.PerlinNoise(1f, Time.timeSinceLevelLoad * 0.22f), .5f - Mathf.PerlinNoise(10f, Time.timeSinceLevelLoad * 0.17f));
            roll += magnitude * (.5f - Mathf.PerlinNoise(20f, Time.timeSinceLevelLoad * 0.2f)) * 1.5f;
        }
        if (c) offset *= c.orthographicSize / 6f;

        transform.localPosition = orig + offset * multiplier;
        transform.localRotation = Quaternion.AngleAxis(roll, Vector3.forward);
    }
}
