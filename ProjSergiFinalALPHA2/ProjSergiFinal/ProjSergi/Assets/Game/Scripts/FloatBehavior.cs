using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatBehavior : MonoBehaviour
{
    float originalY;
    ShipController ShipController;
    public float floatStrength = 0.35f; // You can change this in the Unity Editor to 
                                    // change the range of y positions that are possible.

    void Start()
    {
        ShipController=transform.parent.gameObject.GetComponent<ShipController>();
        Debug.Log(transform.parent.transform.name+"s"); 
            this.originalY = this.transform.position.y;
    }

    void Update()
    {
        if(ShipController!=null)
        if (!ShipController.IsDeath)
            transform.position = new Vector3(transform.position.x,originalY + ((float)Mathf.Sin(Time.time) * floatStrength),transform.position.z);
    }
}