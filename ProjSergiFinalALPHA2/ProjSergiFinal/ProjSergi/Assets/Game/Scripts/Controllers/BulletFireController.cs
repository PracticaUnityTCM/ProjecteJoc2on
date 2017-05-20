using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class BulletFireController : MonoBehaviour {
//    public float dist = 25f;
//    public float _angle = 45f;
//    Rigidbody Rb;
//    public float firingAngle = 45.0f;
//    public float gravity = 9.8f;
//    // Use this for initialization
//    void Start () {
//      //  Debug.Log("ddd");
//        // get Rigitbody from object
//        Rb = GetComponent<Rigidbody>();
//    //    Debug.Log(Rb+"rbSSSS");
//        // set rigidbody isKinematic False
//       Rb.isKinematic = true;
//        // set rigidbody useGravity True
//       Rb.useGravity = false;
//    }
	
//	// Update is called once per frame
//	void Update () {
        
//	}
//    private Vector3 calculateArcOfFire(float distance)
//    {
//        // calculate initival velocity required to land the cube on target using the formula (9)
//        float Vi = Mathf.Sqrt(distance * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * _angle * 2)));
//        float Vy, Vz;   // y,z components of the initial velocity

//        Vy = Vi * Mathf.Sin(Mathf.  Deg2Rad * _angle);
//        Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _angle);

//        // create the velocity vector in local space
//        Vector3 localVelocity = new Vector3(0f, Vy, Vz) * 10f;

//        // transform it to global vector
//        return transform.TransformVector(localVelocity);
//    }
//    public void Fire(Quaternion rotation)
//    {
//        dist = 5f;
//       // Debug.Log("ssssssss"+calculateArcOfFire(dist));
//     // GetComponent<Rigidbody>().velocity = calculateArcOfFire(dist);
//       StartCoroutine("SimulateProjectile",rotation);
//        EffectsManager.Instance.CreateStartBulletOnFire(gameObject, transform.position, rotation, dist,5f);
//    }
//    void OnTriggerEnter(Collider col){
     
//        if (col.gameObject.tag == "Water")
//        {
//            Debug.Log("sss");
//            EffectsManager.Instance.CreateStartSplahWater(transform.position,transform.rotation,5f);
//        }
//        if (col.gameObject.tag == "Ship")
//        {
//            //firebullet.Stop();
//        }
//       Destroy(gameObject);
//    }
    
//    IEnumerator SimulateProjectile(Quaternion rotation)
//    {
//        // Calculate distance to target
//        //  float target_Distance = Vector3.Distance(transform.position, Target.position);
//        float target_Distance = 5f;
//        // Calculate the velocity needed to throw the object to the target at specified angle.
//        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

//        // Extract the X  Y componenent of the velocity
//        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
//        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

//        // Calculate flight time.
//        float flightDuration = target_Distance / Vx+0.5f;
//        transform.rotation = rotation;
       

//        float elapse_time = 0;

//        while (elapse_time < flightDuration)
//        {
//            transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

//            elapse_time += Time.deltaTime;

//            yield return null;
//        }
//    }
//}
