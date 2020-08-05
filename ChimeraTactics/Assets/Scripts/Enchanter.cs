using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Enchanter : Character
{
    public Enchanter(GameObject obj, int initiative, Vector2 pos, int maxHealth, int moveRange)
    : base(initiative, maxHealth, moveRange, pos, obj)
    {
        skill = new Skill("Heal/Curse", 1, 10, 5);
        attack = new Skill("Basic Attack", 0, 1, 2);
    }

    public override char CheckMe()
    {
        Debug.Log("I'm an Enchanter");
        return 'e';
    }

    public override void UseSkill(Character cTarget, Vector2 target, Board b)
    {
        //-Heal or Deal Damage depending if the character is an ally or not
        Debug.Log("I do smth");
    }
}