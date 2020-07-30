using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Ranger : Character
{
    private Skill teleport;
    private Skill attack;

    public Ranger()
    {
        teleport = new Skill("Teleport", 2, 10, 0);
        attack = new Skill("Basic Attack", 0, 7, 3);
    }

    public override void CheckMe()
    {
        Debug.Log("I'm a Ranger");
    }

    public override void UseSkill(Vector2 target)
    {
        Debug.Log("I do smth");
    }
}