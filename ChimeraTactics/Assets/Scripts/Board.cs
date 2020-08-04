using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public char C;
    public bool IsOcuppied;
    public Vector2 position;
    public Vector3 charPosition;

    public Cell(int x, int y)
    {
        this.C = 'g';
        IsOcuppied = false;
        position = new Vector2(x, y);
    }
}

public class Board
{
    private int boardSize, playerCount, enemyCount;
    public Cell[,] BoardCells;

    public Board(int size, int numPlayers, int numEnemies)
    {
        boardSize = size;
        playerCount = numPlayers;
        enemyCount = numEnemies;
        StartBoard();
        RefillBoard();
    }

    void RefillBoard()
    {
        while (playerCount > 0)
        {
            for (int j = 0; j < boardSize; j++)
            {
                int chance = Random.Range(0, 2);
                if (chance == 0)
                    continue;
                int c = Random.Range(1, 4);
                switch (c)
                {
                    case 1:
                        BoardCells[0, j].C = 'w';
                        break;
                    case 2:
                        BoardCells[0, j].C = 'e';
                        break;
                    case 3:
                        BoardCells[0, j].C = 'r';
                        break;

                }
                BoardCells[0, j].IsOcuppied = true;
                playerCount--;
            }
        }

        while (enemyCount > 0)
        {
            for (int j = 0; j < boardSize; j++)
            {
                int chance = Random.Range(0, 2);
                if (chance == 0)
                    continue;
                int c = Random.Range(1, 4);
                switch (c)
                {
                    case 1:
                        BoardCells[boardSize - 1, j].C = 'w';
                        break;
                    case 2:
                        BoardCells[boardSize - 1, j].C = 'e';
                        break;
                    case 3:
                        BoardCells[boardSize - 1, j].C = 'r';
                        break;

                }
                BoardCells[boardSize - 1, j].IsOcuppied = true;
                enemyCount--;
            }
        }


    }

    public void StartBoard()
    {
        BoardCells = new Cell[boardSize, boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                BoardCells[i, j] = new Cell(i, j);

                int chance = Random.Range(0, 2);
                if (chance == 0)
                    continue;
                int c = Random.Range(1, 4);
                if (i == 0 && playerCount > 0)
                {
                    switch (c)
                    {
                        case 1:
                            BoardCells[i, j].C = 'w';
                            break;
                        case 2:
                            BoardCells[i, j].C = 'e';
                            break;
                        case 3:
                            BoardCells[i, j].C = 'r';
                            break;

                    }
                    BoardCells[i, j].IsOcuppied = true;
                    playerCount--;
                }
                else if (i == boardSize - 1 && enemyCount > 0)
                {
                    switch (c)
                    {
                        case 1:
                            BoardCells[i, j].C = 'w';
                            break;
                        case 2:
                            BoardCells[i, j].C = 'e';
                            break;
                        case 3:
                            BoardCells[i, j].C = 'r';
                            break;

                    }
                    BoardCells[i, j].IsOcuppied = true;
                    enemyCount--;
                }
            }
        }
    }
}

