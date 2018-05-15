using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	void PlayGame () {
		SceneManager.LoadScene ("Galaxy");
	}

	void QuitGame () {
		Application.Quit ();
		Debug.Log ("You cant quit in the editor!");
	}
}
