using UnityEngine;

public class Turn
{
    private Board gameBoard;
    private Character culprit;
    private Character target;
    private Vector2 targetPosition;

    public Character GiveTarget
    {
        get { return target; }
        set { target = value; }
    }

    public Vector2 GivePosition
    {
        get { return targetPosition; }
        set { targetPosition = value; }
    }


    public bool isAttacking, isMoving, usedSkill, targetAquired, isFinished;
    private bool hasAttacked, hasMoved;

    public Turn(Board gm, Character c)
    {
        culprit = c;
        gameBoard = gm;
        isFinished = false;
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
            if (isAttacking)
            {
                Attack(target, culprit.attack);
                hasAttacked = true;
            }
            else if (usedSkill)
            {
                //can't use target.position here. cause some targets can be null
                //some targets can just be a empty board piece
                UseSkill(target, targetPosition);
                hasAttacked = true;
            }
            else if (isMoving)
            {
                Move(culprit, targetPosition);
                hasMoved = true;
            }
        }
    }

    void Attack(Character t, Skill skill)
    {
        culprit.HasAttacked = true;
        if (t.Object)
            t.Health -= skill.Damage;
        else
            Debug.Log("I missed");
    }

    void UseSkill(Character ctarget, Vector2 target)
    {
        Debug.Log("I used Skill");
        culprit.UseSkill(ctarget, target);
    }

    //I HATE THIS
    void Move(Character chr, Vector2 posTo)
    {
        int x = (int)chr.Position.x;
        int y = (int)chr.Position.y;
        Cell c = gameBoard.BoardCells[x, y];
        c.IsOcuppied = false;

        x = (int)posTo.x;
        y = (int)posTo.y;
        c = gameBoard.BoardCells[x, y];
        c.IsOcuppied = true;

        chr.Position = posTo;

        var obj = chr.Object.GetComponent<CharacterObject>();
        obj.newPosition = c.charPosition;
        obj.isMoving = true;
    }

}