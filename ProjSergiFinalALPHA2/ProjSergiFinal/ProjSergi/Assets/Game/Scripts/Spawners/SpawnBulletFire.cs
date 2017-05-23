//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SpawnBulletFire : MonoBehaviour {
//    public GameObject BulletFire;
//	// Use this for initialization
//	void Start () {
		
//	}
	
//	// Update is called once per frame
//	void Update () {
//      ///  Debug.Log(transform.rotation.y + " y rotateion");
//       // Debug.Log(Quaternion.EulerAngles(transform.rotation) + "rotation of spawner");
//    }
//    public void SpawnFireFourBullet(Vector3 position, Quaternion rotation)
//    {
//        GameObject BulletFireObj = Instantiate(BulletFire, position,rotation) as GameObject;
//        BulletFireObj.GetComponent<BulletFireController>().Fire(rotation);
//        EffectsManager.Instance.CreateStartThrowerSmokeShoot(transform.position,rotation,5f);
//        EffectsManager.Instance.CreateStartSmokeShoot(transform.position,rotation,2f);
//    }
//}
