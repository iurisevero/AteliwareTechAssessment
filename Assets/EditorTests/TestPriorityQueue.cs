using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using DataStructures.PriorityQueue;

public class TestPriorityQueue
{
    [Test]
    public void TestAscendingPriorityQueue() {
        PriorityQueue<int, int> priorityQueue = new PriorityQueue<int, int>(-1);
        priorityQueue.Insert(5, 5);
        priorityQueue.Insert(3, 3);
        Assert.AreEqual(3, priorityQueue.Pop());
        Assert.IsFalse(priorityQueue.IsEmpty());

        priorityQueue.Insert(4, 4);
        Assert.AreEqual(4, priorityQueue.Pop());
        Assert.AreEqual(5, priorityQueue.Top());
        priorityQueue.Pop();
        Assert.Zero(priorityQueue.Top());
        Assert.IsTrue(priorityQueue.IsEmpty());
    }

    [Test]
    public void TestDescendingPriorityQueue() {
        PriorityQueue<int, ReverseInt> priorityQueue = new PriorityQueue<int, ReverseInt>(-1);
        priorityQueue.Insert(3, 3);
        priorityQueue.Insert(5, 5);
        Assert.AreEqual(5, priorityQueue.Pop());
        Assert.IsFalse(priorityQueue.IsEmpty());

        priorityQueue.Insert(4, 4);
        Assert.AreEqual(4, priorityQueue.Pop());
        Assert.AreEqual(3, priorityQueue.Top());
        priorityQueue.Pop();
        Assert.Zero(priorityQueue.Top());
        Assert.IsTrue(priorityQueue.IsEmpty());
    }
}
