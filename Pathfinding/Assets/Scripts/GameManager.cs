using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    bool si = false;
    public GameObject token1, token2, token3;
    private int[,] GameMatrix; //0 not chosen, 1 player, 2 enemy
    private int[] startPos = new int[2];
    private int[] objectivePos = new int[2];
    private List<Node> openList = new List<Node>();
    private List<Node> closeList = new List<Node>();    

    private void Awake()
    {
        GameMatrix = new int[Calculator.length, Calculator.length];

        for (int i = 0; i < Calculator.length; i++) //fila
            for (int j = 0; j < Calculator.length; j++) //columna
                GameMatrix[i, j] = 0;

        //randomitzar pos final i inicial;
        var rand1 = Random.Range(0, Calculator.length);
        var rand2 = Random.Range(0, Calculator.length);
        startPos[0] = rand1;
        startPos[1] = rand2;
        SetObjectivePoint(startPos);

        GameMatrix[startPos[0], startPos[1]] = 1;
        GameMatrix[objectivePos[0], objectivePos[1]] = 2;

        InstantiateToken(token1, startPos);
        InstantiateToken(token2, objectivePos);
        ShowMatrix();
        //Seteamos el primer nodo al inicial.
        openList.Add(new Node(Calculator.CheckDistanceToObj(startPos, objectivePos), new Vector2(startPos[0], startPos[1]), null));
        closeList.Add(new Node(Calculator.CheckDistanceToObj(startPos, objectivePos), new Vector2(startPos[0], startPos[1]), null));
    }
    private void InstantiateToken(GameObject token, int[] position)
    {
        Instantiate(token, Calculator.GetPositionFromMatrix(position),
            Quaternion.identity);
    }
    private void SetObjectivePoint(int[] startPos) 
    {
        var rand1 = Random.Range(0, Calculator.length);
        var rand2 = Random.Range(0, Calculator.length);
        if (rand1 != startPos[0] || rand2 != startPos[1])
        {
            objectivePos[0] = rand1;
            objectivePos[1] = rand2;
        }
    }

    private void ShowMatrix() //fa un debug log de la matriu
    {
        string matrix = "";
        for (int i = 0; i < Calculator.length; i++)
        {
            for (int j = 0; j < Calculator.length; j++)
            {
                matrix += GameMatrix[i, j] + " ";
            }
            matrix += "\n";
        }
        Debug.Log(matrix);
    }
    //EL VOSTRE EXERCICI COMENÇA AQUI
    private void Update()
    {
        if(!EvaluateWin())
        {
            openList.Sort();
            openList.AddRange(CreateWASDNodes(openList[0]));
        }
    }
    private bool EvaluateWin()
    {
        if (closeList[closeList.Count].position == new Vector2(objectivePos[0], objectivePos[1])) return true;
        return false;
    }

    private List<Node> CreateWASDNodes(Node actualNode)
    {
        List<Node> nodes = new List<Node>();
        for(int i = 0; i < 4; i++)
        {
            nodes.Add(CreateNewNode(actualNode, i));
        }
        return nodes;
    }

    private Node CreateNewNode(Node actualNode, int actualDirection)
    {
        int[] nodePosition = { 0, 0 };
        Vector2 position = new Vector2();
        switch (actualDirection)
        {
            case 0:
                position = new Vector2(actualNode.position.x + 1, actualNode.position.y);
                break;
            case 1: 
                position = new Vector2(actualNode.position.x, actualNode.position.y - 1);
                break;
            case 2:
                position = new Vector2(actualNode.position.x - 1, actualNode.position.y);
                break;
            case 3:
                position = new Vector2(actualNode.position.x, actualNode.position.y + 1);
                break;
        }
        
        nodePosition[0] = System.Convert.ToInt32(position[0]);
        nodePosition[1] = System.Convert.ToInt32(position[1]);

        return new Node(Calculator.CheckDistanceToObj(nodePosition, objectivePos), position, actualNode);
    }

}
