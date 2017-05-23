using System.Collections;
using System.Collections.Generic;
using Helpers;
using System;


using UnityEngine;

public class EffectsManager : MonoBehaviour {
    [SerializeField]
    public Dictionary<string, GameObject> SmokesDamages;
    public Dictionary<string, GameObject> TrailsBoats;
    public GameObject MistBoatEffect;
    private GameObject MistBoatObj;
    public GameObject TrailBoatEffect;
    private GameObject TrailBoatObj;

    public GameObject FireThower;
    
    private GameObject FireThrowerObj;
    
    public GameObject DamageSmoke;
    private GameObject DamageSmokeObj;
    public GameObject DropWater;
    private GameObject DropWaterObj;
    public GameObject SmokeThrowerShoot;
    private GameObject EffectSmokeThrowerObj;
    public GameObject SmokeCloudShoot;
    private GameObject SmokeCloudShootObj;
    public GameObject FireExplosion;
    private GameObject FireExplosionObj;
    public GameObject FireBullet;
    private GameObject FireBulletObj;
    public bool EnableEffects;
    private int NumSmokeDamage;
    private static EffectsManager _instance = null;
   
    public static EffectsManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        NumSmokeDamage = 0;
        SmokesDamages = new Dictionary<string, GameObject>();
    }
    // Use this for initialization
    void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void CreateSmokeDamage(GameObject parent , Vector3 position,Quaternion rotation,string nameObject)
    {
        GameObject objnew;
       
        if (EnableEffects)
        {
            objnew = Instantiate(DamageSmoke, position, Quaternion.Euler(new Vector3(rotation.x - 90f, rotation.y, rotation.z))) as GameObject;
            objnew.SetActive(true);
            Helpers.Helpers.Parent(parent, objnew);
            SmokesDamages.Add(parent.name+NumSmokeDamage, objnew);
            NumSmokeDamage++;
        }
    }
    public void UpdateDamageEnemy(EnemyShipBehaivour EnemyBehaiour,float health,string name)
    {
        GameObject obj;
        if (EnableEffects)
        {
          //  if(SmokesDamages.TryGetValue(name, out obj))
            //obj.GetComponent<WhiteSmokeDamageEffect>().UpdateEnemyBehaibour(EnemyBehaiour,health);
        }
    }
    public void UpdateDamage(string name,float health,bool isShip,EnemyShipBehaivour enemyBehaivour)
    {
        GameObject obj;

        if (EnableEffects)
        {
            if (SmokesDamages.TryGetValue(name, out obj)) 
            if(obj!=null)obj.GetComponent<WhiteSmokeDamageEffect>().UpdateSmoke(health, isShip, enemyBehaivour);
            
        else
            Debug.LogError("Smoke damage not fount");
        }
    }
    public void CreateFireThrower(GameObject parent,Vector3 position,Quaternion rotation)
    {
        if (EnableEffects)
        {
            FireThrowerObj = Instantiate(FireThower, position, rotation) as GameObject;
            FireThrowerObj.SetActive(false);
            Helpers.Helpers.Parent(parent, FireThrowerObj);
        }
    }
    public void StartFireThrower()
    {
   
        if (EnableEffects) 
        FireThrowerObj.gameObject.GetComponent<FireThrowerEffect>().StartFireEffect();
    }
    public void StopFireThrower()
    {   if(EnableEffects)
        FireThrowerObj.gameObject.GetComponent<FireThrowerEffect>().Stop();
    }
    public void CreateStartSplahWater(Vector3 position,Quaternion rotation,float duration) 
    {
        if (EnableEffects)
        {
            DropWaterObj = Instantiate(DropWater, position, rotation) as GameObject;
            DropWaterObj.GetComponent<SplashWaterEffect>().StartWaterSplash();
            StartCoroutine(WaitAndDestroyObj(duration, DropWaterObj));
        }
    }
    public void CreateStartThrowerSmokeShoot(Vector3 position,Quaternion rotation,float duration)
    {
        if (EnableEffects)
        {
            EffectSmokeThrowerObj = Instantiate(SmokeThrowerShoot, position, rotation) as GameObject;
            EffectSmokeThrowerObj.GetComponent<SmokeThrowerEffect>().StartSmokeEffect();
            StartCoroutine(WaitAndDestroyObj(duration, EffectSmokeThrowerObj));
        }
    }
    public void CreateStartSmokeShoot(Vector3 posicion, Quaternion rotation, float duration)
    {
        if (EnableEffects) { 
            SmokeCloudShootObj = Instantiate(SmokeCloudShoot, posicion, rotation) as GameObject;
            SmokeCloudShootObj.GetComponent<SmokeShotEffect>().StartSmokeShotEffect();
            StartCoroutine(WaitAndDestroyObj(duration, SmokeCloudShootObj));
        }
    }
    public void CreateStartBulletOnFire(GameObject parent, Vector3 position,Quaternion rotation,float distance,float duration)
    {
        if (EnableEffects)
        {
            FireBulletObj = Instantiate(FireBullet, position, rotation) as GameObject;
            Helpers.Helpers.Parent(parent, FireBulletObj);
            var ps = FireBulletObj.GetComponent<ParticleSystem>().main;
            ps.duration = distance;
            FireBulletObj.GetComponent<ParticleSystem>().Play();
            StartCoroutine(WaitAndDestroyObj(duration, FireBulletObj));
        }
    }
    public void CreateStartFireExplosion(Vector3 position, Quaternion rotation, float duration)
    {
        if (EnableEffects) {
            AudioManager.Instance.playSoundEfect("ExpolosionBullet");
            FireExplosionObj = Instantiate(FireExplosion, position, rotation) as GameObject;
            FireExplosionObj.GetComponent<EffectExploson>().StartEffectExploson();
        }
    }
    IEnumerator WaitAndDestroyObj(float num,GameObject gameObj)
    {
        yield return new WaitForSeconds(num);
        Destroy(gameObj);
    }
    public void CreateTrailBoat(GameObject parent,Vector3 positiion, Quaternion rotation)
    {
        TrailBoatObj = Instantiate(TrailBoatEffect, positiion, rotation) as GameObject;
        Helpers.Helpers.Parent(parent, TrailBoatObj);
    }
    public  void CreateMistBoat(GameObject parent, Vector3 position, Quaternion rotation)
    {
        MistBoatObj = Instantiate(MistBoatEffect, position, rotation) as GameObject;
        Helpers.Helpers.Parent(parent, MistBoatObj);
    }
}
