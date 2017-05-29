using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLimitTurret : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ship")
        {
            transform.parent.gameObject.GetComponent<TurretController>().isEnabledTurret = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag=="Ship")
        {
            transform.parent.gameObject.GetComponent<TurretController>().isEnabledTurret = false;
        }
    }
}
