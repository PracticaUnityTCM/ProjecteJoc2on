using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatBehavior : MonoBehaviour
{
    float originalY;
    Rigidbody RB;
    ShipController ShipController;
    public float speed = 5f;
    public Vector3 from = new Vector3(-40f, 8f, 150f);
    public Vector3 to = new Vector3(-40f, 8f, 155f);
    public float floatStrength = 0.35f; // You can change this in the Unity Editor to 
                                    // change the range of y positions that are possible.

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        ShipController=transform.parent.gameObject.GetComponent<ShipController>();
        Debug.Log(transform.parent.transform.name+"s"); 
            this.originalY = this.transform.position.y;
    }

    void Update()
    {
        if (ShipController != null)
            if (!ShipController.IsDeath)
            {
              //  from = new Vector3(transform.parent.rotation.x-40f, transform.parent.rotation.y+8f, 150f);
                //to = new Vector3(transform.parent.rotation.x-40f, transform.parent.rotation.y+8f, 155f);
                Quaternion from = Quaternion.Euler(new Vector3(0, 0, 5f));
                Quaternion to = Quaternion.Euler(new Vector3(0, 0, -5f));
                float t = (Mathf.Sin(Time.time * speed * Mathf.PI * 2.0f) + 1.0f) / 2.0f;
                //transform.eulerAngles = Vector3.Lerp(from, to, t);
                //   RB.AddRelativeTorque(Vector3.Lerp(from, to, t));
                t = Mathf.PingPong(Time.time * speed, 0.5f);
                transform.rotation = Quaternion.Slerp(from, to, t);
              //  transform.rotation = Quaternion.Euler(0.0f,0.0f, Mathf.PingPong(Time.deltaTime * 4f, -4.0f));
                transform.position = new Vector3(transform.position.x, originalY + ((float)Mathf.Sin(Time.time) * floatStrength), transform.position.z);
            }
            }
}