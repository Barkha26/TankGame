using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject tilePrefab;
    public int boardSize;
    
    public Node[,] nodes;

    private void Start()
    {
    }

    public void GenerateBoard()
    {
        nodes = new Node[boardSize, boardSize];

        if (!tilePrefab)
            return;

        for (int y = 0; y < boardSize; y++)
        {
            for (int x = 0; x < boardSize; x++)
            {
                GameObject tileInstance = Instantiate(tilePrefab);
                tileInstance.transform.SetParent(transform);
                tileInstance.transform.localPosition = new Vector3(x, 0, y);
                tileInstance.name = "Node (" + x + "," + y + ")";

                Node node = tileInstance.GetComponent<Node>();
                nodes[y, x] = node;

                if (node)
                {
                    node.InitNode(x, y);
                }
            }
        }
     
        transform.parent.GetComponentInChildren<TankGenerator>().GenerateTanks(this);
    }

    public Node IsValidNode(Vector2Int position)
    {
        foreach (var n in nodes)
        {
            if (n.xIndex == position.x && n.yIndex == position.y)
            {
                return n;
            }
        }
        return null;
    }
}
