using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
	private float dist;
	private Ray ray;
    private float step;
    private Vector3 oldPosition;

	// Use this for initialization
	void Start () {
        oldPosition = transform.position;
        step = 2f;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (Input.mousePosition != ray.origin) {
            oldPosition = transform.position;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
		transform.position = Vector3.Lerp(oldPosition, (oldPosition + ray.origin) / 6 + new Vector3 (0,0,-10), step * Time.deltaTime);
	}
}
