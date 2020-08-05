using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private int teamNumber;
    private int health;
    private Vector2 position;
    private int initiative;
    private GameObject obj;
    public Skill attack;
    public Skill skill;
    private int maxHealth;
    private int moveRange;

    public Character(int initiative, int maxHealth, int mvRange, Vector2 pos, GameObject obj)
    {
        Initiative = initiative;
        Object = obj;
        Position = pos;
        MaxHealth = 20;
        Health = MaxHealth;
        moveRange = mvRange;
    }

    private bool hasAttacked, hasMoved;

    public virtual char CheckMe()
    {
        Debug.Log("I'm a Character");
        return 'c';
    }

    public virtual void UseSkill(Character cTarget, Vector2 target, Board b)
    {
        Debug.Log("I do nothing");
    }

    #region Public Access
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int MoveRange
    {
        get { return moveRange; }
        set { moveRange = value; }
    }


    public GameObject Object
    {
        get { return obj; }
        set { obj = value; }
    }


    public int TeamNumber
    {
        get { return teamNumber; }
        set { teamNumber = value; }
    }

    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }

    public int Initiative
    {
        get { return initiative; }
        set { initiative = value; }
    }

    public bool HasAttacked
    {
        get { return hasAttacked; }
        set { hasAttacked = value; }
    }

    public bool HasMoved
    {
        get { return hasMoved; }
        set { hasMoved = value; }
    }
    #endregion

    public bool HasDied()
    {
        if (health <= 0)
            return true;
        return false;
    }

    public void ChangeHealth(int amount, int mul)
    {
        health += amount * mul;
        obj.GetComponent<CharacterObject>().RemoveHealth(MaxHealth, Health);
    }

    public static int CompareByInitiative(Character a, Character b)
    {
        if (a.Initiative > b.Initiative)
            return 1;
        else if (a.Initiative < b.Initiative)
            return -1;
        return 0;
    }

    public int Move(Vector2 posTo, Board b)
    {
        float dist = DistanceTo(posTo);
        if (dist <= moveRange)
        {
            int x = (int)Position.x;
            int y = (int)Position.y;
            Cell c = b.BoardCells[x, y];
            c.IsOcuppied = false;
            c.C = 'g';

            x = (int)posTo.x;
            y = (int)posTo.y;
            c = b.BoardCells[x, y];
            c.IsOcuppied = true;
            c.C = CheckMe();

            Position = posTo;

            var obj = Object.GetComponent<CharacterObject>();
            obj.newPosition = c.charPosition;
            obj.isMoving = true;
            return 0;
        }
        return -1;
    }

    public void Attack(Character t)
    {
        //Maybe put chance to miss
        HasAttacked = true;
        if (t.Object && AttackInRange(t.position, attack))
            t.ChangeHealth(attack.Damage, -1);
        else
            Debug.Log("I missed");
    }

    public float DistanceTo(Vector2 a)
    {
        float x = Mathf.Abs(position.x - a.x);
        float y = Mathf.Abs(position.y - a.y);
        return Mathf.Max(x, y);
    }

    public Vector2 DirectionTo(Vector2 a)
    {
        Vector2 v = a - position;
        Debug.Log(v);
        if (v.x != 0)
            v.x = v.x / Mathf.Abs(v.x);
        if (v.y != 0)
            v.y = v.y / Mathf.Abs(v.y);
        return v;
    }


    public bool AttackInRange(Vector2 a, Skill s)
    {
        float dist = DistanceTo(a);
        //Debug.Log(dist);
        if (dist > s.Range)
            return false;
        return true;
    }

}
