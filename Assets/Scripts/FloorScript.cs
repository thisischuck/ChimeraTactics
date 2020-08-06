using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public GameMaster gm;
    public Vector2 boardPosition;

    MeshRenderer m;

    public Material range;
    public Material normal;
    public Material mouse;

    bool inRange, onMouse;

    void OnEnable()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        m = this.GetComponent<MeshRenderer>();
        inRange = false;
    }

    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        m = this.GetComponent<MeshRenderer>();
        inRange = false;
    }

    void Update()
    {
        if (onMouse)
            m.material = mouse;
        else if (inRange)
            m.material = range;
        else
            m.material = normal;
    }

    public void InRange(bool range)
    {
        inRange = range;
    }

    void OnMouseExit()
    {
        onMouse = false;
    }

    void OnMouseOver()
    {
        onMouse = true;
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
