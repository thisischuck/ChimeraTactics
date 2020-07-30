using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    /*This class manages all aspects of the game. 
		Start 
			Creates and Fills the Board.
			Fills a list with all the Characters in the game
			 
		Mid
			Manages Turns
			Checks for win conditions
		End
			Goes back to menu to play again
	*/
    void Start()
    {
    }

    void Attack(Character culprit, Character target, Skill skill)
    {
        culprit.HasAttacked = true;
        if (target)
            target.Health -= skill.Damage;
        else
            Debug.Log("I missed");
    }

    void Move()
    {

    }


    void Update()
    {

    }
}