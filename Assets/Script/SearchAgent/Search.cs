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

    public static void CreatePathFromParents(GraphNode node, ref List<GraphNode> path)
    {
        // while node not null
        while (node != null)
        {
            // add node to list path
            path.Add(node);
            // set node to node parent
            node = node.parent;
        }



        // reverse path
        path.Reverse();
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

    public static bool Dijkstra(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
        bool found = false;
        var nodes = new SimplePriorityQueue<GraphNode>();
        source.cost = 0;
        nodes.Enqueue(source, source.cost);

        int steps = 0;
        while (!found && nodes.Count > 0 && steps++ < maxSteps)
        {
            var node = nodes.Dequeue();

            if(node == destination)
            {
                found = true;
                break;
            }
            foreach(var neighbor in node.neighbors)
            {
                neighbor.visited = true;
                float cost = node.cost + node.DistanceTo(neighbor);
                if(cost < neighbor.cost)
                {
                    neighbor.cost = node.cost;
                    neighbor.parent = node;
                    // enqueue without duplicates, neighbor with cost as priority
                    nodes.EnqueueWithoutDuplicates(neighbor, cost);
                }
            }
        }
        if(found)
        {
            path = new List<GraphNode>();
            //CreatePathFromParents(destination, ref path);
            CreatePathFromParents(destination, ref path);

        }
        else
        {
            path = nodes.ToList();
        }

        return found;
    }

    public static bool AStar(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
        bool found = false;

        var nodes = new SimplePriorityQueue<GraphNode>();

        source.cost = 0;

        float heuristice = Vector3.Distance(source.transform.position, destination.transform.position);

        nodes.Enqueue(source, source.cost + heuristice);

        int steps = 0;
        while(!found && nodes.Count > 0 && steps++ < maxSteps)
        {
            var node = nodes.Dequeue();

            if(node == destination)
            {
                found = true;
                break;
            }
            foreach(var neighbor in node.neighbors)
            {
                neighbor.visited = true;

                float cost = node.cost + node.DistanceTo(neighbor);

                if(node.cost < neighbor.cost)
                {
                    neighbor.cost = node.cost;
                    neighbor.parent = node;
                    heuristice = Vector3.Distance(neighbor.transform.position, destination.transform.position);

                    //<EnqueueWithoutDuplicates() neighbor, cost + heuristic>
                    nodes.EnqueueWithoutDuplicates(neighbor, cost + heuristice);
                    
                }
            }
        }
        if(found)
        {
            path = new List<GraphNode>();
            //CreatePathFromParents(destination, ref path);
            CreatePathFromParents(destination, ref path);
            
        }
        else
        {
            path = nodes.ToList();
            
        }

        return found;
    }
}