using UnityEngine;

public class AI
{
    public Character culprit;
    Board gameBoard;
    Turn currentTurn;
    GameMaster gm;

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
        }
    }

    void Attack(Character target)
    {
        currentTurn.usedSkill = false;
        currentTurn.isAttacking = true;

        currentTurn.Target = target;
        currentTurn.targetAquired = true;

        currentTurn.Update();
    }

    public void Move()
    {
        Vector2 target = FindTargetToMove();
        currentTurn.isMoving = true;
        currentTurn.Position = target;
        currentTurn.targetAquired = true;

        currentTurn.Update();
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
        Character target = FindClosest(2 / culprit.TeamNumber);
        float dist = Vector2.Distance(culprit.Position, target.Position);
        if (dist > culprit.attack.Range)
            return false;
        return true;
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
                float d = Vector2.Distance(culprit.Position, c.Position);
                if (d < distance)
                    closest = c;
            }
        }
        return closest;
    }

    Vector2 FindDirectionToClosest(int teamNumber)
    {
        Character t = FindClosest(teamNumber);
        Vector2 v = t.Position - culprit.Position;
        return v.normalized;
    }

    void isInsideTheBoard(int x, int y, int size)
    {
        if (x > size - 1)
            x = size - 1;
        else if (x <= 0)
            x = 0;

        if (y > size - 1)
            y = size - 1;
        else if (y <= 0)
            y = 0;
    }

    Vector2 FindTargetToMove()
    {
        Vector2 dir = FindDirectionToClosest(2 / culprit.TeamNumber);
        int range = culprit.MoveRange;
        Vector2 target = (dir * range) + culprit.Position;
        do
        {
            range--;
            target = (dir * range) + culprit.Position;
            isInsideTheBoard((int)target.x, (int)target.y, gm.BoardSize);

        } while (gameBoard.isOccupied((int)target.x, (int)target.y) || range == 0);

        return target;
    }
}