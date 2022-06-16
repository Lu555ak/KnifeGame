using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
	public void Quit()
    {
        PlaySFX();
        Application.Quit();
    }


    public void Level1()
    {
        PlaySFX();
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        PlaySFX();
        SceneManager.LoadScene("Level2");
    }

    public void Level3()
    {
        PlaySFX();
        SceneManager.LoadScene("Level3");
    }

    public void Knife1()
	{
        PlaySFX();
        PlayerPrefs.SetInt("KnifeSelection", 0);
	}

    public void Knife2()
    {
        PlaySFX();
        PlayerPrefs.SetInt("KnifeSelection", 1);
    }

    public void Knife3()
    {
        PlaySFX();
        PlayerPrefs.SetInt("KnifeSelection", 2);
    }

    public void PlaySFX()
	{
        GameObject.Find("ButtonClick").GetComponent<AudioSource>().Play();
    }
}
