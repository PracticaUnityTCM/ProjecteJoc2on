using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeTurretEnemy
{
    TurretRotateFourCanion,
    TurretRotateFourFlameThrower,
    TurretOneCanion,
    TurretFourCanion,
}
public class TurretController : MonoBehaviour {
    public float Timer=0.0f;
    public float TimerFireTotal=5f;
    public TypeTurretEnemy TypeTurretEnemy;
    public TypeBullet TypeBulletTorret;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
            Transform posSpawnerTop = Helpers.Helpers.FindDeepChild(transform, "SpawnPosTop");
            GetComponentInChildren<SpawnBullet>().SpawnFrontBullet(posSpawnerTop.position, posSpawnerTop.rotation, TypeBulletTorret, -1, "BulletShootEnemy");
            Transform posSpawnerBottom = Helpers.Helpers.FindDeepChild(transform, "SpawnPosBottom");
            GetComponentInChildren<SpawnBullet>().SpawnFrontBullet(posSpawnerBottom.position, posSpawnerBottom.rotation, TypeBulletTorret, -1, "BulletShootEnemy");
            Transform posSpawnerLeft = Helpers.Helpers.FindDeepChild(transform, "SpawnPosLeft");
            GetComponentInChildren<SpawnBullet>().SpawnFrontBullet(posSpawnerLeft.position, posSpawnerLeft.rotation, TypeBulletTorret, -1, "BulletShootEnemy");
            Transform posSpawnerRight = Helpers.Helpers.FindDeepChild(transform, "SpawnPosRight");
            GetComponentInChildren<SpawnBullet>().SpawnFrontBullet(posSpawnerRight.position, posSpawnerRight.rotation, TypeBulletTorret, -1, "BulletShootEnemy");
        }
        else if(TypeTurretEnemy== TypeTurretEnemy.TurretOneCanion)
        {
            Debug.Log("s");
            Transform posSpawnerTop = Helpers.Helpers.FindDeepChild(transform, "SpawnPos");
            GetComponentInChildren<SpawnBullet>().SpawnFrontBullet(posSpawnerTop.position, posSpawnerTop.rotation, TypeBulletTorret, -1, "BulletShootEnemy");
        }
    }
}
