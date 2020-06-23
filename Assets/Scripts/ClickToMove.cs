using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickToMove : MonoBehaviour
{
    public bool isTurn;

    Tank selectedTank;
    Node noTankNode;
    public bool canShoot = false;

    private void Update()
    {
        if (!isTurn)
            return;

        if (Input.GetMouseButtonDown(0) && !canShoot)
        {
            Node node = FindClickedNode(Input.mousePosition);

            if (node != null)
            {
                Tank returnedTank = GetTankOverANode(node);

                if (returnedTank != null)
                {
                    selectedTank = returnedTank;
                    Debug.Log("tank selected");
                }
                else if (selectedTank)
                {
                    noTankNode = node;
                    Debug.Log("node selected");
                }

                if (selectedTank && noTankNode)
                {
                    StartCoroutine(selectedTank.MoveToNode(noTankNode));
                    selectedTank = null;
                    noTankNode = null;
                    //turn end
                }
            }
        }
        else if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Node node = FindClickedNode(Input.mousePosition);
            if(node != null)
            {
                Tank returnedTank = GetTankOverANode(node);

                if (returnedTank)
                {
                    returnedTank.GetComponent<Tank>().Shoot();
                    canShoot = false;
                    isTurn = false;
                }
            }
        }
    }

    public void ToggleTankShooting()
    {
        if (!canShoot)
            canShoot = true;
        else
            canShoot = false;
    }

    Tank GetTankOverANode(Node node)
    {
        TankGenerator tankGenerator = transform.parent.GetComponentInChildren<TankGenerator>();
        if (tankGenerator)
        {
            foreach (var tank in tankGenerator.allTanks)
            {
                if (tank.currentNode == node)   return tank;
            }
        }

        return null;
    }

    public Node FindClickedNode(Vector3 position)
    {
        Board board = transform.parent.GetComponentInChildren<Board>();

        if (board.nodes.Length < 1)
            return null;

        //TODO: change the camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.collider.transform.GetComponent<Node>())
            {
                Node node = hit.collider.transform.GetComponent<Node>();
                foreach (var n in board.nodes)
                {
                    if (n == node)
                    {
                        return node;
                    }
                }
            }
        }
        return null;
    }
}
