using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Helpers;
public enum TypeBullet
{
    Normal,
    Fire
}
public enum BulletShotForm
{

}
public class BulletController : MonoBehaviour
{
    ShipController ShipController;
    public Material fireBulletMaterial; 
    public Material normalBulletMaterial;
    public float dist = 5f;
    public float firingAngle = 20.0f;
    public float gravity = 9.8f;
    private float target_Distance;
    private float flightDuration;
    private TypeBullet bulletType;
    private Rigidbody RB;
    public Vector3 _startPosition;
    public Transform _parent;
    public float _time = 1f;
    void Start()
    {
        RB = gameObject.GetComponent<Rigidbody>();      // Rigidbody caching
        _startPosition = transform.localPosition;               // Save shot start position
        _parent = transform.parent;
    }

    void Update()
    {
     //   if()
    }
    public void ApplyStartVelocity(Vector3 startVelocity)
    {

        RB.isKinematic = false;

        RB.AddForce(Vector3.right * startVelocity.x, ForceMode.VelocityChange);
        RB.AddForce(Vector3.up * startVelocity.y, ForceMode.VelocityChange);
        RB.AddForce(Vector3.forward * startVelocity.z, ForceMode.VelocityChange);

    }

    public void ApplyThrust(Transform target)
    {

        float X;
        float Y;
        float Z;
        float X0;
        float Y0;
        float Z0;
        float V0x;
        float V0y;
        float V0z;
        float t;


        RB.isKinematic = false;

        Vector3 forceDirection = target.position - transform.position;

        X = forceDirection.x;       // Distance to travel along X : Space traveled @ time t
        Y = forceDirection.y;       // Distance to travel along Y : Space traveled @ time t
        Z = forceDirection.z;       // Distance to travel along Z : Space traveled @ time t

        // As we calculate in this very moment the distance between the shot object and the target, the intial space coordinates X0, Y0, Z0 will be always 0.
        X0 = 0;
        Y0 = 0;
        Z0 = 0;

        transform.parent = null;        // Detach the shot object from parent in order to get its own velocity

        t = _time;

        // Calculation of the required velocity along each axis to hit the target from the current starting position as if the shot object were stopped 
        V0x = (X - X0) / t;
        V0z = (Z - Z0) / t;
        V0y = (Y - Y0 + (0.5f * Mathf.Abs(Physics.gravity.magnitude) * Mathf.Pow(t, 2))) / t;

        /* Subtraction of the current velocity of the shot object */
        V0x -= RB.velocity.x;
        V0y -= RB.velocity.y;
        V0z -= RB.velocity.z;

        RB.AddForce(Vector3.right * V0x, ForceMode.VelocityChange); // VelocityChange Add an instant velocity change to the rigidbody, applying an impulsive force, ignoring its mass.
        RB.AddForce(Vector3.up * V0y, ForceMode.VelocityChange);
        RB.AddForce(Vector3.forward * V0z, ForceMode.VelocityChange);

     

    }
    public void SetTypeBullet(TypeBullet type)
    {
        bulletType = type;
        if (type == TypeBullet.Fire)
            GetComponent<Renderer>().material = fireBulletMaterial;
        else if (type == TypeBullet.Normal)
            GetComponent<Renderer>().material = normalBulletMaterial;
    }
    //private Vector3 calculateArcOfFire(float distance)
    //{
    //    // calculate initival velocity required to land the cube on target using the formula (9)
    //    float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * _angle * 2)));
    //    float Vy, Vz;   // y,z components of the initial velocity

    //    Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * _angle);
    //    Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _angle);

    //    // create the velocity vector in local space
    //    Vector3 localVelocity = new Vector3(0f, Vy, Vz) * 10f;

    //    // transform it to global vector
    //    return transform.TransformVector(localVelocity);
    //}
    //public void FireW(Quaternion rotation, Vector3 position,float velo)
    //{
    //    StartCoroutine(SimulateProjectileVel(Vector3.zero, rotation, false,velo));

    //}

