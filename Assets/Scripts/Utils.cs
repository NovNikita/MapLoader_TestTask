using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Struct for storing map borders
public struct MapBordersCorrdinates
{
    public float Left { get; }
    public float Right { get; }
    public float Up { get; }
    public float Down { get; }


    public MapBordersCorrdinates(float leftBorder, float rightBorder, float upBorder, float downBorder)
    {
        Left = leftBorder;
        Right = rightBorder;
        Up = upBorder;
        Down = downBorder;
    }
}

