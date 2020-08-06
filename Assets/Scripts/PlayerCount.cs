using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerCount : MonoBehaviour
{
    int playerCount = 2;
    int enemyCount = 4;

    public int maxPlayerCount;
    public int maxEnemyCount;

    public TMP_Text text;

    public void Add(bool isPlayer)
    {
        if (isPlayer && playerCount < maxPlayerCount)
            playerCount++;

        if (!isPlayer && enemyCount < maxEnemyCount)
            enemyCount++;
    }

    public void SendToMaster(GameMaster g)
    {
        g.PlayerCount = playerCount;
        g.EnemyCount = enemyCount;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"{playerCount}/{enemyCount}";
    }
}
