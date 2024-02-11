using System;
using System.Collections;
using UnityEngine;

public static class Vector2IntExtensions 
{
    public static Tuple<int, int> ToTuple(this Vector2Int vector)
    {
        return new Tuple<int, int>(vector.x, vector.y);
    }
}
