using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBulletsController : MonoBehaviour
{

    ShipController ShipController;
    [Header("Front bullet parameters")]
    public float forceMultiplierShootFront = 3000f;
    public float ComponentYShootFront = 0.1f;
    public float powerShoot = 0.0f;
    [Header("Timer PowerUp Bullets Fire")]
    public float PUBulletFireTimer;
    public float PUBulletFireTimerTotal=5f;
    [Header("Timer PowerUp Fire Thrower")]
    public float PUFireThrowerTimer;
    public float PUFireThrowerTimerTotal = 15f;
    [Header("Timer PowerUp Speed Charger")]
    public float SpeedTimer = 0.0f;
    public float SpeedTimerTotal = 2.0f;

    public bool HandleInputs;
    public TypeBullet typeBulletLocal;
    public bool BulletFireActive;
    public bool powerUpFireThrowerActive;
    public bool PowerUpBigCharger;
   
   
    void Start()
    {
        HandleInputs = true;
        ShipController = GetComponent<ShipController>();
        
    }
    
   
  
    public void SetTypeBullet(TypeBullet typeBullet)
    { 
        typeBulletLocal = typeBullet;
        BulletFireActive = true;
    }
    
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    void Update()
    {
        if (BulletFireActive)
        {
            PUBulletFireTimer += Time.deltaTime;
            if (PUBulletFireTimer > PUBulletFireTimerTotal)
            {
                typeBulletLocal = TypeBullet.Normal;
                BulletFireActive = false;
               
                PUBulletFireTimer = 0;
            }
        }
        if (powerUpFireThrowerActive)
        {
            PUFireThrowerTimer += Time.deltaTime;
            if(PUFireThrowerTimer > PUFireThrowerTimerTotal)
            {
                AudioManager.Instance.FireThrowerAudioEnd();
                AudioManager.Instance.playSoundEfect("EndFireThrower");
                powerUpFireThrowerActive = false;
                PUFireThrowerTimer = 0;
                EffectsManager.Instance.StopFireThrower();
            }
        }
        if (HandleInputs)
        {
            //if (Input.GetMouseButtonDown(0) && GameManager.Instance.CanShootNumBullets(1))
            //{
            //    Vector3 pos;
            //    RaycastHit hit;
            //   // Helpers.Helpers.FindDeepChild(transform, "zoneShoot").parent=null;
            //    Ray ray = Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            //    if (Physics.Raycast(ray, out hit, 300.0f))
            //    {

            //        Debug.DrawLine(ray.origin, hit.point);
            //        Vector3 targetDir = hit.point - transform.position;
            //        targetDir = targetDir.normalized;

            //        float dot = Vector3.Dot(targetDir, transform.forward);
            //        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
            //       // float vector=Vector3.Angle(transform.forward, hit.transform.position);

            //        if (angle<30.0f)
            //        {
            //            pos = hit.point;
            //            pos = new Vector3(pos.x, 0.0f, pos.z);
            //            Transform s = Helpers.Helpers.FindDeepChild(transform, "SpawnBulletFront");
            //            s.transform.LookAt(pos);
            //            AudioManager.Instance.playSoundEfect("CannonShot");
            //            GameManager.Instance.DescreseAmuntion(1);
            //            GetComponentInChildren<SpawnBullet>().SpawnBulletToPosition( typeBulletLocal, pos, "BulletShootPlayer",true);
            //        }
            //    }
            //}
            if (Input.GetKeyDown(KeyCode.Space))
            {
                powerShoot = 0.5f;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (powerShoot < 1f)
                    powerShoot += Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.Space) && CanShootNumBullets(1))
            {
                nextFire = Time.time + fireRate;
                Transform s = Helpers.Helpers.FindDeepChild(transform, "SpawnBulletFront");
                GetComponentInChildren<SpawnBullet>().SpawnBulletFrontForce(new Vector3(transform.forward.x, ComponentYShootFront, transform.forward.z) * powerShoot * forceMultiplierShootFront, typeBulletLocal, "BulletShootPlayer");
                AudioManager.Instance.playSoundEfect("CannonShot");
                GameManager.Instance.DescreseAmuntion(1);
            }
            if (CanShootNumBullets(3))
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    // get position object child of boat and named
                    Transform s = Helpers.Helpers.FindDeepChild(transform, "SpawnBulletsLeft");
                    GetComponentInChildren<SpawnBullet>().SpawnLateralBullets(s, transform.rotation, false, typeBulletLocal, "BulletShootPlayer");
                    // Debug.Log(transform.rotation.eulerAngles);
                    AudioManager.Instance.playSoundEfect("CannonShot");
                    GameManager.Instance.DescreseAmuntion(3);
                }
                if (Input.GetKeyDown(KeyCode.L))
                {
                    Transform s = Helpers.Helpers.FindDeepChild(transform, "SpawnBulletsRight");
                    GetComponentInChildren<SpawnBullet>().SpawnLateralBullets(s, transform.rotation, true, typeBulletLocal, "BulletShootPlayer");
                    AudioManager.Instance.playSoundEfect("CannonShot");
                    GameManager.Instance.DescreseAmuntion(3);
                }
            }
            if (powerUpFireThrowerActive)
            {
                //if (Input.GetKeyDown(KeyCode.F))
                //{
                //    AudioManager.Instance.playSoundEfect("StartFireThrower");
                //    AudioManager.Instance.FireThrowerAudioCenter();
                //}
                //if (Input.GetKeyUp(KeyCode.F))
                //{
                //    AudioManager.Instance.FireThrowerAudioEnd();
                   
                //    AudioManager.Instance.playSoundEfect("EndFireThrower");
                //}
                //if (Input.GetKey(KeyCode.F))
                //{
                    EffectsManager.Instance.StartFireThrower(); 
                //}
                //else
                //    EffectsManager.Instance.StopFireThrower();
            }
        }
        // si te powerup carrega
        if(PowerUpBigCharger)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                SpeedTimer = SpeedTimerTotal + Time.deltaTime;
                if (!ShipController.ControllerState.isCollidingFront)
                {
                    ShipController.RB.AddForce(transform.forward * 1500f, ForceMode.Acceleration);
                    StartCoroutine("Wait");// ShipController.CharacterParameters.currentVelocityForwards = 2.0f;
                }
            }
        }
     
    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        PowerUpBigCharger = false;
    }
    
    public bool CanShootNumBullets(int num)
    {
        if (ShipController.amunnition < num)
            return false;
        return true;
    }
    public void SetBigChargerShip()
    {
        PowerUpBigCharger = true;
    }
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("ssssssssssssssssssss");
    }
}
