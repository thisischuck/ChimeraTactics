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
    public GameObject WarriorParent;
    public int BoardSize = 1;
    public int CellSize = 10;
    public Cell[,] BoardCells;
    private List<GameObject> QuadList;

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
        FillBoard();
    }

    void Update()
    {

    }

    void FillBoard()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                char c = BoardCells[i, j].C;
                if (c == 'w')
                {
                    Instantiate(WarriorParent, new Vector3(i * CellSize, 0, j * CellSize), transform.rotation);
                }
            }
        }
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
