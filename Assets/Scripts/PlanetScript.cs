using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetScript : MonoBehaviour {

	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

    [SerializeField]
    private string planetName;

    void OnMouseEnter () {
		//Change cursor to hand
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	}
	void OnMouseExit () {
		Cursor.SetCursor (null, hotSpot, cursorMode);
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			//on click, checks what planet is hit and will change the scene to that planet
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, new Vector2(0,0), 5);

			if (hit) {
				if (hit.collider != null) {
					Debug.Log (hit);
					SceneManager.LoadScene (planetName);
				}
			}
		}
	}
}
