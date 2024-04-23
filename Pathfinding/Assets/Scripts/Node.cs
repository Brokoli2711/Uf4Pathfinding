using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public float heuristic;
    public Vector2 position;
    public Node previousNode;

    public Node(float heuristic, Vector2 position, Node previousNode)
    {
        this.heuristic = heuristic;
        this.position = position;
        this.previousNode = previousNode;
    }
    
}
