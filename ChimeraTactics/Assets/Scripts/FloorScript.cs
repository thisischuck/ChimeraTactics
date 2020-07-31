using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public GameMaster gm;
    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        //send my position to the game master. 
        //if he wants to
        //so that he can send into the Turn
        if (gm.WaitingForTarget)
        {
            gm.Target = this.gameObject;
            gm.WaitingForTarget = false;
        }
    }
}
