using UnityEngine;

//For Control of the Character Visuals
class CharacterObject : MonoBehaviour
{
    public bool isMoving;
    public Vector3 newPosition;

    public GameMaster gm;

    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    void Update()
    {
        if (isMoving)
        {
            Move(newPosition);
        }
    }

    public void Move(Vector3 newPosition)
    {
        transform.position = Vector3.Lerp(transform.position, newPosition, 1);

        var dist = Vector3.Distance(transform.position, newPosition);
        if (dist < 0.5f)
            isMoving = false;
    }

    void OnMouseDown()
    {
        if (gm.WaitingForTarget)
        {
            gm.Target = this.gameObject;
            gm.WaitingForTarget = false;
        }
    }
}