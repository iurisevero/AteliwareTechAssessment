using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseInt : IComparable<ReverseInt>
{
    public int Value { get; }

    public ReverseInt(int value)
    {
        Value = value;
    }

    public int CompareTo(ReverseInt other)
    {
        // Comparação invertida
        return -Value.CompareTo(other.Value);
    }

    public static implicit operator ReverseInt(int value)
    {
        return new ReverseInt(value);
    }
}