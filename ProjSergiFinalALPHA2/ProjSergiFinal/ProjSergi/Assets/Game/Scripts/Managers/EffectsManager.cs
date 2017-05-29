using System.Collections;
using System.Collections.Generic;
using Helpers;
using System;


using UnityEngine;
public class Trail
{
    public GameObject TrailLocalObj;
    public GameObject TrailObj;
    public GameObject MistObj;
}
public class EffectsManager : MonoBehaviour {
    public static Dictionary<string, GameObject> SmokesDamages;
    public static Dictionary<string, Trail> TrailsBoats;
    public static  Dictionary<string, GameObject> FireThrowers;
    public GameObject MistBoatEffect;
    private GameObject MistBoatObj;
    public GameObject TrailBoatEffect;
    private GameObject TrailBoatObj;
    public GameObject TrailBoatEffectLocal;
    private GameObject TrailBoatLocalObj;
    public GameObject FireThower;
    
    private GameObject FireThrowerObjShip;
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
    private int NumFireThrower;
    private static EffectsManager _instance = null;
   
    public static EffectsManager Instance
    {
        get { return _instance; }
    }
    static public bool once_call;

    private void Awake()
    {
        if (!once_call)
        {
            DontDestroyOnLoad(this);
            once_call = true;
            _instance = this;
            DontDestroyOnLoad(_instance);
            // DontDestroyOnLoad(gameObject);
            
            TrailsBoats = new Dictionary<string, Trail>();
            FireThrowers = new Dictionary<string, GameObject>();
            SmokesDamages = new Dictionary<string, GameObject>();
        }
        else
        {
            Destroy(gameObject);
        }
        //if (_instance == null)
        //{
        //    _instance = FindObjectOfType(typeof(EffectsManager)) as EffectsManager;
        //    if (_instance == null) // create new object
        //    {
             
        //    }
        //}
        //else if (_instance != this)
        //{
        //    Destroy(gameObject);
        //}
        //if (_instance != null && _instance != this)
        //{
        //    Destroy(this.gameObject);
        //}
        //else
        //{
        //    _instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
        NumSmokeDamage = 0;
        NumFireThrower = 0;
      
    }
    // Use this for initialization
    void Start () {
        TrailsBoats = new Dictionary<string, Trail>();
        FireThrowers = new Dictionary<string, GameObject>();
        SmokesDamages = new Dictionary<string, GameObject>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void CreateSmokeDamage(GameObject parent , Vector3 position,Quaternion rotation,string nameObject)
    {
        GameObject objnew;
       
        if (EnableEffects && !SmokesDamages.TryGetValue(nameObject,out objnew))
        {
            objnew = Instantiate(DamageSmoke, position, Quaternion.Euler(new Vector3(rotation.x - 90f, rotation.y, rotation.z))) as GameObject;
            objnew.SetActive(true);
            Helpers.Helpers.Parent(parent, objnew);
            SmokesDamages.Add(nameObject, objnew);
            Debug.Log(nameObject+"crates");
        }
    }
    public void UpdateDamage(string name,float health,bool isShip,EnemyShipBehaivour enemyBehaivour)
    {
        GameObject obj;
        Debug.Log(name+"Updates");
        if (EnableEffects)
        {
            if (SmokesDamages.TryGetValue(name, out obj)) 
                if(obj!=null)obj.GetComponent<WhiteSmokeDamageEffect>().UpdateSmoke(health, isShip, enemyBehaivour);
            
               else
             Debug.LogError("Smoke damage not fount");
        }
    }
    public void CreateFireThrower(GameObject parent,Vector3 position,Quaternion rotation , string nameObject,string layer)
    {
        GameObject newObj;
        if (EnableEffects)
        {
            newObj = Instantiate(FireThower, position, Quaternion.Euler(new Vector3(rotation.x , rotation.y, rotation.z))) as GameObject;
            newObj.SetActive(false);
            FireThrowers.Add(nameObject, newObj);
            Helpers.Helpers.Parent(parent, newObj);
            newObj.transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer(layer);
           
        }
    }
    public void CreateFireThrowerShip(GameObject parent, Vector3 position,Quaternion rotation,string layer)
    {
        if (EnableEffects)
        {
            FireThrowerObjShip = Instantiate(FireThower, position, rotation) as GameObject;
            FireThrowerObjShip.SetActive(false);
            FireThrowerObjShip.layer = LayerMask.NameToLayer(layer);
            ///FireThrowers.Add(FireThrowerObjShip.name + NumFireThrower, FireThrowerObjShip);
            Helpers.Helpers.Parent(parent, FireThrowerObjShip);
        }
    }
    public void StartFireThrower()
    {

        if (EnableEffects)
        {
            FireThrowerObjShip.SetActive(true);
            FireThrowerObjShip.gameObject.GetComponent<FireThrowerEffect>().StartFireEffect();
        }
    }
    public void StopFireThrower()
    {
        if (EnableEffects)
        {
            FireThrowerObjShip.gameObject.GetComponent<FireThrowerEffect>().Stop();
            FireThrowerObjShip.SetActive(false);
        }
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
    public void CreateTrailMistBoat(GameObject parent, Vector3 position, Quaternion rotation, string nameObj)
    {
        Trail obj;
        if (!TrailsBoats.TryGetValue(nameObj, out obj))
        { 
        GameObject newObjLocal, newObj, mistObj;
        mistObj = Instantiate(MistBoatEffect, position, rotation) as GameObject;
        newObj = Instantiate(TrailBoatEffect, position, rotation) as GameObject;
        newObjLocal = Instantiate(TrailBoatEffectLocal, position, rotation) as GameObject;
        Trail TrailObj = new Trail();
        TrailObj.TrailLocalObj = newObjLocal;
        TrailObj.TrailObj = newObj;
        TrailObj.MistObj = mistObj;

        TrailsBoats.Add(nameObj, TrailObj);
        Helpers.Helpers.Parent(parent, newObjLocal);
        Helpers.Helpers.Parent(parent, newObj);
        Helpers.Helpers.Parent(parent, mistObj);
        }
    }
    public void UpdateTrailMistBoat(float velocity,string nameObj)
    {
        Trail obj;
        if (TrailsBoats.TryGetValue(nameObj, out obj))
        {
            obj.TrailLocalObj.GetComponentInChildren<TrailBoatEffect>().UpdateParticleSystemTrailForceOverLifeTime(velocity);
            obj.TrailObj.GetComponentInChildren<TrailBoatEffect>().UpdateParticleSystemTrail(velocity);
           obj.MistObj.GetComponentInChildren<MistBoatEffect>().UpdateMistBoatEffect(velocity);
           // TrailBoatLocalObj.GetComponentInChildren<TrailBoatEffect>().UpdateParticleSystemTrailForceOverLifeTime(velocity);
        }
    }
    //public  void CreateMistBoat(GameObject parent, Vector3 position, Quaternion rotation)
    //{
    //    MistBoatObj = Instantiate(MistBoatEffect, position, rotation) as GameObject;
    //    Helpers.Helpers.Parent(parent, MistBoatObj);
    //}
    //public void UpdateMistBoatEffect(float velocity)
    //{
    //    if (MistBoatObj == null)
    //        Debug.Log("ss");
    //    else
    //        Debug.Log("dd");
    //    MistBoatObj.GetComponentInChildren<MistBoatEffect>().UpdateMistBoatEffect(velocity);
    //}
    public void StartFireThrower(string name)
    {
        
        if(EnableEffects)
        {
            GameObject obj;
            if (FireThrowers.TryGetValue(name, out obj))
                if (obj != null) {
                    obj.SetActive(true);
                    obj.GetComponent<FireThrowerEffect>().StartFireEffect();
                }
        }
    }
    public void StopFireThrower(string name)
    {
        if (EnableEffects)
        {
            GameObject obj;
            if (FireThrowers.TryGetValue(name, out obj))
                if (obj != null) {
                    obj.GetComponent<FireThrowerEffect>().Stop();
                    obj.SetActive(false);
                }
        }
    }
    void OnDestroy()
    {
        TrailsBoats.Clear();
        SmokesDamages.Clear();
        FireThrowers.Clear();
    }
}
