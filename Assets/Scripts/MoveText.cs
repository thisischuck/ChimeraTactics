using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveText : MonoBehaviour
{
    // Start is called before the first frame update
    bool started;
    bool fadeIn;
    TextMeshPro text;
    RectTransform t;

    public Vector3 startPosition;
    public Vector3 endPosition;
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        t = GetComponent<RectTransform>();
    }

    public void StartIt()
    {
        fadeIn = true;
        if (!started)
            StartCoroutine("Wait");
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            t.localPosition = Vector3.Lerp(t.localPosition, endPosition, 0.1f);
            text.alpha = Mathf.Lerp(text.alpha, 255, 0.1f);
        }
        else
        {
            t.localPosition = Vector3.Lerp(t.localPosition, startPosition, 0.1f);
            text.alpha = Mathf.Lerp(text.alpha, 0, 0.1f);
        }
    }

    IEnumerator Wait()
    {
        started = true;
        yield return new WaitForSeconds(1.5f);
        fadeIn = false;
        started = false;
    }
}
