using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeTurretEnemy
{
    TurretRotateFourCanion,
    TurretRotateFourFlameThrower,
    TurretOneCanion,
    TurretOneFlameThrower,
    TurretFourCanion,
}
public class TurretController : MonoBehaviour {
    public bool CanReciveDamage;
    public float Health;
    public int MaxHealth=100;
    public float Timer=0.0f;
    public float TimerFireTotal=3f;
    public TypeTurretEnemy TypeTurretEnemy;
    public TypeBullet TypeBulletTorret;
    private GameObject Canvas;
    public bool IsDeath;
    public bool isEnabledTurret=false;
	// Use this for initialization
	void Start () {
        
        if (TypeTurretEnemy == TypeTurretEnemy.TurretOneFlameThrower)
        {
            Transform SpawnPos = Helpers.Helpers.FindDeepChild(transform, "SpawnPos");
            EffectsManager.Instance.CreateFireThrower(gameObject, SpawnPos.position, SpawnPos.rotation,transform.name,"FireLayerEnemy");
        }
        if ((TypeTurretEnemy == TypeTurretEnemy.TurretOneCanion || TypeTurretEnemy == TypeTurretEnemy.TurretRotateFourCanion)&& CanReciveDamage)
        {
            Transform TransSmoke = Helpers.Helpers.FindDeepChild(transform, "SpawnEffects");
            EffectsManager.Instance.CreateSmokeDamage(gameObject, TransSmoke.position, TransSmoke.rotation, transform.name);
        }
	}
    void Awake()
    {
        
        if ((TypeTurretEnemy == TypeTurretEnemy.TurretOneCanion || TypeTurretEnemy==TypeTurretEnemy.TurretRotateFourCanion )&& CanReciveDamage)
        {
            Health = MaxHealth;
            Canvas = transform.GetChild(2).gameObject;
            Canvas.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update ()
    {
        if ((TypeTurretEnemy == TypeTurretEnemy.TurretOneCanion || TypeTurretEnemy == TypeTurretEnemy.TurretRotateFourCanion) && Health < 99f && CanReciveDamage)
        {
            Canvas.SetActive(true);
        }
        if (isEnabledTurret)

            Timer += Time.deltaTime;
        if (Timer > TimerFireTotal) {
                            RepeatSpawn();
            Timer = 0;
        }
	}
    
    
    void RepeatSpawn()
    {
        if (TypeTurretEnemy == TypeTurretEnemy.TurretRotateFourCanion)
        {
            //Transform posSpawnerTop = Helpers.Helpers.FindDeepChild(transform, "SpawnPosTop");
            //GetComponentInChildren<SpawnBullet>().SpawnFrontBullet(posSpawnerTop.position, posSpawnerTop.rotation, TypeBulletTorret, -1, "BulletShootEnemy");
            //Transform posSpawnerBottom = Helpers.Helpers.FindDeepChild(transform, "SpawnPosBottom");
            //GetComponentInChildren<SpawnBullet>().SpawnFrontBullet(posSpawnerBottom.position, posSpawnerBottom.rotation, TypeBulletTorret, -1, "BulletShootEnemy");
            //Transform posSpawnerLeft = Helpers.Helpers.FindDeepChild(transform, "SpawnPosLeft");
            //GetComponentInChildren<SpawnBullet>().SpawnFrontBullet(posSpawnerLeft.position, posSpawnerLeft.rotation, TypeBulletTorret, -1, "BulletShootEnemy");
            Transform posSpawnerRight = Helpers.Helpers.FindDeepChild(transform, "SpawnPosRight");
            posSpawnerRight.gameObject.GetComponentInChildren<SpawnBullet>().SpawnBulletFrontForce(new Vector3(posSpawnerRight.forward.x, 0.2f, posSpawnerRight.forward.z) * 200f, TypeBulletTorret, "BulletShootEnemy");
            Transform posSpawnerTop = Helpers.Helpers.FindDeepChild(transform, "SpawnPosTop");
            posSpawnerTop.gameObject.GetComponentInChildren<SpawnBullet>().SpawnBulletFrontForce(new Vector3(posSpawnerTop.forward.x, 0.2f, posSpawnerTop.forward.z) * 200f, TypeBulletTorret, "BulletShootEnemy");
            Transform posSpawnerBottom = Helpers.Helpers.FindDeepChild(transform, "SpawnPosBottom");
            posSpawnerBottom.gameObject.GetComponentInChildren<SpawnBullet>().SpawnBulletFrontForce(new Vector3(posSpawnerBottom.forward.x, 0.2f, posSpawnerBottom.forward.z) * 200f, TypeBulletTorret, "BulletShootEnemy");
            Transform posSpawnerLeft = Helpers.Helpers.FindDeepChild(transform, "SpawnPosLeft");
            posSpawnerLeft.gameObject.GetComponentInChildren<SpawnBullet>().SpawnBulletFrontForce(new Vector3(posSpawnerLeft.forward.x, 0.2f, posSpawnerLeft.forward.z) * 200f, TypeBulletTorret, "BulletShootEnemy");

        }
        else if(TypeTurretEnemy== TypeTurretEnemy.TurretOneCanion)
        {
            Transform posSpawnerTop = Helpers.Helpers.FindDeepChild(transform, "SpawnPos");
           GetComponentInChildren<SpawnBullet>().SpawnBulletFrontForce(new Vector3(-posSpawnerTop.up.x, 0.2f, -posSpawnerTop.up.z) * 200f, TypeBulletTorret, "BulletShootEnemy");
        }
       
    }
    
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
            Kill();
    }
    public void Kill()
    {
        Health = 0;
        IsDeath = true;
        //RB.useGravity = true;
        //RB.isKinematic = false;
        StartCoroutine("KillTurretCo");
    }
    IEnumerator KillTurretCo()
    {
        yield return null;
        
    }
}
