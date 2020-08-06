using UnityEngine;

public class AI
{
    public Character culprit;
    Board gameBoard;
    Turn currentTurn;
    GameMaster gm;
    bool hasMoved, hasAttacked;

    public AI(Board b, GameMaster gameMaster)
    {
        gameBoard = b;
        gm = gameMaster;
    }

    public Turn ChangeTurn
    {
        set
        {
            currentTurn = value;
            culprit = currentTurn.culprit;
            hasMoved = false;
        }
    }

    public void CheckEnd()
    {
        if (hasAttacked && hasMoved)
        {
            currentTurn.isFinished = true;
            hasMoved = false;
            hasAttacked = false;
        }
    }

    void Attack(Character target)
    {
        currentTurn.usedSkill = false;
        currentTurn.isAttacking = true;

        currentTurn.Target = target;
        currentTurn.targetAquired = true;

        //Debug.Log("I attacked");

        currentTurn.Update();
    }

    public void Move()
    {
        Vector2 target = FindTargetToMove();
        currentTurn.isMoving = true;
        currentTurn.Position = target;
        currentTurn.targetAquired = true;
        currentTurn.Update();
        hasMoved = true;
        //Debug.Log("I moved");
    }

    void UseSkill(Character target)
    {
        currentTurn.usedSkill = false;
        currentTurn.isAttacking = true;

        currentTurn.Target = target;
        currentTurn.targetAquired = true;

        currentTurn.Update();
    }

    public void AttackOrSkill()
    {
        Attack(FindClosest(2 / culprit.TeamNumber));
    }

    public bool CanAttack()
    {
        if (hasAttacked)
            return false;

        hasAttacked = true;
        Character target = FindClosest(2 / culprit.TeamNumber);
        return culprit.AttackInRange(target.Position, culprit.attack);
    }
    //Finds Closest Ally or Enemy
    Character FindClosest(int teamNumber)
    {
        Character closest = null;
        float distance = 20;
        foreach (Character c in gm.ListCharacters)
        {
            if (c.TeamNumber == teamNumber)
            {
                float d = culprit.DistanceTo(c.Position);
                if (d < distance)
                    closest = c;
            }
        }
        return closest;
    }

    Vector3 FindDirectionToClosest(int teamNumber)
    {
        Character t = FindClosest(teamNumber);
        Vector3 v = culprit.DirectionTo(t.Position);
        float f = culprit.DistanceTo(t.Position);
        v.z = f;
        return v;
    }

    Vector2 isInsideTheBoard(int x, int y, int size)
    {
        if (x > size - 1)
            x = size - 1;
        else if (x <= 0)
            x = 0;

        if (y > size - 1)
            y = size - 1;
        else if (y <= 0)
            y = 0;

        return new Vector2(x, y);
    }

    Vector2 FindTargetToMove()
    {
        Vector3 tmp = FindDirectionToClosest(2 / culprit.TeamNumber);
        Vector2 dir = new Vector2(tmp.x, tmp.y);
        int rangeToMove = (int)tmp.z;
        int range = culprit.MoveRange;
        if (range > rangeToMove)
            range = rangeToMove;
        Debug.Log(dir);
        Vector2 target = (dir * range) + culprit.Position;
        do
        {
            target = (dir * range) + culprit.Position;
            target = isInsideTheBoard((int)target.x, (int)target.y, gm.BoardSize);
            range--;
            Debug.Log(target);
        } while (gameBoard.isOccupied((int)target.x, (int)target.y) || range == 0);

        return target;
    }

    void UpdateRanger()
    {
        if (CanAttack())
        {
            AttackOrSkill();
            hasMoved = true;
        }
        else
        {
            Move();
        }
        CheckEnd();
    }

    void UpdateWarrior()
    {
        if (CanAttack())
        {
            AttackOrSkill();
            hasMoved = true;
        }
        else
        {
            Move();
        }
        CheckEnd();
    }

    public void Update()
    {
        char c = culprit.CheckMe();
        switch (c)
        {
            case 'w':
                UpdateWarrior();
                break;
            case 'r':
                UpdateRanger();
                break;
        }
    }
}