    public void Fire(Quaternion rotation,Vector3 position)
    {
        StartCoroutine(SimulateProjectile(Vector3.zero, rotation, false));


        if (bulletType == TypeBullet.Fire)
            EffectsManager.Instance.CreateStartBulletOnFire(gameObject, position, rotation, dist, flightDuration+0.5f);
        
    }
     public  void FireToPosition(Vector3 pos, Quaternion Rotation,bool isShootShip,float VELOCITY)
    {
         // float dista = Vector3.Distance(transform.position, pos);
      
        StartCoroutine(SimulateProjectile(pos, Rotation,isShootShip));
        if (bulletType == TypeBullet.Fire)
              EffectsManager.Instance.CreateStartBulletOnFire(gameObject, transform.position, Rotation, dist, 5f);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Ship")
        {
            AudioManager.Instance.playSoundEfect("ExplosionBullet");
            EffectsManager.Instance.CreateStartFireExplosion(transform.position,transform.rotation,2f);
        }
        if (col.gameObject.tag == "Ship")
        {
            if (bulletType == TypeBullet.Normal)  
                GameManager.Instance.Ship.GetComponent<ShipController>().TakeDamage(9);
            if (bulletType == TypeBullet.Fire)  
                GameManager.Instance.Ship.GetComponent<ShipController>().TakeDamage(5);
            Destroy(gameObject);
        }
        if(col.gameObject.tag=="Enemy")
        {

            col.gameObject.GetComponent<EnemyController>().TakeDamage(4);
        }
        if (col.gameObject.tag == "Water")
        {
            AudioManager.Instance.playSoundEfect("WaterSplash");
            EffectsManager.Instance.CreateStartSplahWater(transform.position, transform.rotation, 5f);
            if (bulletType == TypeBullet.Fire)
            {
                Transform t = transform.GetChild(0);
                t.parent = null;
           
            }
            Destroy(gameObject);
        }
    }
    public void ShootBulletToPosition(Transform pos, Quaternion rotation)
    {
       // FireToPosition(pos.position, rotation);
    }
    IEnumerator SimulateProjectile(Vector3 tarjet,Quaternion rotationBullet,bool isShootShip)
    {
       // Debug.Log(rotationBullet.eulerAngles + "bullesst");
        // Short delay added before Projectile is thrown
        //yield return new WaitForSeconds(0.5f);

        // Move projectile to the position of throwing object + add some offset if needed.
        transform.position = transform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        //  float target_Distance = Vector3.Distance(transform.position, Target.position);

        if (tarjet == Vector3.zero)
        {
            target_Distance = 10f;
        }
        else
        {
            if (isShootShip) {

                target_Distance = Vector3.Distance(transform.position, tarjet);
                if (target_Distance > 5f)
                    target_Distance = 5f;
            }
            else
            target_Distance = Vector3.Distance(transform.position, tarjet);
        }
        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);
 
        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        flightDuration = target_Distance / Vx+1.5f;
        transform.rotation = rotationBullet;
       

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
         //   Debug.Log((Vy - (gravity * elapse_time)) * Time.deltaTime + " ypos ");
            transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
    public void ShootBulletForce(Vector3 direction)
    {
        EffectsManager.Instance.CreateStartThrowerSmokeShoot(transform.position, transform.rotation, 1.0f);
        EffectsManager.Instance.CreateStartSmokeShoot(transform.position, transform.rotation, 1.0f);
        if (bulletType == TypeBullet.Fire)
            EffectsManager.Instance.CreateStartBulletOnFire(gameObject, transform.position, transform.rotation, dist, 5f);
        GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
       
       
        
    }
    //public void ShootBulletToPosicionForce(Vector3 target,Vector3 origin,float force)
    //{
    //    ThrowBallAtTargetLocation(target, 2f);
    //    Vector3 shoot = (target - origin);
    //    Debug.Log(shoot.magnitude+""+ calculateBestThrowSpeed(origin,target,5f));
    //   // GetComponent<Rigidbody>().AddForce(calculateBestThrowSpeed(origin, target,2f) ,ForceMode.Impulse);
    //} 

    //public void FixedUpdate()
    //{

    //}
    //IEnumerator SimulateProjectileVel(Vector3 tarjet, Quaternion rotationBullet, bool isShootShip,float velo)
    //{
    //    // Debug.Log(rotationBullet.eulerAngles + "bullesst");
    //    // Short delay added before Projectile is thrown
    //    //yield return new WaitForSeconds(0.5f);
    //    //NPOTSupport fer una cosa: en el forward guarda la velocitat del barco en una variable global, aqui l'agafes i la sumes a la velocitat de la bala' ok si no funciona aixi, 'fes-ho amb la posició, guarda la posicio del barco en cada moment i aqui quan dispares fas que dispari la bala a partir del punt de la posicio del barco ok'

    //    // Move projectile to the position of throwing object + add some offset if needed.
    //    transform.position = transform.position + new Vector3(0, 0.0f, 0);

    //    // Calculate distance to target
    //    //  float target_Distance = Vector3.Distance(transform.position, Target.position);
    //    //sino hi h
    //    if (tarjet == Vector3.zero)
    //    {
    //        target_Distance = 5f;
    //    }
    //    else
    //    {
    //        if (isShootShip)
    //        {

