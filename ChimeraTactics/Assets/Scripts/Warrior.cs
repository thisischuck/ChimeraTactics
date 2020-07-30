using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Warrior : Character
{
    private Skill dash;
    private Skill attack;

    public Warrior()
    {
        dash = new Skill("Dash", 3, 5, 3);
        attack = new Skill("Basic Attack", 0, 1, 5);
    }

    public override void CheckMe()
    {
        Debug.Log("I'm a Warrior");
    }

    public override void UseSkill(Vector2 target)
    {
        Debug.Log("I do smth");
    }
}
