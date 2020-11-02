using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : Shake
{
    static ScreenShake _instance;
    public static ScreenShake Instance => _instance;

    void Awake()
    {
        _instance = this;
    }

    static public void Shake(float trauma)
    {
        _instance.Trauma = Mathf.Max(trauma, _instance.Trauma);
        if (trauma > .5f)
            Quake(trauma - .4f);
    }
    static public void Wobble(float drunk) => _instance.Drunk = drunk;
    static public void Quake(float power) => _instance.Rumble = Mathf.Max(power, _instance.Rumble, _instance.Rumble * .65f + power * .65f);
    static public void DirectionalShake(Vector2 direction) => _instance.StartWobble(direction);
	
    static public void Shake(float trauma, Vector2 sourceLocation)
    {
        var promoximity = Mathf.Max(Mathf.Abs(sourceLocation.x - _instance.transform.position.x) / 8f, Mathf.Abs(sourceLocation.y - _instance.transform.position.y) / 4f);
        if (promoximity < 1f)
            Shake(trauma);
        else if (promoximity < 1.4f)
            Shake(trauma * (1.5f - promoximity));
    }
}
