using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyShipBehaivour
{
    Patroling,
    Following,
    Shooting,
    Turret
}
public class EnemyController : MonoBehaviour {
    //public CharacterParameters CharacterParameters;
    
    public Transform Player;
    public GameObject Bullet;
    public float rotationSpeed=250f;
    public float moveSpeed=2f;
    public bool isFollowing;
    //public Vector3 distance;
    public float distance;
    public bool isShooting;
    public bool isPatrolling=true;
    public float TimerShoot = 0.0f;
    public float TimerShootTotal=5f;
    public Rigidbody RB;
    public int Health;
    public bool IsDeath;
    public int MaxHealth=100;
    
    public TypeBullet typeBulletEnemy;
    public EnemyShipBehaivour EnemyShipBehaibour;
   private GameObject Canvas;
    
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
        RB.useGravity = true;
        RB.isKinematic = false;
        StartCoroutine("KillEnemyCo");
    }
    IEnumerator KillEnemyCo()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
    // Use this for initialization
    void Start () {
        transform.rotation = Quaternion.Euler(new Vector3(0 , 0, 0));
        Player = GameObject.Find("Ship").transform;
        EnemyShipBehaibour = EnemyShipBehaivour.Patroling;
        Debug.Log(transform.parent.name+"ssssssssssssss");
        Transform TransSmoke = Helpers.Helpers.FindDeepChild(transform, "SpawnEffects");
        EffectsManager.Instance.CreateTrailMistBoat(gameObject,new Vector3(transform.position.x,transform.position.y+1.5f,transform.position.z), transform.rotation, transform.parent.name);
        EffectsManager.Instance.CreateSmokeDamage(gameObject, TransSmoke.position, TransSmoke.rotation, transform.parent.name);
    }
	void Awake()
    {
        Debug.Log(transform.parent.name+"name of parent obj");
        RB = GetComponent<Rigidbody>();
        Health = MaxHealth;
        Canvas = transform.GetChild(3).gameObject;
        Canvas.SetActive(false);
    }
	// Update is called once per frame
	void Update ()
    {
        EffectsManager.Instance.UpdateTrailMistBoat(GetComponent<MoveOnPath>().speed/2f,transform.parent.name);
        EffectsManager.Instance.UpdateDamage(transform.parent.name, Health, false, EnemyShipBehaibour);
        if (Health < 99f)
        {
            Canvas.SetActive(true);   
        }
        
        if (!IsDeath)
        {
            if (EnemyShipBehaibour==EnemyShipBehaivour.Following)
            {
                Debug.Log("ssFollow player");
                LookAtPlayer();
                transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            }
            if (EnemyShipBehaibour==EnemyShipBehaivour.Shooting)
            {
                LookAtPlayer();
                TimerShoot += Time.deltaTime;
                if (TimerShoot > TimerShootTotal)
                { 
                    Transform s = Helpers.Helpers.FindDeepChild(transform, "SpawnFrontBullet");
                    GetComponentInChildren<SpawnBullet>().SpawnBulletToPosition(typeBulletEnemy, Player.position, "BulletShootEnemy",false);
                    TimerShoot = 0;
                }
            }
        }
    }
    void LookAtPlayer()
    {
      Vector3  targetPoint = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(targetPoint);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime *rotationSpeed);
    }
    
}


