using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPath : MonoBehaviour {

    // Use this for initialization
    EditorPath PathToFollow;
    public int CurrentWayPointId = 0;
    public float speed;
    private float ReachDistance;
    public float rotationSpeed=500f;
    public string pathName;
    public int num;
    Vector3 lastPosition;
    Vector3 currentPosition;
	void Start () {
        PathToFollow = transform.parent.GetComponentInChildren<EditorPath>();
        lastPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        EnemyController ene = GetComponent<EnemyController>();
        if (ene.EnemyShipBehaibour==EnemyShipBehaivour.Patroling)
        {
            float diastance = Vector3.Distance(PathToFollow.pathObjs[CurrentWayPointId].position, transform.position);
            transform.position = Vector3.MoveTowards(transform.position, PathToFollow.pathObjs[CurrentWayPointId].position, Time.deltaTime * speed);

            Vector3 targetPoint = new Vector3(PathToFollow.pathObjs[CurrentWayPointId].position.x, transform.position.y, PathToFollow.pathObjs[CurrentWayPointId].position.z) - transform.position;

            Quaternion rotation = Quaternion.LookRotation(PathToFollow.pathObjs[CurrentWayPointId].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            if (diastance <= ReachDistance)
            {
                CurrentWayPointId++;
            }
            if (CurrentWayPointId >= PathToFollow.pathObjs.Count)
            {
               
                CurrentWayPointId = 0;
            }
        }
	}
}
