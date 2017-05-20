using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnCamera : MonoBehaviour {
    public GameObject Camera;
    public RectTransform TransformR;
	// Use this for initialization
	void Start () {
        Camera = Camera.GetComponent<Camera>().gameObject;
        TransformR = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        //Quaternion camrot = Camera.transform.rotation;
        //this.transform.rotation = camrot;
      // TransformR.transform.position= transform.parent.transform.position;
      transform.LookAt(transform.position + Camera.transform.rotation * Vector3.back, Camera.transform.rotation * Vector3.up);
       // this.transform.Rotate(0, 180, 0);
    }
}
