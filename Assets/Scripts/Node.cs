using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    #region Script's Summary
    //This script is managing single tile
    #endregion

    #region Variable Declaration
    //--------------------Variable Declaration-----------------------------

    public int xIndex = 0;
    public int yIndex = 0;

    //public GameObject tile;
    //public GameObject turnDisplayTile;

    public List<Node> neighbours = new List<Node>();
    public bool isOpen = false;

    Board m_board;

    //----------------------------------------------------------------------
    #endregion

    #region User's Functions
    //----------------------User's Functions--------------------------------

    public void InitNode(int x, int y)
    {
        m_board = GetComponentInParent<Board>();
        xIndex = x;
        yIndex = y;

        //Renderer tileRenderer = tile.GetComponent<Renderer>();

        //tileRenderer.material.color = color;
        GetComponentInChildren<TextMesh>().text = "(" + x + ", " + y + ")";
    }

    public void InitNeighbours()
    {
        if (!m_board)
            return;

        //neighbours.Clear();

        //Vector2 currentPos = new Vector2(xIndex, yIndex);

        //foreach (var position in GetMoves.neighbourPosition)
        //{
        //    Node node = m_board.IsValidNode(position + currentPos);

        //    if (node != null)
        //    {
        //        neighbours.Add(node);
        //    }
        //}
    }

    

    //public void HighlightNode(Color color)
    //{
    //    tile.GetComponent<Renderer>().material.color = color;
    //    isOpen = true;
    //}

    //public void DisplayTurn(bool isTurn, Color color)
    //{
    //    if (isTurn)
    //        turnDisplayTile.GetComponent<Renderer>().material.color = color;
    //    else
    //    {
    //        Renderer tileRenderer = tile.GetComponent<Renderer>();
    //        turnDisplayTile.GetComponent<Renderer>().material.color = tileRenderer.material.color;
    //    }
    //}

    //----------------------------------------------------------------------

    #endregion
}
