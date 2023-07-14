using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestUtilities
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestChessboardToMatriz() {
        (int, int) coordinate = Utilities.ChessboardToMatriz("B1");
        Assert.AreEqual((1, 7), coordinate);

        coordinate = Utilities.ChessboardToMatriz("H3");
        Assert.AreEqual((7, 5), coordinate);

        coordinate = Utilities.ChessboardToMatriz("d8");
        Assert.AreEqual((3, 0), coordinate);

        coordinate = Utilities.ChessboardToMatriz("J9");
        Assert.AreEqual((-1, -1), coordinate);
    }

    [Test]
    public void TestCheckChessboardCoordinate() {
        Assert.IsTrue(Utilities.CheckChessboardCoordinate("A5"));
        Assert.IsFalse(Utilities.CheckChessboardCoordinate("b5"));
        Assert.IsFalse(Utilities.CheckChessboardCoordinate("D9"));
        Assert.IsFalse(Utilities.CheckChessboardCoordinate("J1"));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    // [UnityTest]
    // public IEnumerator TestUtilitiesWithEnumeratorPasses()
    // {
    //     // Use the Assert class to test conditions.
    //     // Use yield to skip a frame.
    //     yield return null;
    // }
}
