using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
	public void Quit()
    {
        Application.Quit();
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void Knife1()
	{
        PlayerPrefs.SetInt("KnifeSelection", 0);
	}

    public void Knife2()
    {
        PlayerPrefs.SetInt("KnifeSelection", 1);
    }

    public void Knife3()
    {
        PlayerPrefs.SetInt("KnifeSelection", 2);
    }
}
