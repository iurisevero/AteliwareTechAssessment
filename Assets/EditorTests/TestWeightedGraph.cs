using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestWeightedGraph
{
    [Test]
    public void TestWeightedGraphConstructor() {
        Dictionary<int, List<(int, AddableInt)>> expectedNodes = 
            new Dictionary<int, List<(int, AddableInt)>>(){
                {0, new List<(int, AddableInt)>{(1, 50), (2, 150)}},
                {1, new List<(int, AddableInt)>{}},
                {2, new List<(int, AddableInt)>{(1, 70)}}
            };
        
        WeightedGraph<int, AddableInt> graph = 
            new WeightedGraph<int, AddableInt>(int.MaxValue);

        graph.AddNode(0, 1, 50);
        graph.AddNode(0, 2, 150);
        graph.AddNode(1);
        graph.AddNode(2, 1, 70);

        Assert.AreEqual(expectedNodes, graph.nodes);

        graph.Clear();
        graph.SetInfiniteNumber(int.MaxValue);
        graph.AddNode(0, new List<(int, AddableInt)>{(1, 50), (2, 150)});
        graph.AddNode(1, new List<(int, AddableInt)>{});
        graph.AddNode(2, new List<(int, AddableInt)>{(1, 70)});

        Assert.AreEqual(expectedNodes, graph.nodes);

        graph.Clear();
        graph.SetInfiniteNumber(int.MaxValue);
        graph.AddNode(expectedNodes);

        Assert.AreEqual(expectedNodes, graph.nodes);
    }

    [Test]
    public void TestWeightedGraphTraversal() {
        WeightedGraph<int, AddableInt> graph = 
            new WeightedGraph<int, AddableInt>(int.MaxValue);
        graph.AddNode(0, 1, 50);
        graph.AddNode(0, 2, 150);
        graph.AddNode(1, 0, 50);
        graph.AddNode(1, 2, 70);
        graph.AddNode(2, 0, 150);
        graph.AddNode(2, 1, 70);

        Assert.IsTrue(graph.DepthFirstSeach(0, 2));
        graph.BreadthFirstSearch(0);
        Assert.IsTrue(graph.visited[2]);
        Assert.AreEqual(new AddableInt(120), graph.SSSPDijkstra(0, 2, default(int), -1));

        graph.AddNode(3);
        Assert.IsFalse(graph.DepthFirstSeach(0, 3));
        graph.BreadthFirstSearch(0);
        Assert.IsFalse(graph.visited[3]);
        Assert.AreEqual(
            new AddableInt(int.MaxValue), 
            graph.SSSPDijkstra(0, 3, default(int), -1)
        );
    }
}