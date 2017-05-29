using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContrShipProves : MonoBehaviour {
    public float speed;
    public float speed2;
    public Rigidbody RB;
	// Use this for initialization
	void Start () {
        RB = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            speed = speed + (1.2f * Time.deltaTime);
        }
        else
        {
            if(speed>0)
            speed = speed - (1.2f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            speed = speed + (1.2f * Time.deltaTime);
        }
        else
        {
            if (speed > 0)
                speed = speed - (1.2f * Time.deltaTime);
        }
        RB.angularVelocity = new Vector3(0, Input.GetAxis("Horizontal"));
        RB.velocity = transform.forward * (speed+(-speed2));
            }
}
