using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatBehavior : MonoBehaviour
{
    float originalY;
    ShipController ShipController;
    public float speed = 5f;
    public float floatStrength = 0.35f; 
    void Start()
    {
        //// RB = GetComponent<Rigidbody>();
        ShipController = transform.parent.gameObject.GetComponent<ShipController>();
        this.originalY = this.transform.position.y;
    }
    void Update()
    {
        if (ShipController != null)
            if (!ShipController.IsDeath)
            {
                Quaternion from = Quaternion.Euler(new Vector3(0, transform.parent.rotation.y, 5f));
                Quaternion to = Quaternion.Euler(new Vector3(0, transform.parent.rotation.y, -5f));
                float t = (Mathf.Sin(Time.time * speed * Mathf.PI * 2.0f) + 1.0f) / 2.0f;
                t = Mathf.PingPong(Time.time * speed, 0.5f);
                transform.rotation=Quaternion.Euler(new Vector3(0.0f,transform.parent.rotation.eulerAngles.y,(Quaternion.Slerp(from, to, t).eulerAngles.z)));
                transform.position = new Vector3(transform.position.x, originalY + ((float)Mathf.Sin(Time.time) * floatStrength), transform.position.z);
            }
     }
}