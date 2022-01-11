using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static Vector3 Wrap (Vector3 v, Vector3 min, Vector3 max)
    {
        Vector3 result = v;
        if (result.x > max.x)
        {
            result.x = min.x;
        }
        if (result.y > max.y)
        {
            result.y = min.y;
        }
        if (result.z > max.z)
        {
            result.z = min.z;
        }
        if (result.x < min.x)
        {
            result.x = max.x;
        }
        if (result.y < min.y)
        {
            result.y = max.y;
        }
        if (result.z < min.z)
        {
            result.z = max.z;
        }


        return result;
    }
}