    //            target_Distance = Vector3.Distance(transform.position, tarjet);
    //            if (target_Distance > 5f)
    //                target_Distance = 5f;
    //        }
    //        else
    //            target_Distance = Vector3.Distance(transform.position, tarjet);
    //    }
    //    // Calculate the velocity needed to throw the object to the target at specified angle.
    //    float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);
    //    float VxShip=0.0f;
    //    float Vx, Vy;
    //                    float VyShip=0.0f;
    //    //// Extract the X  Y componenent of the velocity
    //    //  ShipController.CharacterParameters.currentVelocityForwards
    //    float velocitat_vaixell = velo;
    //    if (velo ==0.0f){
    //        velocitat_vaixell = 1;
    //    }
    //    //la velocitat del barco es 0,0
    //    //if (ShipController.CharacterParameters.currentVelocityForwards==0.0f)
    //    //{
    //      //  Vx = Mathf.Sqrt(projectile_Velocity + ShipController.CharacterParameters.currentVelocityForwards) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
    //       // Vy = Mathf.Sqrt(projectile_Velocity + ShipController.CharacterParameters.currentVelocityForwards) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

    //    //}
    //    //else
    //    //{
    //        Vx = 2+( Mathf.Sqrt(projectile_Velocity+velocitat_vaixell) * Mathf.Cos(firingAngle * Mathf.Deg2Rad));
    //        Vy = 2+ (Mathf.Sqrt(projectile_Velocity+velocitat_vaixell) * Mathf.Sin(firingAngle * Mathf.Deg2Rad));
    //    Debug.Log(projectile_Velocity + "projectil" + velocitat_vaixell + " firingAngle " + firingAngle + "sinus " + Mathf.Sin(firingAngle * Mathf.Deg2Rad) + "cosinus " + Mathf.Cos(firingAngle * Mathf.Deg2Rad) + " Vx " + Vx + " Vy " + Vy);
    //    // Debug.Log("vy "+Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad));

    //    // Calculate flight time.
    //    flightDuration = target_Distance / Vx + 1.5f;
    //    transform.rotation = rotationBullet;


    //    float elapse_time = 0;

    //    while (elapse_time < flightDuration)
    //    {
    //          //Debug.Log((Vy - (gravity * elapse_time)) * Time.deltaTime + " ypos ");
    //        transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

    //        elapse_time += Time.deltaTime;

    //        yield return null;
    //    }
    //}
    //private Vector3 calculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget)
    //{
    //    // calculate vectors
    //    Vector3 toTarget = target - origin;
    //    Vector3 toTargetXZ = toTarget;
    //    toTargetXZ.y = 0;

    //    // calculate xz and y
    //    float y = toTarget.y;
    //    float xz = toTargetXZ.magnitude;

    //    // calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
    //    // where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
    //    // so xz = v0xz * t => v0xz = xz / t
    //    // and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
    //    float t = timeToTarget;
    //    float v0y = y / t + 0.5f * Physics.gravity.magnitude * t;
    //    float v0xz = xz / t;

    //    // create result vector for calculated starting speeds
    //    Vector3 result = toTargetXZ.normalized;        // get direction of xz but with magnitude 1
    //    result *= v0xz;                                // set magnitude of xz to v0xz (starting speed in xz plane)
    //    result.y = v0y;                                // set y to v0y (starting speed of y plane)

    //    return result;
    //}
    //public void ThrowBallAtTargetLocation(Vector3 targetLocation, float initialVelocity)
    //{
    //    Vector3 direction = (targetLocation - transform.position).normalized;
    //    float distance = Vector3.Distance(targetLocation, transform.position);

    //    float firingElevationAngle = FiringElevationAngle(Physics.gravity.magnitude, distance, initialVelocity);
    //    Vector3 elevation = Quaternion.AngleAxis(firingElevationAngle, transform.right) * transform.up;
    //    float directionAngle = AngleBetweenAboutAxis(transform.forward, direction, transform.up);
    //    Vector3 velocity = Quaternion.AngleAxis(directionAngle, transform.up) * elevation * initialVelocity;

    //    // ballGameObject is object to be thrown
    //    GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);
    //}

    //// Helper method to find angle between two points (v1 & v2) with respect to axis n
    //public static float AngleBetweenAboutAxis(Vector3 v1, Vector3 v2, Vector3 n)
    //{
    //    return Mathf.Atan2(
    //        Vector3.Dot(n, Vector3.Cross(v1, v2)),
    //        Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    //}

    //// Helper method to find angle of elevation (ballistic trajectory) required to reach distance with initialVelocity
    //// Does not take wind resistance into consideration.
    //private float FiringElevationAngle(float gravity, float distance, float initialVelocity)
    //{
    //    float angle = 0.5f * Mathf.Asin((gravity * distance) / (initialVelocity * initialVelocity)) * Mathf.Rad2Deg;
    //    return angle;
    //}
}
