using UnityEngine;

//For Control of the Character Visuals
class CharacterObject : MonoBehaviour
{
    public bool isMoving;
    public Vector3 newPosition;
    public Vector2 boardPosition;

    public GameMaster gm;

    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    void Update()
    {
        if (isMoving)
        {
            Debug.Log("I'm moving");
            Move(newPosition);
        }
    }

    public void Move(Vector3 newPosition)
    {
        transform.position = Vector3.Lerp(transform.position, newPosition, 0.1f);

        var dist = Vector3.Distance(transform.position, newPosition);
        if (dist <= 0.1f)
            isMoving = false;
    }

    void OnMouseDown()
    {
        if (gm.WaitingForTarget)
        {
            Debug.Log("CharacterObject");
            gm.WaitingForTarget = false;
            gm.SendTarget(1, this.gameObject);
        }
    }
}