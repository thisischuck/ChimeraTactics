using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Warrior : Character
{
    public Warrior(GameObject obj, int initiative, Vector2 pos, int maxHealth, int moveRange)
    : base(initiative, maxHealth, moveRange, pos, obj)
    {
        skill = new Skill("Dash", 3, 5, 3);
        attack = new Skill("Basic Attack", 0, 1, 5);
    }

    public override char CheckMe()
    {
        Debug.Log("I'm a Warrior");
        return 'w';
    }

    public override void UseSkill(Character cTarget, Vector2 target, Board b)
    {
        //-Dash in the Direction of the target. Dealing dmg to everyone in the path
        //-Deals damage in a line facing the target
        Debug.Log("I do smth");
    }
}