﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireThrowerCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().TakeDamage(1);
        }
    }
}
