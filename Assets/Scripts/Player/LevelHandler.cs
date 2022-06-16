using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    public static int enemyCount;
    public static int maxEnemyCount;

    void Update()
    {
        if (enemyCount == maxEnemyCount)
            MainMenu();
    }

    public static void ResetLevel()
	{
        PlayerPrefs.SetInt("EnemiesKilled", PlayerPrefs.GetInt("EnemiesKilled") + 1);
        enemyCount = 0;
        maxEnemyCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void MainMenu()
	{
        SetBestTime();
        PlayerPrefs.SetInt("EnemiesKilled", PlayerPrefs.GetInt("EnemiesKilled") + 1);
        SceneManager.LoadScene("MainMenu");
	}

    private static void SetBestTime()
	{
        string currentLevel = SceneManager.GetActiveScene().name;

        if (PlayerPrefs.GetInt("Time"+ currentLevel) > (int)Time.timeSinceLevelLoad || PlayerPrefs.GetInt("Time"+ currentLevel) == 0)
		{
            PlayerPrefs.SetInt("Time"+ currentLevel, (int)Time.timeSinceLevelLoad);

        }
    }
}
