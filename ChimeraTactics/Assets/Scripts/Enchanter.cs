using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Enchanter : Character
{
    private Skill healCurse;
    private Skill attack;

    public Enchanter(GameObject obj, int initiative, Vector2 pos)
    {
        healCurse = new Skill("Heal/Curse", 1, 10, 5);
        attack = new Skill("Basic Attack", 0, 1, 2);
        Object = obj;
        Initiative = initiative;
        Position = pos;
    }

    public override void CheckMe()
    {
        Debug.Log("I'm an Enchanter");
    }

    public override void UseSkill(Vector2 target)
    {
        Debug.Log("I do smth");
    }
}