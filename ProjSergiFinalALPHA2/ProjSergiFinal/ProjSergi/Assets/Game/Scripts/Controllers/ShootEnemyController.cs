﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemyController : MonoBehaviour {
    private GameObject Enemy;

	// Use this for initialization
	void Start () {
        Enemy = transform.parent.gameObject;
        	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Ship")
        {
            Enemy.GetComponent<EnemyController>().EnemyShipBehaibour = EnemyShipBehaivour.Shooting;
                }
        if (other.gameObject.tag == "FireCollider")
        {
        //    Debug.Log("sssff");
        }
    }
    void OnTriggerExit(Collider col)
    {
      

        Enemy.GetComponent<EnemyController>().EnemyShipBehaibour = EnemyShipBehaivour.Following;


      
    }

}
