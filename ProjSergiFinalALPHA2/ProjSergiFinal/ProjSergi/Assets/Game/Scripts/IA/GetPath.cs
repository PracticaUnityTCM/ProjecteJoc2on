using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPath : MonoBehaviour {
    public GameObject[] allPath;
	// Use this for initialization
	void Start () {
        
        int num = Random.Range(0, allPath.Length);
        transform.position = allPath[num].transform.position;
        MoveOnPath path = GetComponent<MoveOnPath>();
        path.pathName = allPath[num].name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
