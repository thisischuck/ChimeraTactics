using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 StartPosition;
    public Quaternion StartRotation;
    public Quaternion EndRotation;
    public Vector3 EndPosition;
    public Vector3 distance;

    bool started = false;
    bool gameStarted = false;
    bool startedCourotine;
    bool canMove = false;

    GameObject target;



    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (canMove)
            {
                Vector3 pos = target.transform.position + distance;
                transform.position = Vector3.Lerp(transform.position, pos, 0.1f);
                transform.rotation = Quaternion.Lerp(transform.rotation, StartRotation, 0.1f);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, StartPosition, 0.1f);
                transform.rotation = Quaternion.Lerp(transform.rotation, StartRotation, 0.1f);
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, EndPosition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, EndRotation, 0.1f);
        }
    }

    public void GetTarget(GameObject target)
    {
        this.target = target;
    }

    public void StartIt()
    {
        started = !started;
    }

    IEnumerator WaitToMove()
    {
        startedCourotine = true;
        yield return new WaitForSeconds(1f);
        startedCourotine = false;
        canMove = true;
    }
    public void GameStart()
    {
        gameStarted = !gameStarted;
        switch (gameStarted)
        {
            case true:
                if (!startedCourotine)
                    StartCoroutine("WaitToMove");
                break;
            case false:
                canMove = false;
                break;

        }
    }
}