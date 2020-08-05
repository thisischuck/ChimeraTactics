using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Ranger : Character
{
    public Ranger(GameObject obj, int initiative, Vector2 pos, int maxHealth, int moveRange)
    : base(initiative, maxHealth, moveRange, pos, obj)
    {
        skill = new Skill("Teleport", 2, 10, 0);
        attack = new Skill("Basic Attack", 0, 7, 3);
    }

    public override char CheckMe()
    {
        Debug.Log("I'm a Ranger");
        return 'r';
    }

    public override void UseSkill(Character cTarget, Vector2 target, Board b)
    {
        //-Teleport somewhere
        //Debug.Log("I do smth");
    }
}