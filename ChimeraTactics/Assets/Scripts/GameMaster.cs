using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject FloorObject;
    public GameObject WarriorObject, EnchanterObject, RangerObject;
    public int BoardSize;
    public int CellSize;
    public List<Character> ListCharacters;
    public List<Turn> TurnRotation;

    public bool WaitingForTarget;

    Turn currentTurn;
    public GameObject Target;

    /*This class manages all aspects of the game. 
		Start 
			Creates and Fills the Board.
			Fills a list with all the Characters in the game
			 
		Mid
			Manages Turns
			Checks for win conditions
		End
			Goes back to menu to play again
	*/
    Board board;
    void Start()
    {
        ListCharacters = new List<Character>();
        TurnRotation = new List<Turn>();
        board = new Board(10);
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
                Instantiate(FloorObject, pos, FloorObject.transform.rotation, this.transform);

                pos = new Vector3(
                    i * CellSize / 2,
                    1,
                    j * CellSize / 2
                );
                board.BoardCells[i, j].charPosition = pos;
                if (board.BoardCells[i, j].IsOcuppied)
                    CreateCharacters(board.BoardCells[i, j].C, pos, new Vector2(i, j));
            }
        }
    }

    void CreateCharacters(char id, Vector3 pos, Vector2 bPos)
    {
        GameObject obj;
        Character c = null;
        switch (id)
        {
            case 'w':
                obj = Instantiate(WarriorObject, pos, transform.rotation);
                c = new Warrior(obj, Random.Range(4, 7), bPos);
                break;
            case 'r':
                obj = Instantiate(RangerObject, pos, transform.rotation);
                c = new Ranger(obj, Random.Range(6, 10), bPos);
                break;
            case 'e':
                obj = Instantiate(EnchanterObject, pos, transform.rotation);
                c = new Enchanter(obj, Random.Range(2, 7), bPos);
                break;
        }
        ListCharacters.Add(c);
    }

    void TurnCreation()
    {
        ListCharacters.Sort(Character.CompareByInitiative);
        foreach (Character a in ListCharacters)
        {
            Turn t = new Turn(board, a);
            TurnRotation.Add(t);
        }
    }

    public void AttackButton()
    {
        currentTurn.isAttacking = true;
        WaitingForTarget = true;
    }

    public void MoveButton()
    {
        currentTurn.isMoving = true;
        WaitingForTarget = true;
    }

    public void SkillButton()
    {
        currentTurn.usedSkill = true;
        WaitingForTarget = true;
    }

    void Update()
    {
        if (WaitingForTarget)
        {

        }
        else
        {
            currentTurn.Update();
        }
    }
}