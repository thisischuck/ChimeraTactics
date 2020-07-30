using UnityEngine;

class Turn
{
    private Character culprit;

    void Attack(Character culprit, Character target, Skill skill)
    {
        culprit.HasAttacked = true;
        if (target.Object)
            target.Health -= skill.Damage;
        else
            Debug.Log("I missed");
    }

    void Move()
    {

    }

}