using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathFunctions
{
    public static float GetAngleFromVector2(Vector2 vec)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(vec.y, vec.x);
    }
    public static Quaternion GetZRotationFromVector2(Vector2 vec)
    {
        return Quaternion.Euler(Vector3.forward * GetAngleFromVector2(vec));
    }
}
