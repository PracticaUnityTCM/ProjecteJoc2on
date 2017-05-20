using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmenyIA_Patrol : MonoBehaviour {
    public Transform[] Waypoints;
    public float speed;
    public int curWayPoint;
    public bool doPatrol=true;
    public Vector3 target;
    public Vector3 moveDireccion;
    public Vector3 Velocity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(curWayPoint<Waypoints.Length)
        {
            target = Waypoints[curWayPoint].position;
            moveDireccion = target - transform.position;
            Velocity = GetComponent<Rigidbody>().velocity;
            if(moveDireccion.magnitude<1.0f)
            {
                curWayPoint++;
            }
            else
            {
                Velocity = moveDireccion.normalized;
            }
        }
		else
        {
            if(doPatrol)
            {
                curWayPoint = 0;
            }
            else
            {
                Velocity = Vector3.zero;
            }
        }
        GetComponent<Rigidbody>().velocity = Velocity;
        transform.LookAt(target);
	}
}
