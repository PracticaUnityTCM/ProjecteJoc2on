using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShipCharger : MonoBehaviour {
    ShipBulletsController ShipBulletsCtr;
	// Use this for initialization
	void Start () {
        ShipBulletsCtr = GetComponent<ShipBulletsController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("sss");
        if (ShipBulletsCtr.PowerUpBigCharger)
        {
            if (collider.gameObject.tag == "Turret")
            {
                if (collider.gameObject.GetComponent<TurretController>().CanReciveDamage)
                {
                    transform.gameObject.GetComponent<ShipController>().RB.velocity = Vector3.zero;
                    collider.gameObject.GetComponent<TurretController>().TakeDamage(20);
                }
            }
            if (collider.gameObject.tag == "Enemy")
            {
                collider.gameObject.GetComponent<EnemyController>().TakeDamage(20);
            }
        }
    }
    void OnColliderEnter(Collision collider)
    {
        Debug.Log("sssmm");
        if (ShipBulletsCtr.PowerUpBigCharger)
        {
            if (collider.gameObject.tag == "Turret")
            {
                if (collider.gameObject.GetComponent<TurretController>().CanReciveDamage)
                {
                 
                    collider.gameObject.GetComponent<TurretController>().TakeDamage(20);
                }
            }
            if (collider.gameObject.tag == "Enemy")
            {
                collider.gameObject.GetComponent<EnemyController>().TakeDamage(20);
            }
        }
    }
}
