using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public float heuristic;
    public float totalCost;
    public Vector3 position;
    public Node previousNode;

    public Node(float heuristic, Vector3 position, Node previousNode)
    {
        this.heuristic = heuristic;
        this.position = position;
        this.previousNode = previousNode;
        if(previousNode == null) totalCost = 1;
        else totalCost += previousNode.totalCost + heuristic;
    }
    
}
