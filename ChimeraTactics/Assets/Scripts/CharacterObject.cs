using UnityEngine;
using UnityEngine.UI;

//For Control of the Character Visuals
class CharacterObject : MonoBehaviour
{
    public bool isMoving;
    public Vector3 newPosition;
    public Vector2 boardPosition;

    public GameMaster gm;
    public Image image;
    public Material enemyMaterial;

    int teamNumber;

    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();

        teamNumber = gm.FindCharacter(this.gameObject).TeamNumber;

        if (teamNumber == 2)
        {
            GetComponent<MeshRenderer>().material = enemyMaterial;
        }
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
        transform.position = Vector3.Lerp(transform.position, newPosition, 0.01f);

        var dist = Vector3.Distance(transform.position, newPosition);
        if (0.1f >= dist)
            isMoving = false;
    }

    public void RemoveHealth(int maxHealth, int currentHealth)
    {
        float tmp = (float)currentHealth / (float)maxHealth;
        image.fillAmount = tmp;
    }

    void OnMouseDown()
    {
        if (gm.WaitingForTargetAttack)
        {
            Debug.Log("CharacterObject");
            gm.SendTarget(1, this.gameObject);
        }
    }
}