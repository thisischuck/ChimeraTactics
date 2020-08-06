using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
public class GameMaster : MonoBehaviour
{
    public GameObject FloorObject;
    public GameObject CharacterObject;
    public int BoardSize;
    public int CellSize;
    public List<Character> ListCharacters;
    List<Turn> TurnRotation;
    int[] characterPerTeam;

    public int PlayerCount, EnemyCount;

    StartMovement camera;

    public GameObject FloorParent, CharacterParent;
    public ArrowAnimation Arrow;

    public bool WaitingForTargetAttack, WaitingForTargetMove, isPlayer;
    int index = 0;

    public GameObject MidGameUI, StartGameUI, PauseGameUI;

    Turn currentTurn;
    AI aI;
    bool isOver, startedCourotine, isPaused;
    Board board;

    IEnumerator WaitToStart()
    {
        startedCourotine = true;
        yield return new WaitForSeconds(0.5f);
        startedCourotine = false;
        Start();
    }

    void OnEnable()
    {
        NewGame();
        if (!startedCourotine && isOver)
            StartCoroutine("WaitToStart");

    }

    void NewGame()
    {
        if (ListCharacters != null)
        {
            foreach (Character c in ListCharacters)
            {
                Destroy(c.Object);
            }

            ListCharacters.RemoveAll(c => c.CheckMe() != 'g');
        }
        if (TurnRotation != null)
            TurnRotation.RemoveAll(t => t.Equals(t));
        board = null;
        characterPerTeam = new int[] { 0, 0 };
        WaitingForTargetAttack = false;
        WaitingForTargetMove = false;
    }

    void Start()
    {
        isOver = true;
        Arrow = GameObject.Find("PointArrow").GetComponent<ArrowAnimation>();
        ListCharacters = new List<Character>();
        TurnRotation = new List<Turn>();
        var numPlayers = (int)Random.Range(2, 5);
        var numEnemies = (int)Random.Range(4, 9);
        board = new Board(BoardSize, PlayerCount, EnemyCount);
        characterPerTeam = new int[2];
        PrintBoard();
        FillBoard();
        TurnCreation();
        aI = new AI(board, this);
        aI.ChangeTurn = TurnRotation[index];
        camera = Camera.main.GetComponent<StartMovement>();
        Reset();
        isOver = false;
        camera.GetTarget(currentTurn.culprit.Object);
        camera.GameStart();
    }

