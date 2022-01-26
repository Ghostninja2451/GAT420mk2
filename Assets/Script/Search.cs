using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Priority_Queue;

public static class Search
{
    public delegate bool SearchAlgorithm(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps);

    static public bool BuildPath(SearchAlgorithm searchAlgorithm, GraphNode source, GraphNode destination, ref List<GraphNode> path, int steps = int.MaxValue)
    {
        if (source == null || destination == null) return false;

        // reset graph nodes
        GraphNode.ResetNodes();

        // search for path from source to destination nodes        
        bool found = searchAlgorithm(source, destination, ref path, steps);
        return found;
    }

    public static bool DFS(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
        bool found = false;

        var nodes = new Stack<GraphNode>();

        nodes.Push(source);

        int steps = 0;
        while (!found && nodes.Count > 0 && steps++ < maxSteps)
        {
            var node = nodes.Peek();
            node.visited = true;

            bool forward = false;

            foreach(var neighbor in node.neighbors)
            {
                if(!neighbor.visited)
                {
                    nodes.Push(neighbor);
                    forward = true;

                    if(neighbor == destination)
                    {
                        found = true;
                    }

                    break;
                }
            }
            if(!forward)
            {
                nodes.Pop();
            }
        }

        path = new List<GraphNode>(nodes);
        path.Reverse();

        return found;
    }



    public static bool BFS(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
        bool found = false;
        var nodes = new Queue<GraphNode>();
        source.visited = true;
        nodes.Enqueue(source);

        int step = 0;
        while(!found && nodes.Count > 0 && step++ < maxSteps)
        {
            var node = nodes.Dequeue();

            foreach(var neighbor in node.neighbors)
            {
                if(neighbor.visited == false)
                {
                    neighbor.visited = true;
                    neighbor.parent = node;
                    nodes.Enqueue(neighbor);
                }
                if (neighbor == destination)
                {
                    found = true;
                    break;
                }
            }
        }
        path = new List<GraphNode>();
        if(found)
        {
            var node = destination;

            while(node != null)
            {
                path.Add(node);
                node = node.parent;
            }
            nodes.Reverse();
        }
        else
        {
            path = nodes.ToList();
        }
        return found;
    }
}