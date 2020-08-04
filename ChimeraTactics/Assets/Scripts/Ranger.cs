using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Ranger : Character
{
    public Ranger(GameObject obj, int initiative, Vector2 pos, int maxHealth) : base(initiative, maxHealth, pos, obj)
    {
        skill = new Skill("Teleport", 2, 10, 0);
        attack = new Skill("Basic Attack", 0, 7, 3);
    }

    public override void CheckMe()
    {
        Debug.Log("I'm a Ranger");
    }

    public override void UseSkill(Character cTarget, Vector2 target, Board b)
    {
        Debug.Log("I do smth");
    }
}