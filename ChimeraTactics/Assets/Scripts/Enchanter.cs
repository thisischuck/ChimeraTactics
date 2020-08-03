using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Enchanter : Character
{
    public Enchanter(GameObject obj, int initiative, Vector2 pos)
    {
        skill = new Skill("Heal/Curse", 1, 10, 5);
        attack = new Skill("Basic Attack", 0, 1, 2);
        Object = obj;
        Initiative = initiative;
        Position = pos;
    }

    public override void CheckMe()
    {
        Debug.Log("I'm an Enchanter");
    }

    public override void UseSkill(Character cTarget, Vector2 target, Board b)
    {
        Debug.Log("I do smth");
    }
}