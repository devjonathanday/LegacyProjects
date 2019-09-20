using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ParallaxLayer : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 newPosition;

    public bool scrollX;
    public bool scrollY;
    public float scrollFactorX;
    public float scrollFactorY;
    public Transform mainCamera;

    void Start()
    {
        startPosition = transform.position;
    }

    void LateUpdate()
    {
        newPosition = transform.position;
        if (scrollX) newPosition.x = startPosition.x + mainCamera.position.x * scrollFactorX;
        if (scrollY) newPosition.y = startPosition.y + mainCamera.position.y * scrollFactorY;
        transform.position = newPosition;
    }
}