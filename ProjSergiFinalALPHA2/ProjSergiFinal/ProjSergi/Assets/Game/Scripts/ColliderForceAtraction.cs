using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderForceAtraction : MonoBehaviour {
    public float forceFactorEnter=10f;
    public float forceFactorStay=20f;
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Ship")
        {
            Debug.Log("ss");
            coll.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            coll.gameObject.GetComponent<Rigidbody>().AddForce(-(coll.gameObject.GetComponent<Transform>().position - transform.position) * forceFactorEnter * Time.smoothDeltaTime);
        }
    }
    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Ship")
            coll.gameObject.GetComponent<Rigidbody>().AddForce(-(coll.gameObject.GetComponent<Transform>().position - transform.position) * forceFactorStay * Time.smoothDeltaTime);
    }
    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Ship")
        {
         
            coll.gameObject.GetComponent<Rigidbody>().velocity = Vector3.one;
            coll.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        
        }
    }
}
