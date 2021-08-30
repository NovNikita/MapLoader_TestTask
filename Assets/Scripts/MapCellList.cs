using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapCellList
{
    public List<MapCell> List;
}

[System.Serializable]
public class MapCell
{
    public string Id;
    public string Type;
    public float Width;
    public float Height;
    public float X;
    public float Y;

    public Texture2D texture;
}