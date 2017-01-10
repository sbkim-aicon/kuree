using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InputScript : MonoBehaviour
{
	static int sceneNum = 0;

	// Use this for initialization
	void Awake ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Intput_Manager ();
	}

	void Intput_Manager()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			BackToScene ();
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			NextToScene ();
		}
	}

	public void BackToScene()
	{
		if (sceneNum - 1 >= 0) {
			SceneManager.LoadScene (--sceneNum);
			Debug.Log (sceneNum);
		}
	}

	public void NextToScene()
	{
		if (sceneNum + 1 <= 11) { // 10 = Scecne MaxCount
			SceneManager.LoadScene (++sceneNum);
			Debug.Log (sceneNum);
		}
	}
}
