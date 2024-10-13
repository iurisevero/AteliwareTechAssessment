using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStructures.PriorityQueue;

public class WeightedGraph<TElement, TWeight> 
    where TWeight : IComparable<TWeight>, IAddable<TWeight>
{
    TWeight infiniteNumber;
    public Dictionary<TElement, List<(TElement, TWeight)>> nodes { get; private set; }
    public Dictionary<TElement, TWeight> distances { get; private set; }
    public Dictionary<TElement, (TElement, TWeight)> parent { get; private set; }
    public Dictionary<TElement, Boolean> visited { get; private set; }

    public WeightedGraph(TWeight infiniteNumber) {
        this.infiniteNumber = infiniteNumber;
        this.distances = new Dictionary<TElement, TWeight>();
        this.parent = new Dictionary<TElement, (TElement, TWeight)>();
        this.visited = new Dictionary<TElement, bool>();
        this.nodes = new Dictionary<TElement, List<(TElement, TWeight)>>();
    }

    public WeightedGraph(
        TWeight infiniteNumber, Dictionary<TElement, List<(TElement, TWeight)>> nodes
    ) {
        this.infiniteNumber = infiniteNumber;
        this.distances = new Dictionary<TElement, TWeight>();
        this.visited = new Dictionary<TElement, bool>();
        this.nodes = new Dictionary<TElement, List<(TElement, TWeight)>>(nodes);
    }

    public void SetInfiniteNumber(TWeight infiniteNumber) {
        this.infiniteNumber = infiniteNumber;
    }

    public void AddNode(TElement source) {
        if(!nodes.ContainsKey(source)) {
            this.nodes[source] = new List<(TElement, TWeight)>();
        }
    }

    public void AddNode(TElement source, TElement destiny, TWeight weight) {
        if(nodes.ContainsKey(source)) {
            this.nodes[source].Add((destiny, weight));
        } else {
            this.nodes[source] = new List<(TElement, TWeight)>(){(destiny, weight)};
        }
    }

    public void AddNode(TElement source, List<(TElement, TWeight)> neighbors) {
        if(nodes.ContainsKey(source)) {
            this.nodes[source].AddRange(neighbors);
        } else {
            this.nodes[source] = new List<(TElement, TWeight)>(neighbors);
        }
    }

    public void AddNode(Dictionary<TElement, List<(TElement, TWeight)>> nodes) {
        this.nodes = nodes;
    }

    public void Clear() {
        this.infiniteNumber = default(TWeight);
        this.nodes.Clear();
        this.distances.Clear();
        this.visited.Clear();
    }

    public Boolean DepthFirstSeach(TElement source, TElement destiny) {
        foreach(var element in this.nodes) {
            this.visited[element.Key] = false;
        }
        DepthFirstSearchRecursive(source);
        return this.visited[destiny];
    }

    void DepthFirstSearchRecursive(TElement node) {
        this.visited[node] = true;

        foreach(var neightbor in this.nodes[node]) {
            if (!this.visited[neightbor.Item1]) {
                DepthFirstSearchRecursive(neightbor.Item1);
            }
        }
    }

    public void BreadthFirstSearch(TElement initialVertice = default(TElement)) {
        foreach(var element in this.nodes) {
            this.visited[element.Key] = false;
        }

        Queue<TElement> toVisit = new Queue<TElement>();
        toVisit.Enqueue(initialVertice);
        this.visited[initialVertice] = true;

        while(toVisit.Count > 0) {
            TElement node = toVisit.Dequeue();

            foreach(var neighbor in nodes[node]) {
                if (!this.visited[neighbor.Item1]) {
                    this.visited[neighbor.Item1] = true;
                    toVisit.Enqueue(neighbor.Item1);
                }
            }
        }
    }

    // Single Source Shortest Path
    public TWeight SSSPDijkstra(
        TElement source, TElement destiny, TWeight sourceDistance, TWeight minPriority
    ) {
        foreach(var element in this.nodes) {
            this.distances[element.Key] = infiniteNumber;
            this.visited[element.Key] = false;
            this.parent[element.Key] = (default(TElement), infiniteNumber);
        }

        
        PriorityQueue<(TElement, TWeight), TWeight> toVisit = 
            new PriorityQueue<(TElement, TWeight), TWeight>(minPriority);

        this.distances[source] = sourceDistance;
        this.parent[source] = (source, sourceDistance);

        toVisit.Insert(
            (source, this.distances[source]), 
            this.distances[source]
        );
        

        while(!toVisit.IsEmpty()) {
            var node = toVisit.Top().Item1;
            var distance = toVisit.Top().Item2;
            toVisit.Pop();
            
            if (distance.CompareTo(this.distances[node]) > 0) continue;

            if(this.visited[node]) continue;

            this.visited[node] = true;

            foreach(var neighbor in this.nodes[node]) {
                var neighborNode = neighbor.Item1;
                var neighborDistance = neighbor.Item2;

                if (this.distances[node].Add(neighborDistance).CompareTo(
                    this.distances[neighborNode]) < 0
                ) {
                    this.distances[neighborNode]  = 
                        this.distances[node].Add(neighborDistance);
                    this.parent[neighborNode] = (node, neighborDistance); 
                    toVisit.Insert(
                        (neighborNode, this.distances[neighborNode]), 
                        this.distances[neighborNode]
                    );
                }
            }
        }

        return this.distances[destiny];
    }

    public List<(TElement, (TElement, TWeight))> GetSSPDPath(TElement source, TElement destiny) {
        Debug.Log($"GetSSPDPath: {source} -> {destiny}");
        List<(TElement, (TElement, TWeight))> path = new List<(TElement, (TElement, TWeight))>();
        TElement node = destiny;

        do {
            var parentNode = this.parent[node];
            path.Add((parentNode.Item1, (node, parentNode.Item2)));
            node = parentNode.Item1;
        } while (!node.Equals(source) || node.Equals(default(TElement)));

        path.Reverse();
        return path;
    }
}
