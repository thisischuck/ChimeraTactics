using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int teamNumber;
    private int health;
    private Vector2 position;
    private bool hasSkill;
    private int cooldown;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public bool HasSkill
    {
        get { return hasSkill; }
        set { hasSkill = value; }
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

    public bool HasDied()
    {
        if (health < 0)
            return true;
        return false;
    }
}
