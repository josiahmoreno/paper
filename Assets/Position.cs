using PaperLib.Sequence;
using System;
using UnityEngine;

public class Position : IPositionable
{
    public Position(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public float x { get; set ; }
    public float y { get; set; }
    public float z { get ; set ; }

    public IPositionable CopyPosition()
    {
        return new Position(x,y,z);
    }

    internal Vector3 Vector3()
    {
        return new Vector3(x,y,z);
    }
}