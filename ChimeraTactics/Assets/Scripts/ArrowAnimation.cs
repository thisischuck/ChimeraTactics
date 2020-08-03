using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnimation : MonoBehaviour
{
    public GameObject Target;

    public float Speed;
    public float MaxHeigh;

    private float currentHeight;
    bool isUp;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentHeight = MaxHeigh + 0.5f * Mathf.Sin(Time.time * Speed);
        /* if (isUp)
        {
            if (MaxHeigh - currentHeight < 0.1f)
                isUp = false;
            currentHeight = Mathf.Lerp(currentHeight, MaxHeigh, 0.01f);
        }
        else
        {
            if (currentHeight - Minheight < 0.1f)
                isUp = true;
            currentHeight = Mathf.Lerp(currentHeight, Minheight, 0.01f);
        } */
        if (Target)
            this.transform.position = Target.transform.position + new Vector3(0, currentHeight, 0);
    }
}
