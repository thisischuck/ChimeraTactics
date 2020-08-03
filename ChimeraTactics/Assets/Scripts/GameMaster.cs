using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject FloorObject;
    public GameObject WarriorObject, EnchanterObject, RangerObject;
    public int BoardSize;
    public int CellSize;
    List<Character> ListCharacters;
    List<Turn> TurnRotation;

    public GameObject FloorParent, CharacterParent;
    public ArrowAnimation Arrow;

    public bool WaitingForTarget;
    int index = 0;

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
        Arrow = GameObject.Find("PointArrow").GetComponent<ArrowAnimation>();
        ListCharacters = new List<Character>();
        TurnRotation = new List<Turn>();
        board = new Board(10);
        PrintBoard();
        TurnCreation();
    }

    void PrintBoard()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                Vector3 pos = new Vector3(
                    i * CellSize,
                    0.2f,
                    j * CellSize
                );
                var a = Instantiate(
                    FloorObject,
                    pos,
                    FloorObject.transform.rotation,
                    FloorParent.transform
                );
                a.GetComponent<FloorScript>().boardPosition = new Vector2(i, j);
                pos = new Vector3(
                    i * CellSize,
                    1,
                    j * CellSize
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
                obj = Instantiate(WarriorObject, pos, transform.rotation, CharacterParent.transform);
                c = new Warrior(obj, Random.Range(4, 7), bPos);
                break;
            case 'r':
                obj = Instantiate(RangerObject, pos, transform.rotation, CharacterParent.transform);
                c = new Ranger(obj, Random.Range(6, 10), bPos);
                break;
            case 'e':
                obj = Instantiate(EnchanterObject, pos, transform.rotation, CharacterParent.transform);
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
        currentTurn = TurnRotation[index];
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

    public void RestartTurns()
    {
        //Goes to all turns and changes the isFinished to false.
        //If the culprit has not died
    }

    Character FindCharacter(GameObject obj)
    {
        foreach (Character c in ListCharacters)
        {
            if (obj = c.Object)
                return c;
        }
        return null;
    }

    public void SendTarget(int btn, GameObject obj)
    {
        switch (btn)
        {
            case 1:
                currentTurn.Target = FindCharacter(obj);
                //The turn is expecting a Character  
                break;
            case 0:
                currentTurn.Position = obj.GetComponent<FloorScript>().boardPosition;
                //The turn is expecting a position
                break;
        }
    }

    void Update()
    {
        Arrow.Target = currentTurn.GiveCulprit.Object;
        if (currentTurn.isFinished)
        {
            Debug.Log("Finished Turn");
            index++;
            if (index > TurnRotation.Count)
                index = 0;
            currentTurn = TurnRotation[index];
        }
        else if (!WaitingForTarget)
        {
            currentTurn.targetAquired = true;
            currentTurn.Update();
        }

        Debug.Log($"Turn isMoving: {currentTurn.isMoving}");
        Debug.Log($"Turn isAttacking: {currentTurn.isAttacking}");
        Debug.Log($"Turn usedSkill: {currentTurn.usedSkill}");
    }
}