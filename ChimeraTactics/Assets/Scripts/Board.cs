using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public char C;
    public bool IsOcuppied;
    public Vector2 position;

    public Cell(int x, int y)
    {
        this.C = 'g';
        IsOcuppied = false;
        position = new Vector2(x, y);
    }
}

public class Board
{
    private int boardSize;
    public Cell[,] BoardCells;

    public Board(int size)
    {
        boardSize = size;
        StartBoard();
    }

    public void StartBoard()
    {
        BoardCells = new Cell[boardSize, boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                BoardCells[i, j] = new Cell(i, j);

                if (i == j)
                {
                    BoardCells[i, j].C = 'w';
                }

            }
        }
    }
}
