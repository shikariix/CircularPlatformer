using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public int platformAmount;
    public GameObject platformPrefab;
    private float planetSurface;
    private List<GameObject> obstacles;
    private float angle;
    private float previousY;

    // Use this for initialization
    void Start () {
        obstacles = new List<GameObject>();
        planetSurface = (transform.localScale.x / 2) + 1;

        Vector3 center = transform.position;
        previousY = planetSurface;
        do {
            //prepare basic object
            Vector3 pos = RandomCircle(center, previousY + (int)Random.Range(-3, 4));
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, center-pos);
            GameObject temp = Instantiate(platformPrefab, pos, rot);

            //add some individuality
            temp.transform.position -= temp.transform.up * Random.Range(0, 5);
            SetScale(temp);
            angle += temp.transform.localScale.x;

            //now that everything is done, put it where it belongs!
            temp.transform.parent = transform;
            obstacles.Add(temp);
        } while (angle < 360);
    }

    void SetScale(GameObject temp) {
        //Set different scales for objects for more variety in platforms
        Vector3 scale = temp.transform.localScale;
        int scaleX = Random.Range(1, 4);
        int scaleY = Random.Range(1, scaleX);
        temp.transform.localScale = new Vector3(scale.x * scaleX, scale.y * scaleY, scale.z);
    }

    Vector3 RandomCircle(Vector3 center, float radius) {
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.z = center.z;

        //if position is same as previously created object, generate a new position
        foreach (GameObject obj in obstacles) {
            if (obj.transform.position == pos) {
                pos = RandomCircle(center, radius);
            }
        }

        return pos;
    }
}
