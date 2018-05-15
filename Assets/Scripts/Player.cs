using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //player position
    private float gravity = 0f;
    private Vector3 newPos;
    private Vector3 horizontalMovement;
    [SerializeField]
    private float speed;
    private float acceleration = 0f;
    [SerializeField]
    private float maxAcceleration;

    private bool movingRight;
    private bool canWallJump = false;

    //player rotation
    private Vector3 startDir;
    private Vector3 newDir;
    private float angle;

    private bool grounded = false;

    void Start () {
        // Get original angle
        Vector3 p1 = Vector3.zero;
        Vector3 p2 = transform.position;
        startDir = p1 - p2;
    }
	
	void Update () {
        //move towards world center if not touching surface
        if (!grounded) {
            if (gravity < 1f) {
                gravity += Time.deltaTime;
            }
            newPos = Vector3.MoveTowards(Vector3.zero, transform.position, gravity);
        }
        else {
            newPos = Vector3.zero;
        }

        
        //movement input controls
        if (Input.GetKey(KeyCode.D)) {
            horizontalMovement = -(transform.right * (speed + acceleration) * Time.deltaTime);
            Accelerate();
        }
        else if (Input.GetKey(KeyCode.A)) {
            horizontalMovement = transform.right * (speed + acceleration) * Time.deltaTime;
            Accelerate();
        } 
        else {
            if (acceleration > 0f) {
                acceleration -= 0.6f;
               
            } else {
                acceleration = 0;
            }
            horizontalMovement = Vector3.Lerp(Vector3.zero, horizontalMovement, acceleration);
        }

        newPos += horizontalMovement;
        Debug.Log(canWallJump);
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            transform.position += transform.up;
            gravity = -0.5f;
            grounded = false;
        } /*else if (Input.GetKeyDown(KeyCode.Space) && canWallJump) {
            transform.position += transform.up;
            acceleration = -12f;
            grounded = false;
            canWallJump = false;
        }*/

        transform.position -= newPos;
    }

    private void Accelerate() {
        if (acceleration < maxAcceleration) {
            acceleration += 0.4f;
        }
        else {
            acceleration = maxAcceleration;
        }
    }

    void OnCollisionStay(Collision col) {
        grounded = true;
        gravity = 0;

        if (col.gameObject.tag == "Platform") {
            //align bottom with last hit object up
            angle = AbsoluteAngleBetween(transform.up, -col.gameObject.transform.up);
            transform.Rotate(0, 0, angle * Time.deltaTime * 10);
            if (transform.up != col.contacts[0].normal) {
                grounded = false;
                gravity += Time.deltaTime;
                canWallJump = true;
            }
        }
        else {
            //rotate bottom towards world center
            transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.position);
        }
    }

    void OnCollisionExit(Collision col) {
        grounded = false;
        canWallJump = false;
    }

    public static float AbsoluteAngleBetween(Vector3 a, Vector3 b) {
        a.Normalize();
        b.Normalize();
        Vector3 aP = RotateZ(a, 90);

        return (Mathf.Atan2(Vector3.Dot(aP, b), Vector3.Dot(a, b)) / Mathf.PI) * 180;
    }

    public static Vector3 RotateZ(Vector3 toRotate, float degrees) {
        degrees = Mathf.PI * degrees / 180;
        float xnew = Mathf.Cos(degrees)*toRotate.x - Mathf.Sin(degrees)*toRotate.y;
        float ynew = Mathf.Sin(degrees)*toRotate.x + Mathf.Cos(degrees)*toRotate.y;

        toRotate.x = xnew;
        toRotate.y = ynew;

        return toRotate;
    }
}
