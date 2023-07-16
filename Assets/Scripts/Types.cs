using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddableInt 
    : IComparable<AddableInt>, IAddable<AddableInt>, IEquatable<AddableInt>
{
    public int Value { get; }

    public AddableInt(int value)
    {
        Value = value;
    }

    public int CompareTo(AddableInt other)
    {
        return Value.CompareTo(other.Value);
    }

    public bool Equals(AddableInt other)
    {
        return Value.Equals(other.Value);
    }

    public AddableInt Add(AddableInt other)
    {
        return Value + other.Value;
    }

    public static implicit operator AddableInt(int value)
    {
        return new AddableInt(value);
    }
}

public class ReverseInt
    : IComparable<ReverseInt>, IAddable<ReverseInt>, IEquatable<ReverseInt>
{
    public int Value { get; }

    public ReverseInt(int value)
    {
        Value = value;
    }

    public int CompareTo(ReverseInt other)
    {
        // Reversed comparation
        return -Value.CompareTo(other.Value);
    }

    public bool Equals(ReverseInt other)
    {
        return Value.Equals(other.Value);
    }

    public ReverseInt Add(ReverseInt other)
    {
        return Value + other.Value;
    }

    public static implicit operator ReverseInt(int value)
    {
        return new ReverseInt(value);
    }
}

public class AddableFloat 
    : IComparable<AddableFloat>, IAddable<AddableFloat>, IEquatable<AddableFloat>
{
    public float Value { get; }

    public AddableFloat(float value)
    {
        Value = value;
    }

    public int CompareTo(AddableFloat other)
    {
        return Value.CompareTo(other.Value);
    }

    public bool Equals(AddableFloat other)
    {
        return Value.Equals(other.Value);
    }

    public AddableFloat Add(AddableFloat other)
    {
        return Value + other.Value;
    }

    public static implicit operator AddableFloat(float value)
    {
        return new AddableFloat(value);
    }
}

public class ReverseFloat 
    : IComparable<ReverseFloat>, IAddable<ReverseFloat>, IEquatable<ReverseFloat>
{
    public float Value { get; }

    public ReverseFloat(float value)
    {
        Value = value;
    }

    public int CompareTo(ReverseFloat other)
    {
        // Reversed comparation
        return -Value.CompareTo(other.Value);
    }

    public bool Equals(ReverseFloat other)
    {
        return Value.Equals(other.Value);
    }

    public ReverseFloat Add(ReverseFloat other)
    {
        return Value + other.Value;
    }

    public static implicit operator ReverseFloat(float value)
    {
        return new ReverseFloat(value);
    }
}

public class AddableDouble 
    : IComparable<AddableDouble>, IAddable<AddableDouble>, IEquatable<AddableDouble>
{
    public double Value { get; }

    public AddableDouble(double value)
    {
        Value = value;
    }

    public int CompareTo(AddableDouble other)
    {
        return Value.CompareTo(other.Value);
    }

    public bool Equals(AddableDouble other)
    {
        return Value.Equals(other.Value);
    }

    public AddableDouble Add(AddableDouble other)
    {
        return Value + other.Value;
    }

    public static implicit operator AddableDouble(double value)
    {
        return new AddableDouble(value);
    }
}

public class ReverseDouble 
    : IComparable<ReverseDouble>, IAddable<ReverseDouble>, IEquatable<ReverseDouble>
{
    public double Value { get; }

    public ReverseDouble(double value)
    {
        Value = value;
    }

    public int CompareTo(ReverseDouble other)
    {
        // Reversed comparation
        return -Value.CompareTo(other.Value);
    }

    public bool Equals(ReverseDouble other)
    {
        return Value.Equals(other.Value);
    }

    public ReverseDouble Add(ReverseDouble other)
    {
        return Value + other.Value;
    }

    public static implicit operator ReverseDouble(double value)
    {
        return new ReverseDouble(value);
    }
}