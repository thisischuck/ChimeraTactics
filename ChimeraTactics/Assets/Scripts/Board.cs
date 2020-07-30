using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public char C;
    public bool IsOcuppied;

    public Cell()
    {
        this.C = 'g';
        IsOcuppied = false;
    }
}

public class Board : MonoBehaviour
{
    public GameObject Parent;
    public int BoardSize = 1;
    public int CellSize = 10;
    public Cell[,] BoardCells;

    void Start()
    {
        BoardCells = new Cell[BoardSize, BoardSize];
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                BoardCells[i, j] = new Cell();

                if (i == j)
                {
                    BoardCells[i, j].C = 'w';
                }

            }
        }

        PrintBoard();
    }

    void PrintBoard()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                Vector3 pos = new Vector3(
                    i * CellSize,
                    0,
                    j * CellSize
                );
                Instantiate(Parent, pos, Parent.transform.rotation, this.transform);
            }
        }
    }
}
