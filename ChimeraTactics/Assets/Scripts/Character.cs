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

    public Character(int initiative, int maxHealth, Vector2 pos, GameObject obj)
    {
        Initiative = initiative;
        Object = obj;
        Position = pos;
        MaxHealth = 20;
        Health = MaxHealth;
    }

    private bool hasAttacked, hasMoved;

    public virtual void CheckMe()
    {
        Debug.Log("I'm a Character");
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
}
