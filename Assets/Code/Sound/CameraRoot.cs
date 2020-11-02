using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CameraRoot : MonoBehaviour
{
    PixelPerfectCamera Camera;
    public static Transform Root { get; private set; }

    private void Awake()
    {
        Root = transform;
    }

    void Start()
    {
        Camera = FindObjectOfType<PixelPerfectCamera>();
        if (!Camera)
            enabled = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.localPosition = Vector3.zero;
        transform.position = Camera.RoundToPixel(transform.position);
    }
}
