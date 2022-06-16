using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
	public Image dimm;
	bool dimmout = true;
	Color lerpedColor;
	float t = 0;

	private void Start()
    {
		StartCoroutine(LodaerStart());
    }

	private void Update()
	{
		if(dimmout == true)
		{
			lerpedColor = Color.Lerp(Color.black, new Color(0,0,0,0), t);
			dimm.color = lerpedColor;
			if (t < 1)
				t += Time.deltaTime / 2f;
		}
		else
		{
			lerpedColor = Color.Lerp(new Color(0, 0, 0, 0), Color.black, Time.deltaTime);
			dimm.color = lerpedColor;
			if (t < 1)
				t += Time.deltaTime / 2f;
		}
	}

	private IEnumerator LodaerStart()
	{
		yield return new WaitForSeconds(3f);
		dimmout = false;
		t = 0;
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene("MainMenu");
	}
}
