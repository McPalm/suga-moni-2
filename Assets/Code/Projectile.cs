using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 Direction;
    public float gravity = 9f;

    int lag = 0;

    public bool DestroyOnTouchingGround;
    public bool StickToGround;
    public float yScatter = 0f;

    static System.Lazy<LayerMask> Solid = new System.Lazy<LayerMask>(() => LayerMask.GetMask("Solid"));

    void Start()
    {
        if (yScatter != 0f)
            Direction += Vector2.up * yScatter * (.5f - Random.value);
    }
    static RaycastHit2D hit;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (StickToGround || DestroyOnTouchingGround)
            hit = Physics2D.BoxCast(transform.position, new Vector2(.01f, .01f), 0f, Vector2.zero, 0f, Solid.Value);

        if (lag > 0)
            lag--;
        else if (DestroyOnTouchingGround && hit)
            Destroy(gameObject);
        else if (StickToGround && hit)
        {
            transform.position = hit.point;
            enabled = false;
            Destroy(gameObject, 3f);

        }
        else if (OnCamera())
        {
            if (gravity != 0f)
                Direction += Vector2.down * Time.fixedDeltaTime * gravity;
            transform.Translate(Direction * Time.fixedDeltaTime);
        }
        else
            Destroy(gameObject);
    }

    bool OnCamera()
    {
        return transform.OnCamera(40f);
    }
}
