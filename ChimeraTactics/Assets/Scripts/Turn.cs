using UnityEngine;

public class Turn
{
    private Board gameBoard;
    public Character culprit;
    private Character target;
    private Vector2 targetPosition;

    public Character Target
    {
        get { return target; }
        set { target = value; }
    }

    public Vector2 Position
    {
        get { return targetPosition; }
        set { targetPosition = value; }
    }

    public Character GiveCulprit
    {
        get { return culprit; }
    }

    public bool isAttacking, isMoving, usedSkill, targetAquired, isFinished;
    private bool hasAttacked, hasMoved;

    public Turn(Board gm, Character c)
    {
        culprit = c;
        gameBoard = gm;
        isFinished = false;
    }

    public void ResetTurn()
    {
        isFinished = false;
        isAttacking = false;
        isMoving = false;
        usedSkill = false;
        targetAquired = false;
        hasAttacked = false;
        hasMoved = false;
    }

    public void Update()
    {
        if (culprit.HasDied())
        {
            isFinished = true;
        }
        else if (hasAttacked && hasMoved)
        {
            isFinished = true;
        }

        if (targetAquired)
        {
            if (!hasAttacked)
            {
                if (isAttacking)
                {
                    culprit.Attack(target);
                    hasAttacked = true;
                    isAttacking = false;
                    targetAquired = false;
                }
                else if (usedSkill)
                {
                    //can't use target.position here. cause some targets can be null
                    //some targets can just be a empty board piece
                    culprit.UseSkill(target, targetPosition, gameBoard);
                    hasAttacked = true;
                    usedSkill = false;
                    targetAquired = false;
                }
            }

            if (isMoving && !hasMoved)
            {
                var a = culprit.Move(targetPosition, gameBoard);
                if (0 == a)
                    hasMoved = true;
            }
        }
    }
}