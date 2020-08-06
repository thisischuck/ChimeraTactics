using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

//For Control of the Character Visuals
class CharacterObject : MonoBehaviour
{
    public bool isMoving, isAttacking;
    public Vector3 newPosition;
    public Vector2 boardPosition;

    public GameMaster gm;
    public Image image;
    public Material enemyMaterial;

    int teamNumber;
    SpriteRenderer sprite;

    public GameObject bar;
    public GameObject text;

    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();

        teamNumber = gm.FindCharacter(this.gameObject).TeamNumber;

        if (teamNumber == 2)
        {
            sprite = GetComponentInChildren<SpriteRenderer>();
            sprite.material = enemyMaterial;
        }

        bar.SetActive(false);
    }

    bool startedCourotine, isWaiting;

    IEnumerator Wait()
    {
        startedCourotine = true;
        yield return new WaitForSeconds(0.5f);
        startedCourotine = false;
    }

    IEnumerator WaitAttack()
    {
        isWaiting = true;
        yield return new WaitForSeconds(1f);
        isWaiting = false;
        isAttacking = false;
    }

    void Update()
    {
        if (isMoving)
        {
            //Debug.Log("I'm moving");
            Move(newPosition);

            Tooltip("Come Here!");
        }
        else if (isAttacking)
        {
            Tooltip("Take This!");
            if (!isWaiting)
                StartCoroutine("WaitAttack");
        }
        else Tooltip("");
    }

    public void Move(Vector3 newPosition)
    {
        transform.position = Vector3.Lerp(transform.position, newPosition, 0.01f);

        var dist = Vector3.Distance(transform.position, newPosition);
        if (0.1f >= dist)
        {
            transform.position = newPosition;
            isMoving = false;
        }

    }

    void Tooltip(string text)
    {
        if (!startedCourotine)
        {
            this.text.GetComponent<TMP_Text>().text = text;
            StartCoroutine("Wait");
        }
    }

    public void RemoveHealth(int maxHealth, int currentHealth)
    {
        float tmp = (float)currentHealth / (float)maxHealth;
        image.fillAmount = tmp;
    }

    void OnMouseOver()
    {
        bar.SetActive(true);
    }
    void OnMouseExit()
    {
        bar.SetActive(false);
    }

    void OnMouseDown()
    {
        if (gm.WaitingForTargetAttack)
        {
            //Debug.Log("CharacterObject");
            gm.SendTarget(1, this.gameObject, boardPosition);
        }
    }
}