    bool IsTeamAlive(int teamNumber)
    {
        int count = 0;
        foreach (Character c in ListCharacters)
        {
            if (!c.HasDied())
            {
                if (c.TeamNumber == teamNumber)
                    count++;
            }
        }
        if (count > 0)
            return true;
        return false;
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

            }
        }
    }

    void FillBoard()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                if (board.BoardCells[i, j].IsOcuppied)
                {
                    if (i == 0)
                    {
                        CreateCharacters(
                            board.BoardCells[i, j].C,
                            board.BoardCells[i, j].charPosition,
                            new Vector2(i, j),
                            1
                        );
                        characterPerTeam[0]++;
                    }

                    if (i == BoardSize - 1)
                    {
                        CreateCharacters(
                            board.BoardCells[i, j].C,
                            board.BoardCells[i, j].charPosition,
                            new Vector2(i, j),
                            2
                        );
                        characterPerTeam[1]++;
                    }
                }

            }
        }
    }

    void CreateCharacters(char id, Vector3 pos, Vector2 bPos, int team)
    {
        GameObject obj = null;
        Character c = null;
        switch (id)
        {
            case 'w':
                obj = Instantiate(CharacterObject, pos, transform.rotation, CharacterParent.transform);
                c = new Warrior(obj, Random.Range(4, 7), bPos, 20, 3);
                c.TeamNumber = team;
                obj.GetComponent<CharacterObject>().Type = id;
                break;
            case 'r':
                obj = Instantiate(CharacterObject, pos, transform.rotation, CharacterParent.transform);
                c = new Ranger(obj, Random.Range(6, 10), bPos, 15, 5);
                c.TeamNumber = team;
                obj.GetComponent<CharacterObject>().Type = id;

                break;
            case 'e':
                obj = Instantiate(CharacterObject, pos, transform.rotation, CharacterParent.transform);
                c = new Enchanter(obj, Random.Range(2, 7), bPos, 10, 2);
                c.TeamNumber = team;
                obj.GetComponent<CharacterObject>().Type = id;
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
        currentTurn = TurnRotation[0];
    }

    public void Range()
    {
        int f = FloorParent.transform.childCount;
        Character c = currentTurn.culprit;
        for (int i = 0; i < f - 1; i++)
        {
            var child = FloorParent.transform.GetChild(i);
            var s = child.GetComponent<FloorScript>();
            if (WaitingForTargetAttack)
            {
                var d = c.DistanceTo(s.boardPosition);
                if (d <= c.attack.Range)
                    s.InRange(true);
                else s.InRange(false);
            }
            else if (WaitingForTargetMove)
            {
                var d = c.DistanceTo(s.boardPosition);
                if (d <= c.MoveRange)
                    s.InRange(true);
                else s.InRange(false);
            }
            else s.InRange(false);

        }
    }

    public void AttackButton()
    {
        if (!isOver)
        {
            currentTurn.isMoving = false;
            WaitingForTargetMove = false;
            currentTurn.usedSkill = false;
            currentTurn.isAttacking = true;
            WaitingForTargetAttack = true;
        }
    }

    public void MoveButton()
    {
        if (!isOver)
        {
            currentTurn.isMoving = true;
            WaitingForTargetMove = true;
            currentTurn.usedSkill = false;
            currentTurn.isAttacking = false;
            WaitingForTargetAttack = false;
        }
    }

    public void SkillButton()
    {
        currentTurn.isAttacking = false;
        currentTurn.usedSkill = true;
        WaitingForTargetAttack = true;
    }

    public void NextTurnButton()
    {
        if (currentTurn.isFinished && !isPlayer)
            NextTurn();
        else if (isPlayer)
        {
            currentTurn.isFinished = true;
            NextTurn();
        }
    }

    public void PauseButton()
    {
        if (isPaused)
        {
            PauseGameUI.SetActive(false);
            MidGameUI.SetActive(true);
        }
        else MidGameUI.SetActive(false);

        isPaused = !isPaused;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    void NextTurn()
    {
        //Need to check if it's the player or the ai
        //To go Next
        //Debug.Log("Finished Turn");
        WaitingForTargetAttack = false;
        WaitingForTargetMove = false;
        Range();
        MidGameUI.transform.Find("NewRound").GetComponent<MoveText>().StartIt();
        index++;
        if (index > TurnRotation.Count - 1)
            index = 0;
        currentTurn = TurnRotation[index];
        currentTurn.ResetTurn();
        aI.ChangeTurn = currentTurn;

        camera.GetTarget(currentTurn.culprit.Object);
    }

    public void Reset()
    {
        currentTurn.isFinished = false;
        WaitingForTargetAttack = false;
        WaitingForTargetMove = false;
        //Goes to all turns and changes the isFinished to false.
        //If the culprit has not died
    }

    public Character FindCharacter(GameObject obj)
    {
        foreach (Character c in ListCharacters)
        {
            if (c.Object == obj)
                return c;
        }
        return null;
    }

    public void SendTarget(int btn, GameObject obj, Vector2 position)
    {
        switch (btn)
        {
            case 1:
                currentTurn.Target = FindCharacter(obj);
                currentTurn.targetAquired = true;
                WaitingForTargetAttack = false;
                //The turn is expecting a Character  
                break;
            case 0:
                currentTurn.Position = position;
                currentTurn.targetAquired = true;
                WaitingForTargetMove = false;
                //The turn is expecting a position
                break;
        }
    }

    int GameOverCheck()
    {
        if (characterPerTeam[0] == 0)
            return 2;
        else if (characterPerTeam[1] == 0)
            return 1;
        return -1;
    }

    public void GameOver(bool forced)
    {
        int g = GameOverCheck();

        if (g != -1 || forced)
        {
            isOver = true;
            if (g == 1)
                Debug.Log("You Win");
            else
                Debug.Log("You Lost");

            this.gameObject.SetActive(false);
            MidGameUI.SetActive(false);
            StartGameUI.SetActive(true);
            PauseGameUI.SetActive(false);
            camera.StartIt();
            camera.GameStart();
            isPaused = false;
        }
    }

    void DeleteCharacters()
    {
        for (int i = ListCharacters.Count - 1; i >= 0; i--)
        {
            Character c = ListCharacters[i];
            if (c.HasDied())
            {
                //TurnRotation[i].Update();
                TurnRotation.RemoveAt(i);
                ListCharacters.RemoveAt(i);
                //c.Object.SetActive(false);
                Destroy(c.Object);
                characterPerTeam[c.TeamNumber - 1]--;
            }
        }
    }

    bool isWaiting()
    {
        return WaitingForTargetAttack || WaitingForTargetMove;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseButton();

        if (!isOver)
        {
            if (!isPaused)
            {
                Arrow.Target = currentTurn.GiveCulprit.Object;
                if (currentTurn.culprit.TeamNumber == 1)
                {
                    isPlayer = true;
                }
                else isPlayer = false;

                if (!isPlayer)
                {
                    if (!currentTurn.isFinished)
                    {
                        aI.Update();
                    }
                }
                else
                {
                    if (!isWaiting())
                    {
                        currentTurn.targetAquired = true;
                        currentTurn.Update();
                    }
                }

                DeleteCharacters();
                GameOver(false);
                Range();
            }
            else
            {
                PauseGameUI.SetActive(true);
            }
        }
        /* Debug.Log($"Turn isMoving: {currentTurn.isMoving}");
        Debug.Log($"Turn isAttacking: {currentTurn.isAttacking}");
        Debug.Log($"Turn usedSkill: {currentTurn.usedSkill}"); */
    }
}