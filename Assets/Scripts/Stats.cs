using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public Text level1, level2, level3, enemiesKilled;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        int time1 = PlayerPrefs.GetInt("TimeLevel1");
        int time2 = PlayerPrefs.GetInt("TimeLevel2");
        int time3 = PlayerPrefs.GetInt("TimeLevel3");


        level1.text = "                " + Mathf.Round(time1 / 60) + " MIN " + Mathf.Round(time1 % 60) + " SEC";
        level2.text = "                " + Mathf.Round(time2 / 60) + " MIN " + Mathf.Round(time2 % 60) + " SEC";
        level3.text = "                " + Mathf.Round(time3 / 60) + " MIN " + Mathf.Round(time3 % 60) + " SEC";
        enemiesKilled.text = "                                  " + PlayerPrefs.GetInt("EnemiesKilled");
    }
}
