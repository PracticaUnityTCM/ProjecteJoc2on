using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBulletsController : MonoBehaviour
{

    ShipController ShipController;
    [Header("Front bullet parameters")]
    public float forceMultiplierShootFront = 3000f;
    public float ComponentYShootFront = 0.1f;

    public float PUBulletFireTimer;
    public float PUBulletFireTimerTotal=5f;

    public float SpeedTimer = 0.0f;
    public float SpeedTimerTotal = 2.0f;
    public bool CanAttackMouse;
    public GameObject Camera;
    public bool HandleInputs;
    public enum TypeAtack
    {
        Normal,
        Mouse
    }
    public TypeAtack typeAtack;
    public TypeBullet typeBulletLocal;


    public float powerShoot = 0.0f;
    private LayerMask layer;
    private LayerMask ignoreCameralayer;
    void Start()
    {
        HandleInputs = true;
        ShipController = GetComponent<ShipController>();
        layer = LayerMask.GetMask("ShootZone");
        ignoreCameralayer = (1 << 21);
        //
    }
    
   
  
    public void SetTypeBullet(TypeBullet typeBullet)
    { 
        typeBulletLocal = typeBullet;
    }
    public void UpdateBulletType(TypeBullet typeBullet)
    {
        typeBulletLocal = typeBullet;
        PUBulletFireTimer += Time.deltaTime;
        if (PUBulletFireTimer > PUBulletFireTimerTotal)
        {
            typeBulletLocal = TypeBullet.Normal;
            PUBulletFireTimer = 0;
        }
        
    }
    public GameObject projectile;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    void Update()
    {
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
                if (powerShoot < 3f)
                    powerShoot += Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                nextFire = Time.time + fireRate;
                Transform s = Helpers.Helpers.FindDeepChild(transform, "SpawnBulletFront");
                GetComponentInChildren<SpawnBullet>().SpawnBulletFrontForce(new Vector3(transform.forward.x, ComponentYShootFront, transform.forward.z) * powerShoot * forceMultiplierShootFront, TypeBullet.Normal, "BulletShootPlayer");
                AudioManager.Instance.playSoundEfect("CannonShot");
                GameManager.Instance.DescreseAmuntion(1);
            }


            //if (ShipController.CharacterParameters.typeShip == typeShip.Europe)
            //{
            if (GameManager.Instance.CanShootNumBullets(3))
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

            //    if (powerUpFireActive)
            if(false)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    //AudioManager.Instance.PlaySound("StartFireThrower", transform.position);
                }
                if (Input.GetKeyUp(KeyCode.J))
                {
                    //  AudioManager.Instance.FireThrowerAudioEnd();
                }
                if (Input.GetKey(KeyCode.J))
                {
                    EffectsManager.Instance.StartFireThrower();
                    AudioManager.Instance.FireThrowerAudioCenter();
                }
                else
                {
                    EffectsManager.Instance.StopFireThrower();

                }
            }
        }
        // si te powerup carrega
        if(false)
        {
           // if (Input.GetKeyDown(KeyCode.J))
            //    {
            //        SpeedTimer = SpeedTimerTotal + Time.deltaTime;
            //        ShipController.CharacterParameters.currentVelocityForwards = 2.0f;
            //        if (SpeedTimerTotal > 3.0f)
            //        {
            //            //   AudioManager.Instance.GritoVikingo();
            //            ShipController.CharacterParameters.currentVelocityForwards = ShipController.Decelerate(ShipController.CharacterParameters.currentVelocityForwards);
            //        }
            //    }
        }
     
    }
    
   
}
