using UnityEngine;

//For Control of the Character Visuals
class CharacterObject : MonoBehaviour
{
    public bool isMoving;
    public Vector3 newPosition;

    void Start()
    {

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
}