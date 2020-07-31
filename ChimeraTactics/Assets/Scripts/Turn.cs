using UnityEngine;

public class Turn
{
    private Board gameBoard;
    private Character culprit;

    public Turn(Board gm, Character c)
    {
        culprit = c;
        gameBoard = gm;
    }

    void Attack(Character target, Skill skill)
    {
        culprit.HasAttacked = true;
        if (target.Object)
            target.Health -= skill.Damage;
        else
            Debug.Log("I missed");
    }

    void UseSkill(Vector2 target)
    {
        culprit.UseSkill(target);
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
        obj.Move(c.charPosition);
    }

}