using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorScaler : MonoBehaviour
{
    public Transform startAnchor;
    public Transform endAnchor;

    public float defaultLength;

    private Vector3 desiredScale;

    public bool usePosition;
    public bool useRotation;
    public bool useScale;

    void Start()
    {
        desiredScale = Vector3.one;
    }

    void Update()
    {
        //Get the difference between the two vectors
        Vector3 difference = (endAnchor.position - startAnchor.position);

        if (usePosition)
            transform.position = (endAnchor.position + startAnchor.position) / 2;
        if (useRotation)
            transform.rotation = Quaternion.Euler(Vector3.forward * (Mathf.Rad2Deg * Mathf.Atan2(difference.y, difference.x)));

        if (useScale)
        {
            desiredScale.x = difference.magnitude / defaultLength;
            transform.localScale = desiredScale;
        }
    }
}