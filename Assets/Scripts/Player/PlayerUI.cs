using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text time;
    public Text enemy;

    void Update()
    {
        time.text = "Time: " + Mathf.Round(Time.timeSinceLevelLoad).ToString() + "sec";
        enemy.text = "ENEMIES KILLED: " + LevelHandler.enemyCount + "/" + LevelHandler.maxEnemyCount;
    }
}
