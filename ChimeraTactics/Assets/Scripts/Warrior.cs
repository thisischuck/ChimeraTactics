using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Warrior : Character
{
    public Warrior(GameObject obj, int initiative, Vector2 pos, int maxHealth) : base(initiative, maxHealth, pos, obj)
    {
        skill = new Skill("Dash", 3, 5, 3);
        attack = new Skill("Basic Attack", 0, 1, 5);
    }

    public override void CheckMe()
    {
        Debug.Log("I'm a Warrior");
    }

    public override void UseSkill(Character cTarget, Vector2 target, Board b)
    {
        Debug.Log("I do smth");
    }
}