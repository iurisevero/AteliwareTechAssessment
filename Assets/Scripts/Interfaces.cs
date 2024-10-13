using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAddable<T>
{
    T Add(T other);
}
