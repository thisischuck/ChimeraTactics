using UnityEngine;

public class Turn
{
    private Board gameBoard;
    private Character culprit;
    private Character target;

    public bool isAttacking, isMoving, usedSkill, targetAquired;

    public Turn(Board gm, Character c)
    {
        culprit = c;
        gameBoard = gm;
    }

    public void Update()
    {
        if (isAttacking && targetAquired)
        {
            Attack(target, culprit.attack);
        }

        if (usedSkill && targetAquired)
        {
            //can't use target.position here. cause some targets can be null
            //some targets can just be a empty board piece
            UseSkill(target, target.Position);
        }

        if (isMoving)
        {
            Move(culprit, target.Position);
        }
    }

    public void Attack(Character t, Skill skill)
    {
        culprit.HasAttacked = true;
        if (t.Object)
            t.Health -= skill.Damage;
        else
            Debug.Log("I missed");
    }

    void UseSkill(Character ctarget, Vector2 target)
    {
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
        obj.isMoving = true;
    }

}