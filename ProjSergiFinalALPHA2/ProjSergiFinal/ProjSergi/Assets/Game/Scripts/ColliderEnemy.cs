using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEnemy : MonoBehaviour {
    private SphereCollider ColliderShoot;
    private SphereCollider ColliderFollow;
	// Use this for initialization
	void Start () {
        //=transform.FindChild("").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet    ")
        {

        }
      
    }
}

