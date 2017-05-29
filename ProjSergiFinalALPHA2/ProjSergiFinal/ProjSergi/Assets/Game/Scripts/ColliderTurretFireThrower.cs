using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTurretFireThrower : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ship")
        {
            Debug.Log("sstriggerenter on turrer");
            EffectsManager.Instance.StartFireThrower(transform.parent.name);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ship")
        {
            EffectsManager.Instance.StopFireThrower(transform.parent.name);
        }
    }
}
