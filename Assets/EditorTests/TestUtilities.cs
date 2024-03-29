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
        Vector2 expect = new Vector2(1, 7);
        Vector2 coordinate = Utilities.ChessboardToMatriz("B1");
        Assert.AreEqual(expect, coordinate);

        expect.Set(7, 5);
        coordinate = Utilities.ChessboardToMatriz("H3");
        Assert.AreEqual(expect, coordinate);

        expect.Set(3, 0);
        coordinate = Utilities.ChessboardToMatriz("d8");
        Assert.AreEqual(expect, coordinate);

        expect.Set(-1, -1);
        coordinate = Utilities.ChessboardToMatriz("J9");
        Assert.AreEqual(expect, coordinate);
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
