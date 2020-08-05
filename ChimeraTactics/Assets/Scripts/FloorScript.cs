using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public GameMaster gm;
    public Vector2 boardPosition;

    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    void OnMouseDown()
    {
        //send my position to the game master. 
        //if he wants to
        //so that he can send into the Turn
        if (gm.WaitingForTargetMove)
        {
            Debug.Log("FloorObject");
            gm.SendTarget(0, this.gameObject, boardPosition);
        }
    }
}
